using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Filler : MonoBehaviour, IPointerClickHandler
{
    public List<GameObject> fillers = new List<GameObject>();
    public bool coolingDown;
    private float timerMax;
    private float timer;
    private int index;
    public static event Action<int,bool> ThrowBall;
    public static event Action ActivePlayerSelected;
    private bool clickable;

    private void OnEnable()
    {
        clickable = true;
        index = fillers.Count - 1;
        coolingDown = true;
    }

    private void OnDisable()
    {
        ResetFillBar();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickable)
        {
            clickable = false;
            coolingDown = false;
            GameValue.SelectedPlayer.transform.GetChild(0).GetComponent<Animator>().SetBool("Seat", true);
            StartCoroutine(WaitForAnimation());
        }
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1.25f);
        if (GameValue.SelectedPlayer.transform.position.x < 0)
        {
            GameValue.SelectedPlayer.transform.GetChild(0).localScale = new Vector3(-1f, 1f, 1f);
        }
        GameValue.SelectedPlayer.transform.GetChild(0).GetComponent<Animator>().SetBool("Seat", false);
        // yield return new WaitForSeconds(0.25f);
        ThrowBall?.Invoke(Mathf.Abs(index + 1 - fillers.Count),true);
        ActivePlayerSelected?.Invoke();
        yield return new WaitForSeconds(1f);
        transform.gameObject.SetActive(false);
        clickable = true;
        //StartCoroutine(DestroyFiller());
    }

    private IEnumerator DestroyFiller()
    {
        yield return new WaitForSeconds(1f);
        transform.gameObject.SetActive(false);
        clickable = true;
    }

    private async void Update()
    {
        if (!coolingDown) return;
        if (!Waited((GameValue.PlayerTurn == GameValue.PlayerWillPlay.Player) ? 0.1f : 0.08f)) return;
        if (index < 0)
        {
            ResetFillBar();
            index = fillers.Count - 1;
            return;
        }

        fillers[index].gameObject.SetActive(true);
        index--;
    }

    private void ResetFillBar()
    {
        foreach (var filler in fillers)
        {
            filler.SetActive(false);
        }
    }

    private bool Waited(float seconds)
    {
        timerMax = seconds;

        timer += Time.deltaTime;

        if (!(timer >= timerMax)) return false;
        timer = 0;
        timerMax = 0;
        return true;
    }
}