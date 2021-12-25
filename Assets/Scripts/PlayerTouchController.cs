using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTouchController : MonoBehaviour
{
    public GameObject ball;
    public GameObject ballPrefab;
    public GameObject throwingPoint;
    public Slider slider;

    public int toss;
    public float ballPower;
    private Rigidbody2D rb;
    public LineRenderer line;
    private Camera cam;

    float wait;

    public Vector2 minPower;
    public Vector2 maxPower;

    private Vector2 ballForce;
    private Vector3 startPoint;
    private Vector3 endPoint;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    void Start()
    {
        toss = PlayerPrefs.GetInt("Player_Side");
        cam = Camera.main;
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
    }

    
    void Update()
    {
        //player
        int crushed = PlayerPrefs.GetInt("Crushed");
        if (crushed == 1 && toss == 2)
        {
            if (Input.touchCount == 1) // user is touching the screen with a single touch
            {
                Touch touch = Input.GetTouch(0); // get the touch
                if (touch.phase == TouchPhase.Began) //check for the first touch
                {
                    fp = touch.position;
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
                {
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
                {
                    lp = touch.position;  //last touch position. Ommitted if you use list

                    //Check if drag distance is greater than 20 % of the screen height
                    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                    {//It's a drag
                     //check if the drag is vertical or horizontal
                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {   //If the horizontal movement is greater than the vertical movement...
                            if ((lp.x > fp.x))  //If the movement was to the right)
                            {   //Right swipe
                                Debug.Log("Right Swipe");
                                GetComponent<Animator>().SetBool("isWalking", true);
                                transform.localPosition += new Vector3(1f, 0f, 0f);
                            }
                            else if ((lp.x < fp.x))//If the movement was to the left)
                            {   //Left swipe
                                Debug.Log("Left Swipe");
                                GetComponent<Animator>().SetBool("isWalking", true);
                                transform.localPosition += new Vector3(-1f, 0f, 0f);
                            }
                        }
                        else
                        {   //the vertical movement is greater than the horizontal movement
                            if (lp.y > fp.y)  //If the movement was up
                            {   //Up swipe
                                Debug.Log("Up Swipe");
                                GetComponent<Animator>().SetBool("isWalking", true);
                                transform.localPosition += new Vector3(0f, 1f, 0f);
                            }
                            else
                            {   //Down swipe
                                Debug.Log("Down Swipe");
                                GetComponent<Animator>().SetBool("isWalking", true);
                                transform.localPosition += new Vector3(0f, -1f, 0f);
                            }
                        }
                    }
                    else
                    {   //It's a tap as the drag distance is less than 20% of the screen height
                        Debug.Log("Tap");
                    }
                }
            }
            else
            {
                GetComponent<Animator>().SetBool("isWalking", false);
            }
        }

        if (crushed == 0 && ball.activeSelf && toss == 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                startPoint.z = 15;
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                currentPoint.z = 15;
                slider.value = (currentPoint.x / 10);
                DrawLine(startPoint, currentPoint);
            }
            if (Input.GetMouseButtonUp(0))
            {
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 15;

                GetComponent<Animator>().SetBool("isThrowing", true);
            }
        }

        //ai
        if (crushed == 0 && ball.activeSelf && toss == 1)
        {
            wait += Time.deltaTime;
            if (wait > 5)
            {
                wait = 0;
                GetComponent<Animator>().SetBool("isThrowing", true);
            }
        }

        //if (target == 1 && toss == 1)
        //{
        //    GetComponent<Animator>().SetBool("isWalking", true);
        //    movePos = Random.Range(-3, 3);
        //    transform.localPosition += new Vector3(0f, movePos, 0f);
        //}
        //if (transform.position.y == transform.position.y + movePos)
        //{
        //    GetComponent<Animator>().SetBool("isWalking", false);
        //}
    }

    public void Throw()
    {
        print("Throw");
		//ball.SetActive(false);
        //PlayerPrefs.SetInt("Target", 0);
        //Instantiate(ballPrefab, throwingPoint.transform.position,throwingPoint.transform.rotation);
        //GetComponent<Animator>().SetBool("isThrowing", false);

        ball.SetActive(false);
        var Ball = Instantiate(ballPrefab, throwingPoint.transform.position, throwingPoint.transform.rotation);
        rb = Ball.GetComponent<Rigidbody2D>();
        if (toss == 2)
        {
            gameObject.GetComponent<PlayerController>().arrow.SetActive(false);
            ballForce = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
                Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
        }
        else if(toss == 1)
        {
            float startX = gameObject.transform.position.x;
            float startY = gameObject.transform.position.y;
            float endX = startX + Random.Range(0.5f, 2.5f);
            float endY = startY + Random.Range(-0.5f, 0.5f);
            ballForce = new Vector2(Mathf.Clamp(startX - endX, minPower.x, maxPower.x),
               Mathf.Clamp(startY - endY, minPower.y, maxPower.y));
        }
        rb.AddForce(ballForce * ballPower, ForceMode2D.Impulse);
        PlayerPrefs.SetInt("Ball_Force", (int)Mathf.Abs(Mathf.Round(ballForce.x * ballPower)));
        Debug.Log("BallPower = "+ Mathf.Abs(Mathf.Round(ballForce.x * ballPower)));
        Debug.Log("startPoint = " + startPoint + endPoint);
        EndLine();
        GetComponent<Animator>().SetBool("isThrowing", false);
    }

    public void DrawLine(Vector3 startPoint, Vector3 endPoint)
    {
        line.positionCount = 2;
        Vector3[] allPoint = new Vector3[2];
        allPoint[0] = startPoint;
        allPoint[1] = endPoint;
        line.SetPositions(allPoint);
    }

    public void EndLine()
    {
        line.positionCount = 0;
		 print("Endline");
    }
}
