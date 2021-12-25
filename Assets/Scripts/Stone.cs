using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public int minHitForce;
    public int points;

    int score;

    Ball ball;
    Vector3 originalPos;
    
    void Start()
    {

        PlayerPrefs.SetInt("Score", 0);
        originalPos = new Vector3(-3.75f, 1.615f, 0f);
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        int towerState = PlayerPrefs.GetInt("Crushed");
        if(towerState == 1)
        {
            PlayerPrefs.SetInt("Stone_Moved", 1);
            transform.localPosition = originalPos;
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Point" && gameObject.tag == "Stones")
        {
            score += points;
            PlayerPrefs.SetInt("Score", score);
            Debug.Log("Score = " + score);
        }

        if (other.tag == "Ball" && gameObject.name == "StoneBorder")
        {
            int ballPower = PlayerPrefs.GetInt("Ball_Force");
            
            if (ballPower > minHitForce)
            {
                score += 20;
            }
        }
    }
}
