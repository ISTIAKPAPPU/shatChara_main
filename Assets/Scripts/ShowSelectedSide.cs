using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSelectedSide : MonoBehaviour
{
    public GameObject playerSet1;
    public GameObject playerSet2;
    public GameObject playerSet3;
    public GameObject playerSet4;

    // Start is called before the first frame update
    void Start()
    {
        // player1 yellow
        // toss 2 rightside
        int selectedPlayer = PlayerPrefs.GetInt("Selected_Player");
        int toss = PlayerPrefs.GetInt("Player_Side");
        if(selectedPlayer==1 && toss == 2)
        {
            playerSet1.SetActive(true);
			PlayerPrefs.SetInt("Player_Set",1);
        }
        if(selectedPlayer == 1 && toss == 1)
        {
            playerSet2.SetActive(true);
			PlayerPrefs.SetInt("Player_Set",2);
        }
        if(selectedPlayer == 2 && toss == 2)
        {
            playerSet3.SetActive(true);
			PlayerPrefs.SetInt("Player_Set",3);
        }
        if (selectedPlayer == 2 && toss == 1)
        {
            playerSet4.SetActive(true);
			PlayerPrefs.SetInt("Player_Set",4);
        }
    }
}
