using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickSoundOn()
    {
        PlayerPrefs.SetInt("Sound", 0);
    }
    public void OnClickSoundOff()
    {
        PlayerPrefs.SetInt("Sound", 1);
    }
    public void OnClickVibrationOn()
    {
        PlayerPrefs.SetInt("Vibration", 0);
    }
    public void OnClickVibrationOff()
    {
        PlayerPrefs.SetInt("Vibration", 1);
    }
}
