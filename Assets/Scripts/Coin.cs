using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Toss toss;
    int rand;

    int hRound;
    int tRound;

    private void Start()
    {
        rand = PlayerPrefs.GetInt("Random");
    }

    public void Head()
    {
        hRound++;
        if (hRound >= 5)
        {
            if (rand == 0)
            {
                GetComponent<Animator>().speed = 0;
                StartCoroutine(toss.CoinToss());
            }
        }
        
    }

    public void Tail()
    {
        tRound++;
        if (tRound >= 5)
        {
            if (rand == 1)
            {
                GetComponent<Animator>().speed = 0;
                StartCoroutine(toss.CoinToss());
            }
        }
    }
}