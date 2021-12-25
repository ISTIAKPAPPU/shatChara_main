using System.Collections;
using System.Collections.Generic;
using MyGame.Utils;
using Unity.Mathematics;
using UnityEngine;

public class CursorHolder : MonoBehaviour
{
    public GameObject pfCursor;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var movePos = UtilsClass.GetMouseWorldPosition();
            var cursor = Instantiate(pfCursor, movePos, quaternion.identity, transform);
            StartCoroutine(DestroyCursor(cursor));
        }

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.tapCount >= 2)
                {
                    var movePos = Camera.main.ScreenToWorldPoint(touch.position);
                    movePos.z = 0;
                    var cursor = Instantiate(pfCursor, movePos, quaternion.identity, transform);
                    StartCoroutine(DestroyCursor(cursor));
                }
            }
        }
    }

    private IEnumerator DestroyCursor(GameObject tempCursor)
    {
        yield return new WaitForSeconds(.2f);
        DestroyImmediate(tempCursor);
    }
}