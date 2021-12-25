using System.Threading.Tasks;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform playerListTransform;
    public Transform enemyListTransform;
    public GameObject scriptController;

    private void OnEnable()
    {
        UiController.StartGameAgain += Start;
    }

    private void OnDisable()
    {
        UiController.StartGameAgain -= Start;
    }

    private void Start()
    {
        GlobalInit();
        StartGame();
    }

    [ContextMenu("Start Game")]
    public async void StartGame()
    {
        LocalInit();
        GameValue.GlobalLevel++;
        var ret = false;
        UiController.Uc.OpenPanel("Round " + GameValue.GlobalLevel, () => { ret = true; });
        while (!ret)
        {
            await Task.Yield();
        }

        scriptController.SetActive(true);
        playerListTransform.GetComponent<UpdateSortingLayer>().enabled = true;
        if (GameValue.GlobalLastPlayer == GameValue.PlayerWillPlay.Ai ||
            GameValue.GlobalLastPlayer == GameValue.PlayerWillPlay.None)
        {
            AiController.Instance.PlayWithNormalAiBegan();
        }
        else
        {
            AiController.Instance.PlayWithSmartAiBegan();
        }
    }

    private void GlobalInit()
    {
        GameValue.GlobalLastPlayer = PlayerPrefs.GetInt("Player_Side") == 2
            ? GameValue.PlayerWillPlay.Ai
            : GameValue.PlayerWillPlay.Player;
        GameValue.GlobalLevel = 0;
        UiController.Uc.ResetScore();
    }

    private void LocalInit()
    {
        ResetAllPlayer();
        ResetAllAi();
        GameValue.IsGameStart = false;
        GameValue.GameOver = false;
        GameValue.CanTakeInput = true;
        GameValue.BallTransform = null;
        GameValue.PlayerTurnCounter = 0;
        GameValue.PlayerListTransform = playerListTransform;
        GameValue.SelectedPlayer = null;
    }

    private async void Update()
    {
        if (!GameValue.GameOver)
        {
            if (!GameValue.IsGameStart && GameValue.CanTakeInput)
            {
                if (GameValue.PlayerTurn == GameValue.PlayerWillPlay.Player)
                {
                    if (GameValue.PlayerTurnCounter == 3)
                    {
                        GameValue.CanTakeInput = false;
                        AiController.Instance.transform.GetComponent<GameRtsController>().throwPoint.SetActive(false);
                        var ret = false;
                        UiController.Uc.UpdateAiScore();
                        UiController.Uc.OpenPanel("You Lose!", () => { ret = true; });
                        while (!ret)
                        {
                            await Task.Yield();
                        }

                        GameValue.GameOver = true;
                    }
                }
            }
        }

        if (GameValue.GameOver)
        {
            if (GameValue.PlayerTurn == GameValue.PlayerWillPlay.Ai)
            {
                GameValue.GlobalLastPlayer = GameValue.PlayerWillPlay.Ai;
            }
            else
            {
                GameValue.GlobalLastPlayer = GameValue.PlayerWillPlay.Player;
            }

            if (GameValue.BallTransform != null)
                Destroy(GameValue.BallTransform.gameObject);
            scriptController.SetActive(false);
            playerListTransform.GetComponent<UpdateSortingLayer>().enabled = false;
            LocalInit();
            await Task.Delay(1000);
            if (GameValue.GlobalLevel <= 2)
            {
                StartGame();
            }
            else
            {
                if (UiController.Uc.aiScore.childCount > UiController.Uc.playerScore.childCount)
                {
                    UiController.Uc.OpenLosePanel();
                }
                else
                {
                    UiController.Uc.OpenWinPanel();
                }
            }
        }
    }

    private void ResetAllAi()
    {
        foreach (Transform ai in enemyListTransform)
        {
            ResetAnimationAi(ai);
            var pos = ai.GetComponent<PlayerData>().initialPos;
            ai.GetChild(0).transform.position = pos;
            ai.GetChild(1).transform.position = pos;
            ai.GetComponent<PlayerData>().playerTurnEnd = false;
            ai.GetChild(0).GetComponent<MoveTransformVelocity>().SetVelocity(Vector3.zero);
            ai.GetChild(1).GetComponent<MoveTransformVelocity>().SetVelocity(Vector3.zero);
            ai.GetChild(0).gameObject.SetActive(false);
            ai.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void ResetAllPlayer()
    {
        foreach (Transform player in playerListTransform)
        {
            ResetAnimationPlayer(player);
            player.transform.GetChild(0).localScale=new Vector3(1f, 1f, 1f);
            JoyStickController.jc.DeActiveJoystick();
            player.GetChild(1).gameObject.SetActive(false);
            player.GetComponent<PlayerData>().playerTurnEnd = false;
            player.GetComponent<PlayerMovementKeys>().enabled = false;
            player.GetComponent<MoveVelocity>().SetVelocity(Vector3.zero);
            player.GetComponent<MovePositionDirect>().enabled = false;
            player.transform.position = player.GetComponent<PlayerData>().initialPos;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void ResetAnimationPlayer(Transform player)
    {
        player.GetChild(0).GetComponent<Animator>().SetFloat("Horizontal", 0f);
        player.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", 0f);
        player.GetChild(0).GetComponent<Animator>().SetBool("Stack", false);
        player.GetChild(0).GetComponent<Animator>().SetBool("Seat", false);
    }

    private void ResetAnimationAi(Transform player)
    {
        player.GetChild(0).GetChild(0).localScale=new Vector3(-1f, 1f, 1f);
        player.GetChild(1).GetChild(0).localScale=new Vector3(-1f, 1f, 1f);
        player.GetChild(0).GetChild(0).GetComponent<Animator>().SetFloat("Horizontal", 0f);
        player.GetChild(1).GetChild(0).GetComponent<Animator>().SetFloat("Horizontal", 0f);
        player.GetChild(0).GetChild(0).GetComponent<Animator>().SetFloat("Vertical", 0f);
        player.GetChild(1).GetChild(0).GetComponent<Animator>().SetFloat("Vertical", 0f);
        player.GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("Stack", false);
        player.GetChild(1).GetChild(0).GetComponent<Animator>().SetBool("Stack", false);
        player.GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("Seat", false);
        player.GetChild(1).GetChild(0).GetComponent<Animator>().SetBool("Seat", false);
        player.GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("Throw", false);
        player.GetChild(1).GetChild(0).GetComponent<Animator>().SetBool("Throw", false);
    }
}