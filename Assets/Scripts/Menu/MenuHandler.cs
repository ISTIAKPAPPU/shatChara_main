using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public GameObject[] locked;
    public GameObject menuBtn;
    public GameObject levelMenu;
    public GameObject settingsMenu;
    public GameObject instructionMenu;
    public GameObject aboutMenu;
    public GameObject exitMenu;
    public GameObject selectPlayerMenu;
    public GameObject ballSelectionMenu;
    public GameObject lockedMsg;

    // Start is called before the first frame update
    void Start()
    {
        
		//PlayerPrefs.SetInt("Player_Level", 0);
        //PlayerPrefs.SetInt("Unlocked_Level", 0);
		
		int selectedPlayer = PlayerPrefs.GetInt("Selected_Player");
        int selectedBall = PlayerPrefs.GetInt("Selected_Ball");
        if (selectedPlayer == 0 || selectedBall == 0)
        {
            PlayerPrefs.SetInt("Selected_Player", 1);
            PlayerPrefs.SetInt("Selected_Ball", 1);
        }
        int menuStat = PlayerPrefs.GetInt("Menu_Stat");
        if (menuStat == 0) { menuBtn.SetActive(true); }
        if(menuStat == 1) { levelMenu.SetActive(true); }

        int levelLocked = PlayerPrefs.GetInt("Unlocked_Level");
        for(int i = 0; i < locked.Length; i++)
        {
            if (i < levelLocked)
            {
                locked[i].SetActive(false);
            }
            if (i > levelLocked)
            {
                locked[i].SetActive(true);
            }
        }
    }
	
	void Update ()
	{
		int levelLocked = PlayerPrefs.GetInt("Unlocked_Level");
        for(int i = 0; i < locked.Length; i++)
        {
            if (i < levelLocked)
            {
                locked[i].SetActive(false);
            }
            if (i > levelLocked)
            {
                locked[i].SetActive(true);
            }
        }
	}

    public void OnClickPlay()
    {
        ballSelectionMenu.SetActive(false);
        menuBtn.SetActive(false);
        levelMenu.SetActive(true);
    }
    public void OnClickShop()
    {
        ballSelectionMenu.SetActive(true);
        menuBtn.SetActive(false);
        levelMenu.SetActive(false);
    }
    public void OnClickSettings()
    {
        ballSelectionMenu.SetActive(false);
        menuBtn.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OnClickInstructions()
    {
        ballSelectionMenu.SetActive(false);
        menuBtn.SetActive(false);
        instructionMenu.SetActive(true);
    }

    public void OnClickAbout()
    {
        ballSelectionMenu.SetActive(false);
        menuBtn.SetActive(false);
        aboutMenu.SetActive(true);
    }

    public void OnClickExit()
    {
        ballSelectionMenu.SetActive(false);
        menuBtn.SetActive(false);
        exitMenu.SetActive(true);
    }

    public void OnClickExitYes()
    {
        Application.Quit();
    }

    public void OnCliclSelectPlayerMenu()
    {
        settingsMenu.SetActive(false);
        selectPlayerMenu.SetActive(true);
    }
    public void OnClickBackFromPlayer()
    {
        selectPlayerMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OnClickBallSelectionMneu()
    {
        settingsMenu.SetActive(false);
        ballSelectionMenu.SetActive(true);
    }

    public void OnClickBackFromBall()
    {
        settingsMenu.SetActive(true);
        ballSelectionMenu.SetActive(false);
    }

    public void OnClickBack()
    {
        menuBtn.SetActive(true);
        levelMenu.SetActive(false);
        settingsMenu.SetActive(false);
        instructionMenu.SetActive(false);
        aboutMenu.SetActive(false);
        exitMenu.SetActive(false);
        selectPlayerMenu.SetActive(false);
    }
	

    public void OnClickLevel1()
    {
        PlayerPrefs.SetInt("Player_Level", 0);
        SceneManager.LoadScene("Level");
    }

    public void OnClickLevel2()
    {
        int unlockedLevel = PlayerPrefs.GetInt("Unlocked_Level");
        if (unlockedLevel >= 1)
        {
            PlayerPrefs.SetInt("Player_Level", 1);
            SceneManager.LoadScene("Level");
        }
        else
        {
            StartCoroutine(LockedMessage());
        }
    }

    public void OnClickLevel3()
    {
        int unlockedLevel = PlayerPrefs.GetInt("Unlocked_Level");
        if (unlockedLevel >= 2)
        {
            PlayerPrefs.SetInt("Player_Level", 2);
            SceneManager.LoadScene("Level");
        }
        else
        {
            StartCoroutine(LockedMessage());
        }
    }

    public void OnClickLevel4()
    {
        int unlockedLevel = PlayerPrefs.GetInt("Unlocked_Level");
        if (unlockedLevel >= 3)
        {
            PlayerPrefs.SetInt("Player_Level", 3);
            SceneManager.LoadScene("Level");
        }
        else
        {
            StartCoroutine(LockedMessage());
        }
    }

    public void OnClickLevel5()
    {
        int unlockedLevel = PlayerPrefs.GetInt("Unlocked_Level");
        if (unlockedLevel >= 4)
        {
            PlayerPrefs.SetInt("Player_Level", 4);
            SceneManager.LoadScene("Level");
        }
        else
        {
            StartCoroutine(LockedMessage());
        }
    }

    public void OnClickLevel6()
    {
        int unlockedLevel = PlayerPrefs.GetInt("Unlocked_Level");
        if (unlockedLevel >= 5)
        {
            PlayerPrefs.SetInt("Player_Level", 5);
            SceneManager.LoadScene("Level");
        }
        else
        {
            StartCoroutine(LockedMessage());
        }
    }

    public void OnClickLevel7()
    {
        int unlockedLevel = PlayerPrefs.GetInt("Unlocked_Level");
        if (unlockedLevel >= 6)
        {
            PlayerPrefs.SetInt("Player_Level", 6);
            SceneManager.LoadScene("Level");
        }
        else
        {
            StartCoroutine(LockedMessage());
        }
    }

    public void OnClickLevel8()
    {
        int unlockedLevel = PlayerPrefs.GetInt("Unlocked_Level");
        if (unlockedLevel >= 7)
        {
            PlayerPrefs.SetInt("Player_Level", 7);
            SceneManager.LoadScene("Level");
        }
        else
        {
            StartCoroutine(LockedMessage());
        }
    }

    public void OnClickLevel9()
    {
        int unlockedLevel = PlayerPrefs.GetInt("Unlocked_Level");
        if (unlockedLevel >= 8)
        {
            PlayerPrefs.SetInt("Player_Level", 8);
            SceneManager.LoadScene("Level");
        }
        else
        {
            StartCoroutine(LockedMessage());
        }
    }

    public void OnClickLevel10()
    {
        int unlockedLevel = PlayerPrefs.GetInt("Unlocked_Level");
        if (unlockedLevel >= 9)
        {
            PlayerPrefs.SetInt("Player_Level", 9);
            SceneManager.LoadScene("Level");
        }
        else
        {
            StartCoroutine(LockedMessage());
        }
    }

    IEnumerator LockedMessage()
    {
        lockedMsg.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        lockedMsg.SetActive(false);
    }
}
