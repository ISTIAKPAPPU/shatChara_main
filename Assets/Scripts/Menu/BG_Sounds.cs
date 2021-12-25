using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Sounds : MonoBehaviour
{
    public GameObject bgSounds;

    // Start is called before the first frame update
    void Start()
    {
        int sounds = PlayerPrefs.GetInt("Sound");
        if (sounds == 1)
        {
            bgSounds.SetActive(false);
        }
        if (sounds == 0)
        {
            bgSounds.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int sounds = PlayerPrefs.GetInt("Sound");
        if (sounds == 1)
        {
            bgSounds.SetActive(false);
        }
        if (sounds == 0)
        {
            bgSounds.SetActive(true);
        }
    }
}
