using UnityEngine;
public class PlayerData : MonoBehaviour
{
     public bool playerTurnEnd = false;

     public Vector3 initialPos;

    private void Awake()
    {
        Transform transform1;
        initialPos = (transform1 = transform).CompareTag("Player")
            ? transform1.position
            : transform.GetChild(0).position;
    }
}