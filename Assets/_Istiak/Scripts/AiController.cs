using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using BallScripts;
using DG.Tweening;
using DiceScripts;
using MyGame.Utils;
using UnityEngine;
using Random = UnityEngine.Random;


public class AiController : MonoBehaviour
{
    public Transform enemyList;

    [SerializeField] private GameObject passBtnObj;

    // [SerializeField] private Transform allDices;
    public GameObject aiParticleHolder;
    public static AiController Instance;
    public static event Action<GameObject, GameObject> GiveBallToAiHand;
    public static event Action<GameObject, GameObject> MoveBallToPlayerThrowPoint;
    private GameObject currentAiObj;
    private TweenVariables tv;
    [HideInInspector] public bool canAttack;

    private void OnEnable()
    {
        canAttack = true;
        tv = GetComponent<TweenVariables>();
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [ContextMenu("PlayWithSmartAI(Beginning)")]
    public void PlayWithSmartAiBegan()
    {
        GameValue.PlayerTurn = GameValue.PlayerWillPlay.Ai;
        PlayWithSmartAI(true);
    }

    [ContextMenu("PlayWithNormalAI(Beginning)")]
    public void PlayWithNormalAiBegan()
    {
        var dialogue = Dialogue.Instance;
        if (string.IsNullOrWhiteSpace(dialogue.txtDisplay.text))
            dialogue.StartTyping("Select A player");
        GameValue.PlayerTurn = GameValue.PlayerWillPlay.Player;
        foreach (Transform ai in enemyList)
        {
            ai.GetComponent<AiPlayer>().NormalAI();
        }
    }

    public async void LockTarget(Vector3 targetPlayer, GameObject player)
    {
        if (!canAttack) return;
        if (targetPlayer == Vector3.zero) return;
        if (GameValue.BallTransform == null) return;
        var aiChild = SmartAiPosEqualToBallPos();
        if (aiChild == null) return;
        canAttack = false;
        Debug.Log("attack");
        //var canCall = true;
        if (aiChild.transform.position.x > 0)
        {
            aiChild.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
        }
        aiChild.transform.GetChild(0).GetComponent<Animator>().SetBool("Seat", true);
        aiChild.transform.GetChild(0).GetComponent<Animator>().SetBool("Throw", true);
        await Task.Delay(500);
        var endPos = UtilsClass.GetEndPosition(GameValue.BallTransform.position, targetPlayer);
        DOTween.Sequence()
            .Append(GameValue.BallTransform.DOJump(targetPlayer, tv.jumpPower, tv.numJumps,
                    tv.jumpDuration, tv.isSnapping)
                .SetEase(tv.ease))
            .AppendCallback(async () =>
            {
                if (aiChild.transform.GetChild(0).localScale.x > 0)
                {
                    aiChild.transform.GetChild(0).localScale = new Vector3(-1f, 1f, 1f);
                }
                aiChild.transform.GetChild(0).GetComponent<Animator>().SetBool("Seat", false);
                aiChild.transform.GetChild(0).GetComponent<Animator>().SetBool("Throw", false);
                if (Vector3.Distance(targetPlayer, player.transform.position) < 8)
                {
                    if (!GameValue.CanTakeInput) return;
                    GameValue.CanTakeInput = false;
                    Debug.LogWarning("die");
                    // GameValue.PlayerWin = GameValue.PlayerWillWin.Ai;
                    UiController.Uc.UpdateAiScore();
                    var ret = false;
                    UiController.Uc.OpenPanel("You Lose!", () => { ret = true; });
                    while (!ret)
                    {
                        await Task.Yield();
                    }

                    // canCall = false;
                    GameValue.GameOver = true;
                }

                if (!aiChild.transform.parent.GetChild(0).gameObject.activeSelf)
                {
                    var parent = aiChild.transform.parent;
                    SetNormalAiFromSmartAi(aiChild);
                    parent.GetChild(1).gameObject.SetActive(false);
                }
            })
            .Append(GameValue.BallTransform.DOJump(endPos, tv.jumpPower / 2, tv.numJumps * 2, 0.8f,
                    tv.isSnapping)
                .SetEase(tv.ease))
            .AppendCallback(() =>
            {
                if (GameValue.CanTakeInput)
                    CollectBallAfterAttack(aiChild);
            });
    }

    private async void CollectBallAfterAttack(GameObject previousAi)
    {
        if (!GameValue.CanTakeInput) return;
        canAttack = true;
        var newAi = SmartAiCloseToBallPos(previousAi.transform.parent.gameObject, GameValue.BallTransform.position,
            true);
        // Debug.Log(newAi.name);
        newAi = GetSmartAiFromNormalAi(newAi);
        // Debug.Log(newAi.name);
        newAi.transform.GetComponent<IMovePosition>()
            .SetMovePosition(GameValue.BallTransform.position);
        while (!(Vector3.Distance(newAi.transform.position, GameValue.BallTransform.position) < 3))
        {
            if (!GameValue.CanTakeInput) break;
            await Task.Yield();
        }

        // Debug.Log("setting");
        GameValue.BallTransform.GetComponent<BallManager>().ballPicker = newAi.transform;
    }

    public GameObject SmartAiPosEqualToBallPos()
    {
        var minDistance = Mathf.Infinity;
        GameObject child = null;
        foreach (Transform ai in enemyList)
        {
            var ballTransPosition = GameValue.BallTransform.position;
            if (ai.GetChild(0).gameObject.activeSelf)
            {
                if (Vector3.Distance(ai.GetChild(0).position, ballTransPosition) < 8f)
                {
                    if (Vector3.Distance(ai.GetChild(0).position, ballTransPosition) < minDistance)
                    {
                        minDistance = Vector3.Distance(ai.GetChild(0).position, ballTransPosition);
                        child = ai.GetChild(0).gameObject;
                    }
                }
            }

            if (ai.GetChild(1).gameObject.activeSelf)
            {
                if (Vector3.Distance(ai.GetChild(1).position, ballTransPosition) < 8f)
                {
                    if (Vector3.Distance(ai.GetChild(1).position, ballTransPosition) < minDistance)
                    {
                        minDistance = Vector3.Distance(ai.GetChild(1).position, ballTransPosition);
                        child = ai.GetChild(1).gameObject;
                    }
                }
            }
        }

        return child;
    }

    public GameObject SmartAiCloseToBallPos(GameObject preAi, Vector3 otherPosition, bool isNormal)
    {
        var minDistance = Mathf.Infinity;
        GameObject parent = null;
        foreach (Transform ai in enemyList)
        {
            if (!GameValue.CanTakeInput) break;
            if (preAi == null || ai != preAi.transform)
            {
                // var ballTransPosition = GameValue.BallTransform.position;
                if (isNormal)
                {
                    if (ai.GetChild(0).gameObject.activeSelf)
                    {
                        if (Vector3.Distance(ai.GetChild(0).position, otherPosition) < minDistance)
                        {
                            minDistance = Vector3.Distance(ai.GetChild(0).position, otherPosition);
                            parent = ai.gameObject;
                        }
                    }
                }

                if (ai.GetChild(1).gameObject.activeSelf)
                {
                    if (Vector3.Distance(ai.GetChild(1).position, otherPosition) < minDistance)
                    {
                        minDistance = Vector3.Distance(ai.GetChild(1).position, otherPosition);
                        parent = ai.gameObject;
                    }
                }
            }
        }

        return parent;
    }

    public async void PlayWithSmartAI(bool beginning)
    {
        if (GameValue.PlayerTurnCounter == 0)
        {
            foreach (Transform ai in enemyList)
            {
                ai.GetComponent<AiPlayer>().SmartAI(beginning);
            }
        }

        if (beginning)
        {
            currentAiObj = GetAiForThrowBall();
            if (currentAiObj == null)
            {
                if (!GameValue.CanTakeInput) return;
                GameValue.CanTakeInput = false;
                aiParticleHolder.SetActive(false);
                var ret = false;
                UiController.Uc.UpdatePlayerScore();
                UiController.Uc.OpenPanel("You Win!", () => { ret = true; });
                while (!ret)
                {
                    await Task.Yield();
                }

                GameValue.GameOver = true;
                return;
            }

            aiParticleHolder.SetActive(true);
            var position = aiParticleHolder.transform.position;
            var movePosition = new Vector3(position.x, position.y,
                0);
            currentAiObj.transform.GetComponent<IMovePosition>()
                .SetMovePosition(movePosition);
            while (!(Vector3.Distance(currentAiObj.transform.position, movePosition) < 2))
            {
                await Task.Yield();
            }

            if (GameValue.PlayerTurnCounter == 0)
            {
                GiveBallToAiHand?.Invoke(aiParticleHolder, currentAiObj);
            }
            else
            {
                ////////////////////////////////// ball pass by player logic//////////////////////////////////////////////////
                var dialogue = transform.GetComponent<Dialogue>();
                dialogue.StartTyping("Plz pass the ball");
                passBtnObj.SetActive(true);
            }
        }
    }

    public async void DefenceBallAi()
    {
        // if (!GameValue.IsGameStart) return;
        Debug.Log("calling");
        var randomAi = GetRandomAi();
        randomAi = GetSmartAiFromNormalAi(randomAi);
        randomAi.transform.GetComponent<IMovePosition>()
            .SetMovePosition(GameValue.BallTransform.position);

        while (!(Vector3.Distance(randomAi.transform.position, GameValue.BallTransform.position) < 3))
        {
            if (!GameValue.CanTakeInput) break;
            await Task.Yield();
        }

        GameValue.BallTransform.GetComponent<BallManager>().ballPicker = randomAi.transform;
    }

    public async void BallPassToAnotherAi(GameObject previousAi)
    {
        if (previousAi.transform.position.x > 0)
        {
            previousAi.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
        }

        previousAi.transform.GetChild(0).GetComponent<Animator>().SetBool("Seat", false);
        previousAi.transform.GetChild(0).GetComponent<Animator>().SetBool("Throw", true);

        Debug.Log("pass");
        var newAi = RandomAiExcept(previousAi.transform.parent.gameObject);
        newAi = GetSmartAiFromNormalAi(newAi);
        GameValue.BallTransform.GetComponent<BallManager>().ballPicker = newAi.transform;
        DOTween.Sequence().Append(GameValue.BallTransform.DOJump(newAi.transform.position, tv.jumpPower,
                tv.numJumps, tv.jumpDuration, tv.isSnapping))
            .AppendCallback(() =>
                {
                    SetNormalAiFromSmartAi(previousAi);
                    previousAi.transform.GetChild(0).GetComponent<Animator>().SetBool("Throw", false);
                    if (previousAi.transform.GetChild(0).position.x > 0)
                    {
                        previousAi.transform.GetChild(0).localScale = new Vector3(-1f, 1f, 1f);
                    }
                }
            );
    }

    private GameObject GetRandomAi()
    {
        return enemyList.GetChild(Random.Range(0, enemyList.childCount)).gameObject;
    }

    private GameObject RandomAiExcept(GameObject oldAi)
    {
        GameObject newAi = null;
        do
        {
            newAi = GetRandomAi();
        } while (newAi == oldAi);

        return newAi;
    }

    public void OnBallPassButton()
    {
        passBtnObj.SetActive(false);
        Debug.Log("Name;" + currentAiObj.transform.parent.name);
        var distance = Mathf.Infinity;
        GameObject player = null;
        var ballPos = GetComponent<ThrowBall>().ball.transform.position;
        foreach (Transform tempPlayer in GameValue.PlayerListTransform)
        {
            JoyStickController.jc.DeActiveJoystick();
            tempPlayer.transform.GetChild(1).gameObject.SetActive(false);
            tempPlayer.GetComponent<PlayerMovementKeys>().enabled = false;
            if (!(Vector3.Distance(tempPlayer.position, ballPos) < distance)) continue;
            player = tempPlayer.gameObject;
            distance = Vector3.Distance(tempPlayer.position, ballPos);
        }

        if (player is null) return;
        player.transform.GetChild(1).gameObject.SetActive(true);
        player.transform.GetComponent<MovePositionDirect>().enabled = true;
        player.transform.GetComponent<MovePositionDirect>().isStart = true;
        player.transform.GetComponent<IMovePosition>()
            .SetMovePosition(ballPos);
        GameValue.SelectedPlayer = player;
    }

    public IEnumerator PassBall()
    {
        GameValue.SelectedPlayer.transform.GetChild(0).GetComponent<Animator>().SetBool("Seat", true);
        yield return new WaitForSeconds(1.25f);
        GameValue.SelectedPlayer.transform.GetChild(0).GetComponent<Animator>().SetBool("Seat", false);
        JoyStickController.jc.ActiveJoystick();
        GameValue.SelectedPlayer.transform.GetComponent<PlayerMovementKeys>().enabled = true;
        Debug.Log("Name;" + currentAiObj.transform.parent.name);
        GiveBallToAiHand?.Invoke(aiParticleHolder, currentAiObj);
    }

    public async void BallPassByAiToPlayer()
    {
        var randomAi = GetRandomAi();
        randomAi = GetSmartAiFromNormalAi(randomAi);
        randomAi.transform.GetComponent<IMovePosition>()
            .SetMovePosition(GameValue.BallTransform.position);

        while (!(Vector3.Distance(randomAi.transform.position, GameValue.BallTransform.position) < 3))
        {
            await Task.Yield();
        }

        randomAi.transform.GetChild(0).GetComponent<Animator>().SetBool("Seat", true);

        randomAi.transform.GetChild(0).GetComponent<Animator>().SetBool("Throw", true);
        await Task.Delay(1000);
        randomAi.transform.GetChild(0).GetComponent<Animator>().SetBool("Seat", false);
        GameValue.BallTransform.GetComponent<BallManager>().ballPicker = randomAi.transform;
        MoveBallToPlayerThrowPoint?.Invoke(null, GameValue.SelectedPlayer);
        SetNormalAiFromSmartAi(randomAi);
    }

    private GameObject GetAiForThrowBall()
    {
        GameObject tempAi = null;
        foreach (Transform ai in enemyList)
        {
            if (ai.GetComponent<PlayerData>().playerTurnEnd) continue;
            tempAi = ai.GetChild(1).gameObject;
            ai.GetComponent<PlayerData>().playerTurnEnd = true;
            break;
        }

        return tempAi;
    }


    private GameObject GetSmartAiFromNormalAi(GameObject ai)
    {
        var normalAi = ai.transform.GetChild(0).gameObject;
        var smartAi = ai.transform.GetChild(1).gameObject;
        smartAi.transform.position = normalAi.transform.position;
        normalAi.SetActive(false);
        smartAi.SetActive(true);
        return smartAi;
    }

    public void SetAllAiNormalExcept(Transform currentAi)
    {
        foreach (Transform ai in enemyList)
        {
            if (ai == currentAi) continue;
            var smartAi = ai.GetChild(1).gameObject;
            SetNormalAiFromSmartAi(smartAi);
        }
    }

    public void SetNormalAiFromSmartAi(GameObject smartAi)
    {
        var normalAi = smartAi.transform.parent.GetChild(0).gameObject;
        normalAi.transform.position = smartAi.transform.position;
        smartAi.SetActive(false);
        normalAi.SetActive(true);
    }

    private void SetSmartAiFromNormalAi(GameObject ai)
    {
        var normalAi = ai.transform.GetChild(0).gameObject;
        var smartAi = ai.transform.GetChild(1).gameObject;
        smartAi.transform.position = normalAi.transform.position;
        normalAi.SetActive(false);
        smartAi.SetActive(true);
    }

    public void CollectDiceByRandomAi()
    {
        //yield return new WaitForSeconds(5f);
        Debug.Log("randomAiCollecting");
        AiMovetoCollectDice(GetSmartAiFromNormalAi(GetRandomAi()));
    }

    public void AiMovetoCollectDice(GameObject currentAi)
    {
        if (GameValue.CanTakeInput)
        {
            currentAi.transform.GetComponent<IMovePosition>()
                .SetMovePosition(Vector3.zero);
        }
    }
}