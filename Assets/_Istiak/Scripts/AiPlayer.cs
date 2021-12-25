using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPlayer : MonoBehaviour
{
    private enum ActivePlayer
    {
        SmartAI,
        NormalAI,
        None
    }

    public int acturacy;

    [SerializeField] private GameObject normalAi;
    [SerializeField] private GameObject smartAi;
    [SerializeField] private ActivePlayer activePlayer;

    public void NormalAI()
    {
        if (normalAi == (activePlayer == ActivePlayer.NormalAI)) return;
        normalAi.transform.position = smartAi.transform.position;
        smartAi.SetActive(false);
        normalAi.SetActive(true);
        activePlayer = ActivePlayer.NormalAI;
    }

   public void SmartAI(bool isBeginning)
   {
       if (!isBeginning)
       {
           if (smartAi == (activePlayer == ActivePlayer.SmartAI)) return;
           smartAi.transform.position = normalAi.transform.position;
           normalAi.SetActive(false);
           smartAi.SetActive(true);
           activePlayer = ActivePlayer.SmartAI;
       }
       else
       {
           smartAi.transform.position = smartAi.transform.parent.GetComponent<PlayerData>().initialPos;
           normalAi.SetActive(false);
           smartAi.SetActive(true);
           activePlayer = ActivePlayer.SmartAI;
       }
   }
}