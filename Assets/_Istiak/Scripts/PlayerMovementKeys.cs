using System;
using UnityEngine;

public class PlayerMovementKeys : MonoBehaviour
{
    public MovementJoystick movementJoystick;
    public Vector3 moveVector;

    private void Update()
    {
        if (GameValue.GameOver) return;
        if (!GameValue.CanTakeInput) return;
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W)) moveY = +1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = +1f;
        if (movementJoystick.joystickVec.y != 0)
        {
            moveY = movementJoystick.joystickVec.y;
            moveX = movementJoystick.joystickVec.x;
            if (GameValue.SelectedPlayer.transform.GetChild(0).localScale.x < 0)
            {
                GameValue.SelectedPlayer.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
            if (GameValue.SelectedPlayer.transform.GetChild(0).position.x < 0)
            {
                GameValue.SelectedPlayer.transform.GetChild(0).localScale = new Vector3(-1f, 1f, 1f);
            }
        }

        moveVector = new Vector3(moveX, moveY).normalized;
        GetComponent<IMoveVelocity>().SetVelocity(moveVector);
    }
    
}