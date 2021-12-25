using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTrigger : MonoBehaviour
{
    public GameObject gamelose;
    public GameObject gameWin;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled) return; // if script disable dont execute the function

        if (other.tag == "Ball" && gameObject.tag == "Player")
        {
            int crushed = PlayerPrefs.GetInt("Crushed");
            if (crushed == 1)
            {
                Destroy(GameObject.FindGameObjectWithTag("Ball"));
                Time.timeScale = 0;
                gamelose.SetActive(true);
            }
        }
        if (other.tag == "Ball" && gameObject.tag == "Enemy")
        {
            int crushed = PlayerPrefs.GetInt("Crushed");
            if (crushed == 1)
            {
                Destroy(GameObject.FindGameObjectWithTag("Ball"));
                Time.timeScale = 0;
                gameWin.SetActive(true);
            }
        }
    }
}
