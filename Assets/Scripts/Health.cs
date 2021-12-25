using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject[] health;
    public GameObject loss;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Health", 3);
    }

    // Update is called once per frame
    void Update()
    {
        int healthCount = PlayerPrefs.GetInt("Health");
        if(healthCount <= 2)
        {
            health[2].SetActive(false);
        }
        if (healthCount <= 1)
        {
            health[1].SetActive(false);
        }
        if (healthCount == 0)
        {
            health[0].SetActive(false);
            Time.timeScale = 0;
            loss.SetActive(true);
        }
    }
}
