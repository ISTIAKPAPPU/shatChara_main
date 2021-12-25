using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject[] player;
    public GameObject[] player2;
    public GameObject[] player3;
    public GameObject[] player4;
    public GameObject[] enemy;
    public GameObject[] enemy2;
    public GameObject[] enemy3;
    public GameObject[] enemy4;

    public GameObject win;
    public GameObject loss;

    int toss;
	int selectedPlayer;
    bool playerActive;

    bool enemyActive;

    void Start()
    {
        toss = PlayerPrefs.GetInt("Player_Side");
		int selectedPlayer = PlayerPrefs.GetInt("Selected_Player");
        PlayerPrefs.SetInt("Stone_Moved", 0);
        PlayerPrefs.SetInt("EnemyBack", 0);
        PlayerPrefs.SetInt("Crushed", 0);
        PlayerPrefs.SetInt("Health", 3);
        PlayerPrefs.SetInt("Active_Player_Index", 0);
        if (toss == 2)
        {  
	      if(PlayerPrefs.GetInt("Player_Set")==1)
            player[0].GetComponent<PlayerController>().enabled = true;
	      if(PlayerPrefs.GetInt("Player_Set")==2)
            player2[0].GetComponent<PlayerController>().enabled = true;
	      if(PlayerPrefs.GetInt("Player_Set")==3)
            player3[0].GetComponent<PlayerController>().enabled = true;
	      if(PlayerPrefs.GetInt("Player_Set")==4)
            player4[0].GetComponent<PlayerController>().enabled = true;
        }

        GameObject[] e = GameObject.FindGameObjectsWithTag("Enemy");
        int j = 0;
        for (int i = 0; i < e.Length; i++)
        {
            if (e[i].activeInHierarchy)
            {  
		       if(PlayerPrefs.GetInt("Player_Set")==1)
                enemy[j] = e[i];
		       if(PlayerPrefs.GetInt("Player_Set")==2)
                enemy2[j] = e[i];
		       if(PlayerPrefs.GetInt("Player_Set")==3)
                enemy3[j] = e[i];
		       if(PlayerPrefs.GetInt("Player_Set")==4)
                enemy4[j] = e[i];
                
				j++;
            }
        }
        GameObject[] f = GameObject.FindGameObjectsWithTag("Player");
        int k = 0;
        for (int i = 0; i < f.Length; i++)
        {
            if (f[i].activeInHierarchy)
            {
               if(PlayerPrefs.GetInt("Player_Set")==1)
				player[k] = f[i];
               if(PlayerPrefs.GetInt("Player_Set")==2)
				player2[k] = f[i];
               if(PlayerPrefs.GetInt("Player_Set")==3)
				player3[k] = f[i];
               if(PlayerPrefs.GetInt("Player_Set")==4)
				player4[k] = f[i];
			
                k++;
            }
        }
    }

    void Update()
    {
        int crushed = PlayerPrefs.GetInt("Crushed");
        if (crushed == 1 && !enemyActive && toss == 2)
        {
            enemyActive = true;
            int rand = Random.Range(0, enemy.Length);
            //enemy[rand].GetComponent<BoxCollider2D>().enabled = true;
            if(PlayerPrefs.GetInt("Player_Set")==1)
              enemy[rand].GetComponent<EnemyController>().enabled = true;
            if(PlayerPrefs.GetInt("Player_Set")==2)
              enemy2[rand].GetComponent<EnemyController>().enabled = true;
            if(PlayerPrefs.GetInt("Player_Set")==3)
              enemy3[rand].GetComponent<EnemyController>().enabled = true;
            if(PlayerPrefs.GetInt("Player_Set")==4)
              enemy4[rand].GetComponent<EnemyController>().enabled = true;
        }

        int back = PlayerPrefs.GetInt("EnemyBack");
        if (back == 1 && toss == 2)
        {
            enemyActive = false;
            for (int i = 0; i < enemy.Length; i++)
            {
               // enemy[i].GetComponent<BoxCollider2D>().enabled = false;
			   if(PlayerPrefs.GetInt("Player_Set")==1)
                enemy[i].GetComponent<EnemyController>().enabled = false;
			   if(PlayerPrefs.GetInt("Player_Set")==2)
                enemy2[i].GetComponent<EnemyController>().enabled = false;
			   if(PlayerPrefs.GetInt("Player_Set")==3)
                enemy3[i].GetComponent<EnemyController>().enabled = false;
			   if(PlayerPrefs.GetInt("Player_Set")==4)
                enemy4[i].GetComponent<EnemyController>().enabled = false;
            }
        }

        // active player script
        if (toss == 1 && !playerActive)
        {
            if(PlayerPrefs.GetInt("Player_Set")==1)
			enemy[0].GetComponent<PlayerController>().enabled = true;
            if(PlayerPrefs.GetInt("Player_Set")==2)
			enemy2[0].GetComponent<PlayerController>().enabled = true;
            if(PlayerPrefs.GetInt("Player_Set")==3)
			enemy3[0].GetComponent<PlayerController>().enabled = true;
            if(PlayerPrefs.GetInt("Player_Set")==4)
			enemy4[0].GetComponent<PlayerController>().enabled = true;
		
            playerActive = true;
        }
    }

    public void Next(int i,int j)
    {
        playerActive = false;
        PlayerPrefs.SetInt("Stone_Moved", 0);
        PlayerPrefs.SetInt("EnemyBack", 0);
        PlayerPrefs.SetInt("Crushed", 0);
        if (toss == 1)
        {  
	        if(PlayerPrefs.GetInt("Player_Set")==1){
            enemy[i].GetComponent<PlayerController>().enabled = false;
            enemy[j].GetComponent<PlayerController>().enabled = true;
            }
	        if(PlayerPrefs.GetInt("Player_Set")==2){
            enemy2[i].GetComponent<PlayerController>().enabled = false;
            enemy2[j].GetComponent<PlayerController>().enabled = true;
            }
	        if(PlayerPrefs.GetInt("Player_Set")==3){
            enemy3[i].GetComponent<PlayerController>().enabled = false;
            enemy3[j].GetComponent<PlayerController>().enabled = true;
            }
	        if(PlayerPrefs.GetInt("Player_Set")==4){
            enemy4[i].GetComponent<PlayerController>().enabled = false;
            enemy4[j].GetComponent<PlayerController>().enabled = true;
            }
		}
        if (toss == 2)
        {
            if(PlayerPrefs.GetInt("Player_Set")==1){
			player[i].GetComponent<PlayerController>().enabled = false;
            player[j].GetComponent<PlayerController>().enabled = true;
            }
			if(PlayerPrefs.GetInt("Player_Set")==2){
			player2[i].GetComponent<PlayerController>().enabled = false;
            player2[j].GetComponent<PlayerController>().enabled = true;
            }
			if(PlayerPrefs.GetInt("Player_Set")==3){
			player3[i].GetComponent<PlayerController>().enabled = false;
            player3[j].GetComponent<PlayerController>().enabled = true;
            }
			if(PlayerPrefs.GetInt("Player_Set")==4){
			player4[i].GetComponent<PlayerController>().enabled = false;
            player4[j].GetComponent<PlayerController>().enabled = true;
            }
		}
        Debug.Log("L");
        PlayerPrefs.SetInt("Active_Player_Index", j);
    }

    public void Win()
    {
        win.SetActive(true);
        Time.timeScale = 0;
    }

    public void Loss()
    {
        loss.SetActive(true);
        Time.timeScale = 0;
    }
}
