using System.Collections.Generic;
using UnityEngine;

public static class GameValue
{
    public enum PlayerWillPlay
    {
        Player,
        Ai,
        None
    }

    public static PlayerWillPlay PlayerTurn;
    public static bool IsGameStart;
    public static bool CanTakeInput;
    public static bool GameOver;
    public static Transform BallTransform;
    public static Transform PlayerListTransform;
    public static GameObject SelectedPlayer;
    public static int PlayerTurnCounter;

    public static PlayerWillPlay GlobalLastPlayer;
    public static int GlobalLevel;
}