using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class BallOut : MonoBehaviour
{
    private async void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameValue.CanTakeInput) return;
        if (!GameValue.IsGameStart) return;
        if (other.CompareTag("Player")) return;
        if (other.CompareTag("Enemy")) return;
        if (other.CompareTag("Wall"))
        {
            GameValue.CanTakeInput = false;

            var ret = false;
            if (GameValue.PlayerTurn == GameValue.PlayerWillPlay.Player)
            {
                UiController.Uc.UpdatePlayerScore();
                UiController.Uc.OpenPanel("Out! You Win!", () => { ret = true; });
                while (!ret)
                {
                    await Task.Yield();
                }
            }
            else
            {
                UiController.Uc.UpdateAiScore();
                UiController.Uc.OpenPanel("Out! You Lose!", () => { ret = true; });
                while (!ret)
                {
                    await Task.Yield();
                }
            }

            Debug.Log("Win");
            GameValue.GameOver = true;
            FillerController.Fb.DeActiveFillBar();
        }
    }
}