using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
using MyGame.Utils;


namespace BallScripts
{
    public class ThrowBall : MonoBehaviour
    {
        [SerializeField] private GameObject pfBall;
        [SerializeField] private Transform ballInitialTransform;
        [SerializeField] private Transform ballThrowTransformPlayer;
        [SerializeField] private Transform ballThrowTransformAi;
        [SerializeField] private int middleYOffset = 5;
        private GameObject throwPointParticleHolder;
        [HideInInspector] public GameObject ball;
        private GameObject currentPlayer;
        public static event Action ExploreDice;
        private Vector3 ballInitialPosition;
        private Vector3 ballThrowPosition;

        private TweenVariables tv;
        private const float ReachedPathPositionDistance = 1f;

        private void OnEnable()
        {
            tv = GetComponent<TweenVariables>();
            ballInitialPosition = ballInitialTransform.position;
            Filler.ThrowBall += OnBallThrow;
            ThrowPointCollisionDetect.GiveBallToPlayerHand += InstantiateBall;
            AiController.GiveBallToAiHand += InstantiateBall;
            AiController.MoveBallToPlayerThrowPoint += InstantiateBall;
        }

        private void OnDisable()
        {
            Filler.ThrowBall -= OnBallThrow;
            ThrowPointCollisionDetect.GiveBallToPlayerHand -= InstantiateBall;
            AiController.GiveBallToAiHand -= InstantiateBall;
            AiController.MoveBallToPlayerThrowPoint -= InstantiateBall;
        }

        private void InstantiateBall(GameObject throwPointParticle, GameObject player)
        {
            Debug.Log("instantiate");
            if (player != null)
                currentPlayer = player;
            if (throwPointParticle != null)
                throwPointParticleHolder = throwPointParticle;
            ballThrowPosition = currentPlayer.CompareTag("Player")
                ? ballThrowTransformPlayer.position
                : ballThrowTransformAi.position;
            if (GameValue.PlayerTurnCounter == 0)
            {
                ball = Instantiate(pfBall, ballInitialPosition, Quaternion.identity);
                GameValue.BallTransform = ball.transform;
            }

            ball.transform.GetComponent<BallManager>().ballPicker = currentPlayer.transform;
            MoveBallToThrowPoint(currentPlayer.transform.position);
        }

        private void MoveBallToThrowPoint(Vector3 throwPoint)
        {
            DOTween.Sequence()
                .Append(ball.transform.DOJump(throwPoint, tv.jumpPower, tv.numJumps * 2,
                        tv.jumpDuration, tv.isSnapping)
                    .SetEase(tv.ease))
                .AppendCallback(Callback);
        }

        private async void Callback()
        {
            throwPointParticleHolder.SetActive(false);
            if (currentPlayer.CompareTag("Player") && currentPlayer.transform.GetChild(1).gameObject.activeSelf)
            {
                FillerController.Fb.ActiveFillBar();
            }
            else
            {
                await Task.Delay(1000);
                OnBallThrow(currentPlayer.transform.parent.GetComponent<AiPlayer>().acturacy, false);
            }

            // if (currentPlayer.CompareTag("Player")) return;
            // foreach (Transform player in GameValue.PlayerListTransform)
            // {
            //     player.GetComponent<SelectedPlayer>().clickable = true;
            // }
        }

        private void OnBallThrow(int accuracy, bool isPlayer)
        {
            Debug.Log(accuracy);
            middleYOffset = accuracy;
            BallMove(isPlayer);
        }

