using System;
using UnityEngine;

public class PlayerListDataUpdate : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void OnEnable()
    {
        foreach (Transform data in transform)
        {
            data.GetChild(0).GetComponent<MoveTransformVelocity>().moveSpeed = moveSpeed / 2;
            data.GetChild(1).GetComponent<MoveTransformVelocity>().moveSpeed = moveSpeed;
        }
    }
}