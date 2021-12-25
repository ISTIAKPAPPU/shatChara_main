using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DiceScripts
{
    public class DiceSetter : MonoBehaviour
    {
        [SerializeField] private Transform allDices;
        [SerializeField] private GameObject diceCounterObj;
        [SerializeField] private float timeRemaining = 3f;
        [SerializeField] private bool startTimer = false;
        [SerializeField] private float diceMoveDuration = 0.8f;
        [SerializeField] private float counterObjMoveOffset = 20f;
        private float initRemainingTime;
        private Vector3 initDiceCounterPos;
        private List<GameObject> playerCounter;

        private GameObject currentAi;
        //private bool canGo;

        private void Start()
        {
            //canGo = true;
            playerCounter = new List<GameObject>();
            currentAi = null;
            initRemainingTime = timeRemaining;
            initDiceCounterPos = diceCounterObj.transform.position;
        }

        private void ResetAllDice()
        {
            // canGo = true;
            startTimer = false;
            playerCounter.Clear();
            // aiCounter.Clear();
            currentAi = null;
            timeRemaining = initRemainingTime;
            foreach (Transform dice in allDices)
            {
                dice.position = dice.GetComponent<DiceData>().initDicePos;
            }
        }

        private bool IsDiceCanCollect()
        {
            return allDices.Cast<Transform>().Any(dice => dice.GetComponent<DiceData>().initDicePos != dice.position);
        }

        private void OnCollisionStay2D(Collision2D otherTr)
        {
            if (!GameValue.CanTakeInput) return;
            var other = otherTr.transform.gameObject;
            if (other.CompareTag("Ball")) return;
            if (other.CompareTag("Player") && GameValue.PlayerTurn != GameValue.PlayerWillPlay.Player) return;
            if (other.CompareTag("Enemy")) return;
            if (!IsDiceCanCollect()) return;
            if (other.CompareTag("Player"))
            {
                other.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Stack", true);
            }
        }

        private void OnCollisionEnter2D(Collision2D otherTr)
        {
            if (!GameValue.CanTakeInput) return;
            var other = otherTr.transform.gameObject;
            if (other.CompareTag("Ball")) return;
            if (other.CompareTag("Player") && GameValue.PlayerTurn != GameValue.PlayerWillPlay.Player) return;
            if (other.CompareTag("Enemy") && GameValue.PlayerTurn != GameValue.PlayerWillPlay.Ai) return;
            //if (GameValue.IsLevelComplete) return;
            if (!IsDiceCanCollect()) return;
            if (other.CompareTag("Player"))
            {
                playerCounter.Add(other.gameObject);
            }
            else
            {
                // aiCounter.Add(other.gameObject);
                currentAi = other.gameObject;
                currentAi.transform.GetChild(0).GetComponent<Animator>().SetBool("Stack", true);
            }

            timeRemaining = initRemainingTime;
            startTimer = true;
            // Debug.Log("playerEnter");
        }

        private void OnCollisionExit2D(Collision2D otherTr)
        {
            if (!GameValue.CanTakeInput) return;
            var other = otherTr.transform.gameObject;
            if (other.CompareTag("Ball")) return;
            if (other.CompareTag("Player") && GameValue.PlayerTurn != GameValue.PlayerWillPlay.Player) return;
            if (other.CompareTag("Enemy") && GameValue.PlayerTurn != GameValue.PlayerWillPlay.Ai) return;
            if (!IsDiceCanCollect()) return;
            if (other.CompareTag("Player"))
            {
                playerCounter.Remove(other.gameObject);
                if (playerCounter.Count > 0) return;
                playerCounter.Clear();
                timeRemaining = initRemainingTime;
                startTimer = false;
            }
            else
            {
                other.transform.GetChild(0).GetComponent<Animator>().SetBool("Stack", false);
                currentAi = null;
                timeRemaining = initRemainingTime;
                startTimer = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!GameValue.CanTakeInput) return;
            if (other.CompareTag("Ball")) return;
            if (other.CompareTag("Player") && GameValue.PlayerTurn != GameValue.PlayerWillPlay.Player) return;
            if (other.CompareTag("Enemy") && GameValue.PlayerTurn != GameValue.PlayerWillPlay.Ai) return;
            //if (GameValue.IsLevelComplete) return;
            if (!IsDiceCanCollect()) return;
            if (other.CompareTag("Player"))
            {
                playerCounter.Add(other.gameObject);
            }
            else
            {
                // aiCounter.Add(other.gameObject);
                currentAi = other.gameObject;
                Debug.Log("name       :" + currentAi.transform.GetChild(0).name);
                currentAi.transform.GetChild(0).GetComponent<Animator>().SetBool("Stack",true);
               // currentAi.transform.GetChild(0).GetComponent<Animator>().SetBool("Stack", true);
            }

            timeRemaining = initRemainingTime;
            startTimer = true;
            // Debug.Log("playerEnter");
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!GameValue.CanTakeInput) return;
            if (other.CompareTag("Ball")) return;
            if (other.CompareTag("Player") && GameValue.PlayerTurn != GameValue.PlayerWillPlay.Player) return;
            if (other.CompareTag("Enemy") && GameValue.PlayerTurn != GameValue.PlayerWillPlay.Ai) return;
            if (!IsDiceCanCollect()) return;
            if (other.CompareTag("Player"))
            {
                playerCounter.Remove(other.gameObject);
                if (playerCounter.Count > 0) return;
                playerCounter.Clear();
                timeRemaining = initRemainingTime;
                startTimer = false;
            }
            else
            {
                other.transform.GetChild(0).GetComponent<Animator>().SetBool("Stack", false);
                currentAi = null;
                timeRemaining = initRemainingTime;
                startTimer = false;
            }
        }

        private async void Update()
        {
            if (GameValue.GameOver || !GameValue.IsGameStart)
            {
                ResetAllDice();
                return;
            }

            if (GameValue.PlayerTurn == GameValue.PlayerWillPlay.Player)
            {
                if (playerCounter.Count <= 0)
                {
                    AiController.Instance.LockTarget(Vector3.zero, null);
                }
                else
                {
                    AiController.Instance.LockTarget(playerCounter[0].transform.position, playerCounter[0]);
                }
            }

            if (!GameValue.CanTakeInput) return;
            if (!startTimer) return;
            if (timeRemaining > 0)
            {
                var dialogue = Dialogue.Instance;
                if (string.IsNullOrWhiteSpace(dialogue.txtDisplay.text))
                    dialogue.StartTyping("Collecting...");
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                var diceCollected = ResetDicePosition();
                Debug.Log("end" + diceCollected);
                if (diceCollected == 7)
                {
                    if (!GameValue.CanTakeInput)
                    {
                        return;
                    }

                    GameValue.CanTakeInput = false;

                    var ret = false;
                    if (GameValue.PlayerTurn == GameValue.PlayerWillPlay.Player)
                    {
                        UiController.Uc.UpdatePlayerScore();
                        UiController.Uc.OpenPanel("You Win!", () => { ret = true; });
                        while (!ret)
                        {
                            await Task.Yield();
                        }
                    }
                    else
                    {
                        UiController.Uc.UpdateAiScore();
                        UiController.Uc.OpenPanel("You Lose!", () => { ret = true; });
                        while (!ret)
                        {
                            await Task.Yield();
                        }
                    }

                    Debug.Log("Win");
                    GameValue.GameOver = true;
                    FillerController.Fb.DeActiveFillBar();
                    return;
                }

                diceCounterObj.transform.position = initDiceCounterPos;
                diceCounterObj.SetActive(true);
                diceCounterObj.transform.GetComponent<TextMeshProUGUI>().text = $"+ {diceCollected}";
                DOTween.Sequence()
                    .Append(diceCounterObj.transform.DOMoveY(diceCounterObj.transform.position.y + counterObjMoveOffset,
                        diceMoveDuration, false))
                    .AppendCallback(() => { diceCounterObj.SetActive(false); });
                timeRemaining = initRemainingTime;
                if (GameValue.PlayerTurn == GameValue.PlayerWillPlay.Ai)
                {
                    Debug.Log("currentAi:" + currentAi.transform.parent.name);
                    if (currentAi != null)
                    {
                        var ai = currentAi;
                        ai.transform.GetComponent<IMovePosition>()
                            .SetMovePosition(ai.transform.parent.GetComponent<PlayerData>().initialPos);
                        // while (!(Vector3.Distance(ai.transform.position,
                        //     ai.transform.parent.GetComponent<PlayerData>().initialPos) < 4))
                        // {
                        //     await Task.Yield();
                        // }
                        await Task.Delay(1000);
                        Debug.Log("set to normal");
                        AiController.Instance.SetNormalAiFromSmartAi(ai);
                    }

                    AiController.Instance.CollectDiceByRandomAi();
                }
            }
        }

        private int ResetDicePosition()
        {
            var tempDice = allDices.Cast<Transform>()
                .FirstOrDefault(dice => dice.position != dice.GetComponent<DiceData>().initDicePos);
            if (tempDice == null)
            {
                startTimer = false;
                //GameValue.IsLevelComplete = true;
                return 0;
            }

            tempDice.DOMove(tempDice.GetComponent<DiceData>().initDicePos, diceMoveDuration, false);
            return tempDice.GetSiblingIndex() + 1;
        }
    }
}