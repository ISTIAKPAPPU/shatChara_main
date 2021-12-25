using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedPlayer : MonoBehaviour, IPointerClickHandler
{
    public static event Action<GameObject> PlayerSelected;
   // public bool clickable;

    private void OnEnable()
    {
        //clickable = true;
        Filler.ActivePlayerSelected += OnPlayerStartMove;
    }

    private void OnDisable()
    {
        Filler.ActivePlayerSelected -= OnPlayerStartMove;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("2");
        if (!GameValue.CanTakeInput) return;
        Debug.Log("3");
        //if (!clickable) return;
        Debug.Log("4");
        PlayerSelected?.Invoke(eventData.pointerCurrentRaycast.gameObject);
    }

    private void OnPlayerStartMove()
    {
        
    }
}