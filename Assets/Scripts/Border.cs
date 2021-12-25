using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public LevelController lvController;

    int toss;

    // Start is called before the first frame update
    void Start()
    {
        toss = PlayerPrefs.GetInt("Player_Side");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ball")
        {
            int towerState = PlayerPrefs.GetInt("Crushed");
            if (towerState == 1)
            {
                if (toss == 1)
                {
                    LevelController lvController = GameObject.FindObjectOfType<LevelController>();
                    lvController.Loss();
                    enabled = false;
                }
                if (toss == 2)
                {
                    LevelController lvController = GameObject.FindObjectOfType<LevelController>();
                    lvController.Win();
                    enabled = false;
                }
            }
            else
            {
                if (toss == 1)
                {
                    int activePlayerIndex = PlayerPrefs.GetInt("Active_Player_Index");
                    LevelController lvController = GameObject.FindObjectOfType<LevelController>();

                    if (activePlayerIndex == 0)
                    {
                        Destroy(GameObject.FindGameObjectWithTag("Ball"));
                        lvController.Next(0, 1);
                        enabled = false;
                    }
                    if (activePlayerIndex == 1)
                    {
                        Destroy(GameObject.FindGameObjectWithTag("Ball"));
                        lvController.Next(1, 2);
                        enabled = false;
                    }
                    else if (activePlayerIndex == 2)
                    {
                        Debug.Log("B");
                        lvController.Win();
                        enabled = false;
                    }
                }

                if (toss == 2)
                {
                    int activePlayerIndex = PlayerPrefs.GetInt("Active_Player_Index");
                    LevelController lvController = GameObject.FindObjectOfType<LevelController>();

                    if (activePlayerIndex == 0)
                    {
                        int health = PlayerPrefs.GetInt("Health");
                        if (health > 0)
                        {
                            PlayerPrefs.SetInt("Health", health - 1);
                        }
                        Destroy(GameObject.FindGameObjectWithTag("Ball"));
                        lvController.Next(0, 1);
                        enabled = false;
                    }
                    if (activePlayerIndex == 1)
                    {
                        int health = PlayerPrefs.GetInt("Health");
                        if (health > 0)
                        {
                            PlayerPrefs.SetInt("Health", health - 1);
                        }
                        Destroy(GameObject.FindGameObjectWithTag("Ball"));
                        lvController.Next(1, 2);
                        enabled = false;
                    }
                    else if (activePlayerIndex == 2)
                    {
                        Debug.Log("B");
                        lvController.Loss();
                        enabled = false;
                    }
                }
            }
        }
    }
}
