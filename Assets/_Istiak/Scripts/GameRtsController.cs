using UnityEngine;

public class GameRtsController : MonoBehaviour
{
    public GameObject throwPoint;

    private void OnEnable()
    {
        SelectedPlayer.PlayerSelected += OnSelectedPlayer;
    }

    private void OnDisable()
    {
        SelectedPlayer.PlayerSelected -= OnSelectedPlayer;
    }

    private void OnSelectedPlayer(GameObject playerObj)
    {
        Debug.Log("1");
        if (!GameValue.CanTakeInput) return;
        if (FillerController.Fb.fillBar.activeSelf) return;
        if (!GameValue.IsGameStart)
        {
            if (!playerObj.transform.GetChild(1).gameObject.activeSelf &&
                GameValue.PlayerTurn == GameValue.PlayerWillPlay.Player)
            {
                throwPoint.SetActive(true);
            }

            Debug.Log("SingleSelection");

            if (!playerObj.transform.GetComponent<PlayerData>().playerTurnEnd)
            {
                if (GameValue.SelectedPlayer != null)
                {
                    GameValue.SelectedPlayer.transform.GetChild(1).gameObject.SetActive(false);
                    GameValue.SelectedPlayer.transform.GetComponent<PlayerMovementKeys>().enabled = false;
                }

                JoyStickController.jc.ActiveJoystick();
                GameValue.SelectedPlayer = playerObj;
                GameValue.SelectedPlayer.transform.GetChild(1).gameObject.SetActive(true);
                playerObj.GetComponent<PlayerMovementKeys>().enabled = true;
            }
            else
            {
                var dialogue = Dialogue.Instance;
                if (string.IsNullOrWhiteSpace(dialogue.txtDisplay.text))
                    dialogue.StartTyping("Player Turn ended Select Another Player");
            }
        }
        else
        {
            if (GameValue.SelectedPlayer != null)
            {
                GameValue.SelectedPlayer.transform.GetChild(1).gameObject.SetActive(false);
                GameValue.SelectedPlayer.transform.GetComponent<PlayerMovementKeys>().enabled = false;
            }

            JoyStickController.jc.ActiveJoystick();
            GameValue.SelectedPlayer = playerObj;
            GameValue.SelectedPlayer.transform.GetChild(1).gameObject.SetActive(true);
            playerObj.GetComponent<PlayerMovementKeys>().enabled = true;
        }
    }
}