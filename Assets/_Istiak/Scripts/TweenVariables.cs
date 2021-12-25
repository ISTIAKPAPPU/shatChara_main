using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TweenVariables : MonoBehaviour
{
    public int numJumps = 1;
    public float jumpPower = 30f;
    public float jumpDuration = 1f;
    public bool isSnapping = false;
    public Ease ease;
}