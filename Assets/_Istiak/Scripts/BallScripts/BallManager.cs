using System;
using UnityEngine;

namespace BallScripts
{
    public class BallManager : MonoBehaviour
    {
        public Transform ballPicker;
        [SerializeField] private float aiPassTimeRemaining = 3f;

        [SerializeField] private GameObject canPassAi;

        private float initPassRemainingTimeAi;

        private void OnEnable()
        {
            canPassAi = null;
            initPassRemainingTimeAi = aiPassTimeRemaining;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!GameValue.CanTakeInput) return;
            if (!GameValue.IsGameStart) return;
            if (other.CompareTag("Enemy") && GameValue.PlayerTurn == GameValue.PlayerWillPlay.Player)
            {
                if (canPassAi == null)
                {
                    //other.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Throw", false);
                    
                    other.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Seat", true);
                    canPassAi = other.gameObject;
                }

                return;
            }

            if (other.CompareTag("Player") && GameValue.PlayerTurn != GameValue.PlayerWillPlay.Ai) return;
            if (other.CompareTag("Player") && other.gameObject.transform.GetChild(1).gameObject.activeSelf)
                FillerController.Fb.ActiveFillBar();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!GameValue.CanTakeInput) return;
            if (!GameValue.IsGameStart) return;
            if (other.CompareTag("Enemy")) return;
            if (other.CompareTag("Player") && GameValue.PlayerTurn != GameValue.PlayerWillPlay.Ai) return;
            if (other.CompareTag("Player") && other.gameObject.transform.GetChild(1).gameObject.activeSelf)
                FillerController.Fb.ActiveFillBar();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            //Debug.Log("Enter");
            if (!GameValue.CanTakeInput) return;
            if (!GameValue.IsGameStart) return;
            if (other.CompareTag("Enemy") && GameValue.PlayerTurn == GameValue.PlayerWillPlay.Player)
            {
                // if (!AiController.Instance.SmartAiPosEqualToBallPos())
                // {
                    canPassAi = null;
                    return;
              //  }
            }

            if (other.CompareTag("Player") && GameValue.PlayerTurn != GameValue.PlayerWillPlay.Ai) return;
            if (other.CompareTag("Player"))
                FillerController.Fb.DeActiveFillBar();
        }

        private void Update()
        {
            if (!GameValue.IsGameStart) return;
            if (!GameValue.CanTakeInput || !canPassAi || !AiController.Instance.canAttack)
            {
                aiPassTimeRemaining = initPassRemainingTimeAi;
                return;
            }

            if (aiPassTimeRemaining > 0)
            {
                aiPassTimeRemaining -= Time.deltaTime;
            }
            else
            {
                aiPassTimeRemaining = initPassRemainingTimeAi;
                if (canPassAi != null)
                {
                    aiPassTimeRemaining = initPassRemainingTimeAi;
                    var ai = canPassAi;
                    canPassAi = null;
                    AiController.Instance.BallPassToAnotherAi(ai);
                }
            }
        }
    }
}