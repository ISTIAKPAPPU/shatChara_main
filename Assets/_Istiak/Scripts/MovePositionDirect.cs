using System;
using UnityEngine;

public class MovePositionDirect : MonoBehaviour, IMovePosition
{
    private Vector3 movePosition;
    public bool isPlayer;
    public bool isStart;

    public void SetMovePosition(Vector3 movePosition)
    {
        this.movePosition = movePosition;
    }

    private void Update()
    {
        Vector3 moveDir = (movePosition - transform.position).normalized;
        if (Vector3.Distance(movePosition, transform.position) < 1f)
        {
            moveDir = Vector3.zero;
            if (isPlayer && isStart)
            {
                if (GameValue.PlayerTurn == GameValue.PlayerWillPlay.Player)
                {
                    isStart = false;
                    GameValue.SelectedPlayer.transform.GetComponent<MovePositionDirect>().enabled = false;
                    GameValue.SelectedPlayer.transform.GetComponent<Rigidbody2D>().constraints =
                        RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY |
                        RigidbodyConstraints2D.FreezeRotation;
                    GameValue.SelectedPlayer = null;
                }
                else
                {
                    isStart = false;
                    GameValue.SelectedPlayer.transform.GetComponent<MovePositionDirect>().enabled = false;
                    StartCoroutine(AiController.Instance.PassBall());
                }
            } // Stop moving when near
        }

        GetComponent<IMoveVelocity>().SetVelocity(moveDir);
    }

    private void ResetAllPlayer()
    {
        foreach (Transform player in GameValue.PlayerListTransform)
        {
            player.GetComponent<MovePositionDirect>().enabled = false;
            //GameValue.SelectedPlayer.GetComponent<SelectedPlayer>().clickable = true;
            //  GameValue.SelectedPlayer.GetComponent<PlayerMovementKeys>().enabled = true;
        }
    }
}