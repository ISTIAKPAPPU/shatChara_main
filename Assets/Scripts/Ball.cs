using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    int toss;
    
    void Start()
    {
        int playerLevel = PlayerPrefs.GetInt("Player_Level");
        if(playerLevel==8 || playerLevel == 9)
        {
            this.gameObject.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 1;
        }
        if (playerLevel < 8)
        {
            this.gameObject.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 0.5f;
        }
        toss = PlayerPrefs.GetInt("Player_Side");
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Stones")
        {
            int viv = PlayerPrefs.GetInt("Vibration");
            if (viv == 0)
            {
                Handheld.Vibrate();
            }
            PlayerPrefs.SetInt("Crushed", 2);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        if (other.gameObject.name == "Objects")
        {
             //PlayerPrefs.GetInt("Health")
			 PlayerPrefs.SetInt("Health",PlayerPrefs.GetInt("Health")-1);
		   // PlayerPrefs.SetInt("Crushed", 2);
        }
		if (other.gameObject.name == "platform")
        {
            PlayerPrefs.SetInt("Crushed", 2);
        }
        if (other.gameObject.tag == "Border")
        {
            PlayerPrefs.SetInt("Crushed", 2);
        }
    }
}
