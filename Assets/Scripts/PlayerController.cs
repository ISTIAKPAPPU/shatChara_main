using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject tempBall;
    public GameObject arrow;

    public int toss;

    private void Start()
    {
        toss = PlayerPrefs.GetInt("Player_Side");
        tempBall.SetActive(true);
        if (toss == 2)
        {
            arrow.SetActive(true);
        }
    }

    private void Update()
    {
        if (toss == 1)
        {
            GetComponent<PlayerTouchController>().enabled = true;
            toss = 3;
        }
    }

    private void OnMouseDown()
    {
        if (!enabled) return; // if script disable dont execute the function

        // Toggle the PlayerTouchController Script
        if (GetComponent<PlayerTouchController>().enabled == true)
        {
            GetComponent<PlayerTouchController>().enabled = false;
        }
        else
        {
            GetComponent<PlayerTouchController>().enabled = true;
        }
    }
}
