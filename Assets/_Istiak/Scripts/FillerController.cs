using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillerController : MonoBehaviour
{
    public static FillerController Fb;
    public GameObject fillBar;

    private void Awake()
    {
        if (Fb == null)
        {
            Fb = this;
        }
    }

    public void ActiveFillBar()
    {
        fillBar.SetActive(true);
    }
    
    public void DeActiveFillBar()
    {
        fillBar.SetActive(false);
    }
}