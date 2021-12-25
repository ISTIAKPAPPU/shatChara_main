using System.Collections;
using System.Collections.Generic;
using BallScripts;
using UnityEngine;

public class UpdateAiSortingLayer : MonoBehaviour
{
    private readonly List<Transform> _aiList = new List<Transform>();

    private void Start()
    {
        _aiList.Add(transform.GetChild(0).GetChild(0));
        _aiList.Add(transform.GetChild(0).GetChild(1));
        _aiList.Add(transform.GetChild(1).GetChild(0));
        _aiList.Add(transform.GetChild(1).GetChild(1));
        _aiList.Add(transform.GetChild(2).GetChild(0));
        _aiList.Add(transform.GetChild(2).GetChild(1));
    }

    // Update is called once per frame
    private void Update()
    {
        _aiList.Sort((c1, c2) =>
        {
            var ret = c2.position.y.CompareTo(c1.position.y);
            return ret;
        });
        foreach (var player in _aiList)
        {
            int index = _aiList.FindIndex(a => a == player);
            player.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = index + 1;
            if (GameValue.PlayerTurn != GameValue.PlayerWillPlay.Ai)
            {
                if (GameValue.BallTransform != null)
                {
                    if (player != GameValue.BallTransform.GetComponent<BallManager>().ballPicker)
                    {
                        //player.transform.GetChild(0).GetComponent<Animator>().SetBool("Seat", false);
                       // player.transform.GetChild(0).GetComponent<Animator>().SetBool("Throw", true);
                    }
                }
            }
        }
    }
}