using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    // Start is called before the first frame update
    public static UiController Uc;
    public GameObject roundPanel;
    public RectTransform gameWinPanel;
    public RectTransform gameLosePanel;
    public Ease ease = Ease.OutBounce;
    public Transform playerScore;
    public Transform aiScore;
    public GameObject pfScore;
    public static event Action StartGameAgain;

    private void Awake()
    {
        if (Uc == null)
        {
            Uc = this;
        }
    }

    public void OpenPanel(string message, Action callback)
    {
        Debug.Log("Enter");

        roundPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;
        roundPanel.SetActive(true);
        DOTween.Sequence()
            .Append(roundPanel.transform.DOScale(1, 2f).SetEase(ease))
            .Append(roundPanel.transform.DOScale(0, 1f))
            .AppendCallback(() =>
            {
                roundPanel.SetActive(false);
                callback();
            });
    }

    public void OpenWinPanel()
    {
        gameWinPanel.gameObject.SetActive(true);
        // gameOverPanel.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = message;
        DOTween.Sequence()
            .Append(gameWinPanel.DOAnchorPos(Vector2.zero, 1f).SetEase(ease));
    }

    public void OpenLosePanel()
    {
        gameLosePanel.gameObject.SetActive(true);
        // gameOverPanel.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = message;
        DOTween.Sequence()
            .Append(gameLosePanel.DOAnchorPos(Vector2.zero, 1f).SetEase(ease));
    }

    public async void CloseWinPanel()
    {
        var activePanel = gameWinPanel.gameObject.activeSelf ? gameWinPanel : gameLosePanel;
        Debug.Log("levelName:"+activePanel.name);
        DOTween.Sequence()
            .Append(activePanel.DOAnchorPos(new Vector2(0, 700), 1f).SetEase(Ease.Linear))
            .AppendCallback(() => { activePanel.gameObject.SetActive(false); });
        await Task.Delay(1000);
        StartGameAgain?.Invoke();
    }

    public void UpdatePlayerScore()
    {
        Instantiate(pfScore, playerScore);
    }

    public void UpdateAiScore()
    {
        Instantiate(pfScore, aiScore);
    }

    public void ResetScore()
    {
        while (playerScore.childCount != 0)
        {
            DestroyImmediate(playerScore.GetChild(0).gameObject);
        }

        while (aiScore.childCount != 0)
        {
            DestroyImmediate(aiScore.GetChild(0).gameObject);
        }
    }
}