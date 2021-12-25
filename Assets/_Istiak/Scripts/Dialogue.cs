using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI txtDisplay;
    [SerializeField] private string txt;
    [SerializeField] private float typingSpeed;
    public static Dialogue Instance;

    private void OnEnable()
    {
        txtDisplay.text = "";
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnDisable()
    {
        txtDisplay.text = "";
    }

    public void StartTyping(string text)
    {
        txt = text;
        StartCoroutine(Type());
    }

    private IEnumerator Type()
    {
        foreach (var letter in txt.ToCharArray())
        {
            txtDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(1f);
        txtDisplay.text = "";
    }
}