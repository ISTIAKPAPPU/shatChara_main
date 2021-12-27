using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public GameObject level;
    public GameObject toss;

    public GameObject timer;

    // public GameObject game_win;
    //  public GameObject game_lose;
    public GameObject game_pause;

    int removeAds;
    AdMobManager adMob;

    private void Start()
    {
        PlayerPrefs.SetInt("Start_Game", 0);
        toss.SetActive(true);
        removeAds = PlayerPrefs.GetInt("Remove_Ads");
        if (removeAds == 0)
        {
            adMob = FindObjectOfType<AdMobManager>();
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Start_Game") == 1)
        {
            PlayerPrefs.SetInt("Start_Game", 0);
        }
    }

    public void StartLevel()
    {
        toss.SetActive(false);
        int playerLevel = PlayerPrefs.GetInt("Player_Level");
       // timer.SetActive(true);
        print("Lvl " + playerLevel);
        level.SetActive(true);
        PlayerPrefs.SetInt("Start_Game", 1);
        //Obs();
    }
    
    public void RePlay()
    {
        if (removeAds == 0)
        {
            adMob.requestInterstital();
            adMob.ShowInterstitialAd();
        }

        PlayerPrefs.SetInt("Player_Side", 0);
        Time.timeScale = 1;
        int playerLevel = PlayerPrefs.GetInt("Player_Level");
        SceneManager.LoadScene("Level");
    }

    public void NextLevel()
    {
        if (removeAds == 0)
        {
            adMob.requestInterstital();
            adMob.ShowInterstitialAd();
        }

        PlayerPrefs.SetInt("Player_Side", 0);
        Time.timeScale = 1;
        int playerLevel = PlayerPrefs.GetInt("Player_Level");
        SetScore(playerLevel);
        PlayerPrefs.SetInt("Player_Level", playerLevel + 1);
        int unlockedLevel = PlayerPrefs.GetInt("Unlocked_Level");
        if (unlockedLevel == playerLevel)
        {
            PlayerPrefs.SetInt("Unlocked_Level", unlockedLevel + 1);
        }

        PlayerPrefs.SetInt("Menu_Stat", 1);
        SceneManager.LoadScene("Level");

        //PlayerPrefs.SetInt("Menu_State", 1);
        //FindObjectOfType<ProgressSceneLoader>().LoadScene("GameScene");
    }

    public void SetScore(int level)
    {
        int score = PlayerPrefs.GetInt("Score");
        PlayerPrefs.SetInt("Score" + level, score);
    }

    public void OnClickPauseBtn()
    {
        Time.timeScale = 0;
        game_pause.SetActive(true);
    }

    public void OnClickResumeBtn()
    {
        Time.timeScale = 1;
        game_pause.SetActive(false);
    }

    public void OnClickHomeBtn()
    {
        if (removeAds == 0)
        {
            adMob.requestInterstital();
            adMob.ShowInterstitialAd();
        }

        Time.timeScale = 1;
        PlayerPrefs.SetInt("Menu_Stat", 0);
        SceneManager.LoadScene("Menu");
    }
}