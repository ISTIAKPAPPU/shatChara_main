using UnityEngine;


public class MoveWaypoints : MonoBehaviour {
    
    [SerializeField] private Vector3[] waypointList;
    private int waypointIndex;

    private void Update() {
        SetMovePosition(GetWaypointPosition());

        float arrivedAtPositionDistance = 1f;
        if (Vector3.Distance(transform.position, GetWaypointPosition()) < arrivedAtPositionDistance) {
            // Reached position
            waypointIndex = (waypointIndex + 1) % waypointList.Length;
        }
    }

    private Vector3 GetWaypointPosition() {
        return waypointList[waypointIndex];
    }

    private void SetMovePosition(Vector3 movePosition) {
        GetComponent<IMovePosition>().SetMovePosition(movePosition);
    }

}
