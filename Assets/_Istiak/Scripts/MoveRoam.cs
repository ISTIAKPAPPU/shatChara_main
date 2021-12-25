using UnityEngine;
using MyGame.Utils;

public class MoveRoam : MonoBehaviour {

    private Vector3 startPosition;
    private Vector3 targetMovePosition;
    [SerializeField] private float maxMoveRoam = 30f;
    [SerializeField] private float minMoveRoam = 10f;

    private void Awake() {
        startPosition = transform.position;
    }

    private void Start() {
        SetRandomMovePosition();
    }

    private void SetRandomMovePosition() {
        targetMovePosition = startPosition + UtilsClass.GetRandomDir() * Random.Range(minMoveRoam, maxMoveRoam);
    }

    private void Update() {
        SetMovePosition(targetMovePosition);

        float arrivedAtPositionDistance = 1f;
        if (Vector3.Distance(transform.position, targetMovePosition) < arrivedAtPositionDistance) {
            // Reached position
            SetRandomMovePosition();
        }
    }

    private void SetMovePosition(Vector3 movePosition) {
        GetComponent<IMovePosition>().SetMovePosition(movePosition);
    }


}