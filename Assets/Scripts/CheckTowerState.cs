using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTowerState : MonoBehaviour
{
    public GameObject gameWin;
    public GameObject[] stones;
    public GameObject platform;
    public GameObject crushedMsg;
    
    public float speed;
    Camera cam;

    float wait;
    int sCount;
    public static int stoneCount;
    int toss;

    private void Start()
    {
        cam = Camera.main;
        PlayerPrefs.SetFloat("WaitTime", 0);
        toss = PlayerPrefs.GetInt("Player_Side");
    }

    void Update()
    {
        int towerState = PlayerPrefs.GetInt("Crushed");
        int stoneMoved = PlayerPrefs.GetInt("Stone_Moved");

        if(towerState == 1)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 2.5f, speed);
            cam.transform.localScale = Vector3.Lerp(cam.transform.localScale, platform.transform.localPosition, speed);
        }
        else if(towerState == 1 && stoneCount == 7)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5, speed);
            cam.transform.localScale = Vector3.Lerp(cam.transform.localScale, new Vector3(0f, 0f, -10f), speed);
        }

        if (towerState == 1 && stoneMoved == 1)
        {
            GameObject[] stone = GameObject.FindGameObjectsWithTag("Stones");
            for(int i = 0; i < stone.Length; i++)
            {
                if(stone[i].transform.localPosition.x <= -3.3f && stone[i].transform.localPosition.x >= -4f)
                {
                    stoneCount++;
                }
            }
        }
        if(stoneCount == 7)
        {
            Time.timeScale = 0;
            gameWin.SetActive(true);
        }

        if (towerState == 2)
        {
            GameObject[] stone = GameObject.FindGameObjectsWithTag("Stones");
            for (int i = 0; i < stone.Length; i++)
            {
                if (stone[i].transform.localPosition.x <= -3f && stone[i].transform.localPosition.x >= -4.45f)
                {
                    sCount++;
                }
            }

            if (sCount > 3)
            {
                wait += Time.deltaTime;
                if (Time.deltaTime > 5)
                {
                    if (toss == 1)
                    {
                        int activePlayerIndex = PlayerPrefs.GetInt("Active_Player_Index");
                        LevelController lvController = GameObject.FindObjectOfType<LevelController>();

                        if (activePlayerIndex == 0)
                        {
                            wait = 0;
                            Destroy(GameObject.FindGameObjectWithTag("Ball"));
                            lvController.Next(0, 1);
                        }
                        if (activePlayerIndex == 1)
                        {
                            wait = 0;
                            Destroy(GameObject.FindGameObjectWithTag("Ball"));
                            lvController.Next(1, 2);
                        }
                        else if(activePlayerIndex == 2)
                        {
                            lvController.Win();
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
                            wait = 0;
                            Destroy(GameObject.FindGameObjectWithTag("Ball"));
                            lvController.Next(0, 1);
                        }
                        if (activePlayerIndex == 1)
                        {
                            int health = PlayerPrefs.GetInt("Health");
                            if (health > 0)
                            {
                                PlayerPrefs.SetInt("Health", health - 1);
                            }
                            wait = 0;
                            Destroy(GameObject.FindGameObjectWithTag("Ball"));
                            lvController.Next(1, 2);
                        }
                        else if (activePlayerIndex == 2)
                        {
                            Debug.Log("TS");
                            lvController.Loss();
                        }
                    }
                }
            }
            if (sCount < 4)
            {
                PlayerPrefs.SetFloat("WaitTime", 30);
                PlayerPrefs.SetInt("Crushed", 1);
                int pLevel = PlayerPrefs.GetInt("Player_Level");
                if (pLevel == 0)
                {
                    Crushed();
                }
            }
        }
    }

    public void Crushed()
    {
        crushedMsg.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnClickOk()
    {
        crushedMsg.SetActive(false);
        Time.timeScale = 1;
    }
}
