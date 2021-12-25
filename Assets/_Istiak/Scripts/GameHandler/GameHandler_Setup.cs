using _.Stuff.GridPathfinding;
using UnityEngine;
using MyGame.Utils;
using MyGame.MonoBehaviours;

public class GameHandler_Setup : MonoBehaviour
{
    public static GridPathfinding gridPathfinding;

    private void OnEnable()
    {
        gridPathfinding = new GridPathfinding(new Vector3(-150, -65), new Vector3(150, 65), 5f);
        gridPathfinding.RaycastWalkable();
    }
    
}