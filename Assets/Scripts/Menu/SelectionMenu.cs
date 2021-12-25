using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour
{
    public GameObject player1;
    public GameObject player1Border;
    public GameObject player1Arrow;
    public GameObject player2;
    public GameObject player2Border;
    public GameObject player2Arrow;

    public GameObject[] lockIcon;
    public GameObject[] ball;

    int lockedBall;
    int lockedBall2;
    int lockedBall3;

    // Start is called before the first frame update
    void Start()
    {
        int selectedPlayer = PlayerPrefs.GetInt("Selected_Player");
        if (selectedPlayer == 1)
        {
            player1.GetComponent<Image>().color = new Color32(58, 58, 58, 150);
            player1Border.SetActive(true);
            player1Arrow.SetActive(true);
        }
        else if (selectedPlayer == 2)
        {
            player2.GetComponent<Image>().color = new Color32(58, 58, 58, 150);
            player2Border.SetActive(true);
            player2Arrow.SetActive(true);
        }

        int selectedBall = PlayerPrefs.GetInt("Selected_Ball");
        for (int i = 0; i < ball.Length; i++)
        {
            ball[i].GetComponent<Image>().color = new Color32(255, 255, 225, 0);
        }
        ball[selectedBall].GetComponent<Image>().color = new Color32(255, 255, 225, 150);

        lockedBall = PlayerPrefs.GetInt("Ball2");
        lockedBall2 = PlayerPrefs.GetInt("Ball3");
        lockedBall3 = PlayerPrefs.GetInt("Ball4");
        if (lockedBall == 1) { lockIcon[0].SetActive(false); }
        if (lockedBall2 == 1) { lockIcon[1].SetActive(false); }
        if (lockedBall3 == 1) { lockIcon[2].SetActive(false); }
    }

    public void OnClickPlayer1()
    {
        PlayerPrefs.SetInt("Selected_Player", 1);
        player1.GetComponent<Image>().color = new Color32(58, 58, 58, 150);
        player1Border.SetActive(true);
        player1Arrow.SetActive(true);
        player2.GetComponent<Image>().color = new Color32(58, 58, 58, 0);
        player2Border.SetActive(false);
        player2Arrow.SetActive(false);
    }
    public void OnClickPlayer2()
    {
        PlayerPrefs.SetInt("Selected_Player", 2);
        player1.GetComponent<Image>().color = new Color32(58, 58, 58, 0);
        player1Border.SetActive(false);
        player1Arrow.SetActive(false);
        player2.GetComponent<Image>().color = new Color32(58, 58, 58, 150);
        player2Border.SetActive(true);
        player2Arrow.SetActive(true);
    }

    public void OnClickBall1()
    {
        PlayerPrefs.SetInt("Selected_Ball", 0);
        for (int i = 0; i < ball.Length; i++)
        {
            ball[i].GetComponent<Image>().color = new Color32(255, 255, 225, 0);
        }
        ball[0].GetComponent<Image>().color = new Color32(255, 255, 225, 150);
    }
    public void OnClickBall2()
    {
        if (lockedBall == 1)
        {
            PlayerPrefs.SetInt("Selected_Ball", 1);
            for (int i = 0; i < ball.Length; i++)
            {
                ball[i].GetComponent<Image>().color = new Color32(255, 255, 225, 0);
            }
            ball[1].GetComponent<Image>().color = new Color32(255, 255, 225, 150);
        }
    }
    public void OnClickBall3()
    {
        if (lockedBall2 == 1)
        {
            PlayerPrefs.SetInt("Selected_Ball", 2);
            for (int i = 0; i < ball.Length; i++)
            {
                ball[i].GetComponent<Image>().color = new Color32(255, 255, 225, 0);
            }
            ball[2].GetComponent<Image>().color = new Color32(255, 255, 225, 150);
        }
    }
    public void OnClickBall4()
    {
        if (lockedBall3 == 1)
        {
            PlayerPrefs.SetInt("Selected_Ball", 3);
            for (int i = 0; i < ball.Length; i++)
            {
                ball[i].GetComponent<Image>().color = new Color32(255, 255, 225, 0);
            }
            ball[3].GetComponent<Image>().color = new Color32(255, 255, 225, 150);
        }
    }
}
