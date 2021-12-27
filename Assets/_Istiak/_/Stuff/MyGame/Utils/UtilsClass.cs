using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Utils
{
    public class UtilsClass : MonoBehaviour
    {
        // Get Mouse Position in World with Z = 0f
        public const int sortingOrderDefault = 5000;
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }

        public static Vector3 GetMouseWorldPositionWithZ()
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        }

        public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
        }

        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }
        
        public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault) {
            if (color == null) color = Color.white;
            return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
        }
        public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder) {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }
        // Generate random normalized direction
        public static Vector3 GetRandomDir() {
            return new Vector3(UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f,1f)).normalized;
        }
        public static Vector3 GetEndPosition(Vector3 initialPosition, Vector3 middlePosition)
        {
            var isInitXPositive = !(initialPosition.x < 0);
            var isInitYPositive = !(initialPosition.y < 0);
            var isMidYPositive = !(middlePosition.y < 0);
            Vector3 endPos;

            switch (isInitXPositive)
            {
                case true when isInitYPositive:
                {
                    var yPos = Mathf.Abs(initialPosition.y) - middlePosition.y;
                    endPos = new Vector3(Mathf.Abs(initialPosition.x), Mathf.Abs(yPos), 0);
                    if (isMidYPositive)
                    {
                        endPos.x *= -1;
                        endPos.y *= -1;
                    }
                    else
                    {
                        endPos.x *= -1;
                        endPos.y *= -1;
                    }

                    break;
                }
                case true when !isMidYPositive:
                {
                    if (Mathf.Abs(initialPosition.y) > 35)
                    {
                        initialPosition.y = 30;
                    }
                    var yPos = Mathf.Abs(initialPosition.y) + middlePosition.y;
                    endPos = new Vector3(Mathf.Abs(initialPosition.x), Mathf.Abs(yPos), 0);
                    endPos.x *= -1;
                    break;
                }
                case true:
                {
                    if (Mathf.Abs(initialPosition.y) > 35)
                    {
                        initialPosition.y = 30;
                    }
                    var yPos = Mathf.Abs(initialPosition.y) + middlePosition.y;
                    endPos = new Vector3(Mathf.Abs(initialPosition.x), Mathf.Abs(yPos), 0);
                    endPos.x *= -1;
                    break;
                }
                default:
                {
                    if (!isInitYPositive)
                    {
                        var yPos = Mathf.Abs(initialPosition.y) + middlePosition.y;
                        endPos = new Vector3(Mathf.Abs(initialPosition.x), Mathf.Abs(yPos), 0);
                    }
                    else
                    {
                        var yPos = Mathf.Abs(initialPosition.y) - middlePosition.y;
                        endPos = new Vector3(Mathf.Abs(initialPosition.x), Mathf.Abs(yPos), 0);
                    }

                    endPos.y *= -1;
                    break;
                }
            }

            return endPos;
        }
    }
}