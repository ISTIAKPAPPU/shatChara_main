using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toss : MonoBehaviour
{
    public GameObject toss;
    public GameObject win;
    public GameObject loss;
    public GameObject lossPanel1;
    public GameObject lossPanel2;
    public GameObject coinAnim;
    public GameObject startBtn;

    int choice;
    int rand;

    public void OnClickHead()
    {
        choice = 0;
        rand = Random.Range(0, 2);
        PlayerPrefs.SetInt("Random", rand);
        CoinFlip();
    }
    public void OnClickTail()
    {
        //dollar
        choice = 1;
        rand = Random.Range(0, 2);
        PlayerPrefs.SetInt("Random", rand);
        CoinFlip();
    }

    public void CoinFlip()
    {
        Time.timeScale = 1;
        toss.SetActive(false);
        coinAnim.SetActive(true);
    }

    public IEnumerator CoinToss()
    {
        if (rand == choice)
        {
            yield return new WaitForSeconds(5f);
            win.SetActive(true);
            PlayerPrefs.SetInt("Player_Side", 2);
            yield return new WaitForSeconds(5f);
            startBtn.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(5f);
            loss.SetActive(true);
            yield return new WaitForSeconds(4f);
            lossPanel1.SetActive(false);
            lossPanel2.SetActive(true);
            PlayerPrefs.SetInt("Player_Side", 1);
            yield return new WaitForSeconds(5f);
            startBtn.SetActive(true);
        }
    }
}
