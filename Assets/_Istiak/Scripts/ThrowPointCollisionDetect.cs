using System;
using UnityEngine;


public class ThrowPointCollisionDetect : MonoBehaviour
{
    public static event Action<GameObject, GameObject> GiveBallToPlayerHand;
    [SerializeField] private GameObject particleHolder;
    private bool clickable;

    private void OnEnable()
    {
        clickable = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (clickable)
        {
            clickable = false;
            JoyStickController.jc.DeActiveJoystick();
            GameValue.SelectedPlayer.GetComponent<MoveVelocity>().SetVelocity(Vector3.zero);
            GameValue.SelectedPlayer.GetComponent<PlayerMovementKeys>().enabled = false;
            if (GameValue.PlayerTurnCounter == 0)
            {
                GiveBallToPlayerHand?.Invoke(particleHolder, GameValue.SelectedPlayer);
            }
            else
            {
                Debug.Log("go ball");
                AiController.Instance.BallPassByAiToPlayer();
            }
        }
    }
}