        private async void BallMove(bool isPlayer)
        {
            var middlePos = GetRandomMiddlePosition();
            var endPos = UtilsClass.GetEndPosition(ballThrowPosition, middlePos);
            if (currentPlayer.CompareTag("Enemy") && !isPlayer)
            {
                currentPlayer.transform.GetChild(0).GetComponent<Animator>().SetBool("Seat", true);

                currentPlayer.transform.GetChild(0).GetComponent<Animator>().SetBool("Throw", true);
                await Task.Delay(1000);
                currentPlayer.transform.GetChild(0).GetComponent<Animator>().SetBool("Seat", false);
            }

            ballThrowPosition = endPos;
            DOTween.Sequence()
                .Append(ball.transform.DOJump(middlePos, tv.jumpPower, tv.numJumps,
                        tv.jumpDuration, tv.isSnapping)
                    .SetEase(tv.ease))
                .AppendCallback(async () =>
                {
                    if (currentPlayer.CompareTag("Enemy"))
                    {
                        currentPlayer.transform.GetChild(0).GetComponent<Animator>().SetBool("Seat", false);
                        currentPlayer.transform.GetChild(0).GetComponent<Animator>().SetBool("Throw", false);
                    }


                    if (!GameValue.IsGameStart)
                    {
                        if (!(Vector3.Distance(middlePos, Vector3.zero) < ReachedPathPositionDistance)) return;

                        ExploreDice?.Invoke();
                    }
                    else
                    {
                        var targetAi = AiController.Instance.SmartAiCloseToBallPos(null, Vector3.zero, false);
                        Debug.Log("targetAI");
                        if (targetAi == null) return;
                        Debug.Log(">5<9");
                        if (middlePos != Vector3.zero) return;
                        Debug.Log("middle point:" + middlePos);
                        Debug.Log("targetAi.transform.position:" + targetAi.transform.GetChild(1).position);
                        Debug.Log("distance:" + Vector3.Distance(targetAi.transform.GetChild(1).position, middlePos));
                        if (!(Vector3.Distance(targetAi.transform.GetChild(1).position, middlePos) <
                              8)) return;
                        //playerWin
                        Debug.Log("CanTakeInput");
                        if (!GameValue.CanTakeInput) return;
                        GameValue.CanTakeInput = false;
                        var ret = false;
                        UiController.Uc.UpdatePlayerScore();
                        UiController.Uc.OpenPanel("You Win!", () => { ret = true; });
                        while (!ret)
                        {
                            await Task.Yield();
                        }

                        GameValue.GameOver = true;
                    }
                })
                .Append(ball.transform.DOJump(endPos, tv.jumpPower / 2, tv.numJumps * 2,
                        0.8f, tv.isSnapping)
                    .SetEase(tv.ease))
                .AppendCallback(TweenCallback
                );

            async void TweenCallback()
            {
                if (GameValue.IsGameStart) return;

                if (Vector3.Distance(middlePos, Vector3.zero) < ReachedPathPositionDistance)
                {
                    if (currentPlayer.CompareTag("Enemy"))
                    {
                        AiController.Instance.SetAllAiNormalExcept(currentPlayer.transform.parent);
                        AiController.Instance.AiMovetoCollectDice(currentPlayer);
                    }

                    if (GameValue.PlayerTurn == GameValue.PlayerWillPlay.Player)
                    {
                        JoyStickController.jc.ActiveJoystick();
                        GameValue.SelectedPlayer.transform.GetComponent<PlayerMovementKeys>().enabled = true;
                        AiController.Instance.DefenceBallAi();
                    }

                    GameValue.IsGameStart = true;
                    return;
                }

                GameValue.PlayerTurnCounter++;
                if (currentPlayer.CompareTag("Player"))
                {
                    var dialogue = Dialogue.Instance;
                    if (string.IsNullOrWhiteSpace(dialogue.txtDisplay.text))
                        dialogue.StartTyping("Go To Check Point");
                    currentPlayer.transform.GetComponent<PlayerData>().playerTurnEnd = true;
                    currentPlayer.transform.GetComponent<PlayerMovementKeys>().enabled = false;
                    currentPlayer.transform.GetComponent<MovePositionDirect>().enabled = true;
                    currentPlayer.transform.GetComponent<MovePositionDirect>().isStart = true;
                    currentPlayer.transform.GetComponent<IMovePosition>()
                        .SetMovePosition(currentPlayer.transform.GetComponent<PlayerData>().initialPos);
                    //JoyStickController.jc.DeActiveJoystick();
                    currentPlayer.transform.GetChild(1).gameObject.SetActive(false);
                    await Task.Delay(500);
                    throwPointParticleHolder.SetActive(true);
                }
                else
                {
                    var parent = currentPlayer.transform.parent;
                    parent.GetComponent<PlayerData>().playerTurnEnd = true;
                    currentPlayer.transform.GetComponent<IMovePosition>()
                        .SetMovePosition(parent.GetComponent<PlayerData>().initialPos);
                    await Task.Delay(500);
                    AiController.Instance.PlayWithSmartAI(true);
                }
            }
        }

        private Vector3 GetRandomMiddlePosition()
        {
            var yPoint = -1;
            if (GameValue.IsGameStart)
            {
                if (GameValue.PlayerTurn == GameValue.PlayerWillPlay.Ai)
                {
                    //player throw
                    if (middleYOffset > 5 && middleYOffset < 9)
                    {
                        yPoint = 0;
                    }
                    else if (middleYOffset <= 5)
                    {
                        yPoint = Random.Range(-5, -10);
                    }
                    else if (middleYOffset >= 9)
                    {
                        yPoint = Random.Range(5, 10);
                    }
                }
                else
                {
                    //Ai throw
                    yPoint = Random.Range(-middleYOffset, middleYOffset);
                }
            }
            else
            {
                if (GameValue.PlayerTurn == GameValue.PlayerWillPlay.Ai)
                {
                    //Ai throw
                    yPoint = Random.Range(-middleYOffset, middleYOffset);
                }
                else
                {
                    //player throw
                    if (middleYOffset > 5 && middleYOffset < 9)
                    {
                        yPoint = 0;
                    }
                    else if (middleYOffset <= 5)
                    {
                        yPoint = Random.Range(-5, -10);
                    }
                    else if (middleYOffset >= 9)
                    {
                        yPoint = Random.Range(5, 10);
                    }
                }
            }

            Debug.Log("ypoint:" + yPoint);
            return new Vector3(0, yPoint, 0);
        }
    }
}