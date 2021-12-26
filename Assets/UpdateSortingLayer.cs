using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSortingLayer : MonoBehaviour
{
    private List<Transform> _playerList;
    public Transform throwPoint;
    public Sprite front;
    public Sprite back;
    public Sprite left;
    public Sprite right;

    private void OnEnable()
    {
        _playerList = new List<Transform>
        {
            transform.GetChild(0),
            transform.GetChild(1),
            transform.GetChild(2)
        };
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameValue.CanTakeInput) return;

        _playerList.Sort((c1, c2) =>
        {
            var ret = c2.position.y.CompareTo(c1.position.y);
            return ret;
        });
        // foreach (var player in _playerList)
        // {
        //     int index = _playerList.FindIndex(a => a == player);
        //     player.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = index + 1;
        //     if (!GameValue.IsGameStart)
        //     {
        //         if (GameValue.PlayerTurn == GameValue.PlayerWillPlay.Player)
        //         {
        //             if (GameValue.SelectedPlayer == null || (player != GameValue.SelectedPlayer.transform &&
        //                                                      !player.GetChild(1).gameObject.activeSelf))
        //             {
        //                 Debug.Log("AIIII");
        //                 player.GetComponent<Rigidbody2D>().constraints =
        //                     RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY |
        //                     RigidbodyConstraints2D.FreezeRotation;
        //             }
        //             else
        //             {
        //                 player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        //                 player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        //             }
        //         }
        //         else
        //         {
        //             if (!player.GetChild(1).gameObject.activeSelf)
        //             {
        //                 Debug.Log("Player");
        //                 player.GetComponent<Rigidbody2D>().constraints =
        //                     RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY |
        //                     RigidbodyConstraints2D.FreezeRotation;
        //             }
        //             else
        //             {
        //                 player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        //                 player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        //             }
        //         }
        //     }
        //     else
        //     {
        //         if (!player.GetChild(1).gameObject.activeSelf)
        //         {
        //             Debug.Log("Player");
        //             player.GetComponent<Rigidbody2D>().constraints =
        //                 RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY |
        //                 RigidbodyConstraints2D.FreezeRotation;
        //         }
        //         else
        //         {
        //             player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        //             player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        //         }
        //     }
        // }
    }
}