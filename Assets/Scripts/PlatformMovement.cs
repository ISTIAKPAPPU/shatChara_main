using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public char movementAxis;
    public float moveSpeed;
    
    float platformPosX;
    float platformPosY;
    int toss;
    int crushed;

    // Start is called before the first frame update
    void Start()
    {
        platformPosX = gameObject.transform.position.x;
        platformPosY = gameObject.transform.position.y;
        toss = PlayerPrefs.GetInt("Player_Side");
    }

    // Update is called once per frame
    void Update()
    {
        crushed = PlayerPrefs.GetInt("Crushed");
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(0.5f);

        if (gameObject.name == "platform" && crushed == 0)
        {
            if (movementAxis == 'y' || movementAxis == 'Y')
            {
                if (transform.position.y < 1.3)
                {
                    transform.Translate(0, 0.5f * Time.deltaTime * moveSpeed, 0);
                }
                else if (transform.position.y > -3.75)
                {
                    transform.Translate(0, -0.5f * Time.deltaTime * moveSpeed, 0);
                }
            }

            if (movementAxis == 'x' || movementAxis == 'X')
            {
                if (transform.position.x < 1.34)
                {
                    transform.Translate(0.5f * Time.deltaTime * moveSpeed, 0, 0);
                }
                else if (transform.position.x > -4.4)
                {
                    transform.Translate(-0.5f * Time.deltaTime * moveSpeed, 0, 0);
                }
            }
        }
        else if(crushed == 0)
        {
            if (movementAxis == 'y' || movementAxis == 'Y')
            {
                if (transform.position.y < platformPosY + 1.1)
                {
                    transform.Translate(0, 0.5f * Time.deltaTime * moveSpeed, 0);
                }
                else if (transform.position.y > platformPosY - 1.1)
                {
                    transform.Translate(0, -0.5f * Time.deltaTime * moveSpeed, 0);
                }
            }
            if (movementAxis == 'x' || movementAxis == 'X')
            {
                if (transform.position.x < platformPosX + 1.8)
                {
                    transform.Translate(0.5f * Time.deltaTime * moveSpeed, 0, 0);
                }
                else if (transform.position.x > platformPosX - 1.8)
                {
                    transform.Translate(-0.5f * Time.deltaTime * moveSpeed, 0, 0);
                }
            }
        }
    }
}
