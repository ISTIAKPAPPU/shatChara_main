using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject loss;
    public Text timerText;
    public float time;
    private int t;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= 0)
        {
            float addTime = 0f;
            if (PlayerPrefs.GetFloat("WaitTime") > 0)
            {
                addTime = PlayerPrefs.GetFloat("WaitTime");
                print("Vitore");
            }

            time = time - 1 * Time.deltaTime;
            t = (int) (time + (addTime));
            timerText.text = t.ToString() + " s";
           // print("Time show");
        }

        if (time < 0 && CheckTowerState.stoneCount < 7)
        {
            print("Time Minus");
            loss.SetActive(true);
            Time.timeScale = 0;
        }

        //else
        // {
        //    Time.timeScale = 0;
        //     loss.SetActive(true);
        // }
    }
}