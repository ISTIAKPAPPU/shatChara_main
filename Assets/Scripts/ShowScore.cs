using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public Text scoreText;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        score = PlayerPrefs.GetInt("Score");
        scoreText.text = score.ToString();
    }
}
