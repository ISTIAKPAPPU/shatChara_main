using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillerBar : MonoBehaviour
{
    public Image filler;
    public bool coolingDown;
    public float waitTime = 30.0f;
    public Gradient gradient;

    // Update is called once per frame
    private void Update()
    {
        if (!coolingDown) return;
        filler.fillAmount += 1.0f / waitTime * Time.deltaTime;
        filler.color = gradient.Evaluate(filler.fillAmount);
    }
}
