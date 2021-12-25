using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public GameObject bird;
    public Transform birdNewPos;

    public GameObject[] instructions;
    public GameObject[] stones;
    public GameObject[] player;
    public float[] initialPos;
    public GameObject[] enemyPosition;
    public GameObject[] teacher;

    public GameObject startIns;
    public GameObject ball;
    public GameObject playerBall;

    public GameObject playBtn;
    public GameObject skipBtn;

    public GameObject target;
    public float speed;

    private Camera cam;

    public int moveSpeed;

    float wait;
    float wait2;

    bool zoomOut;
    bool rally;
    bool sortPlayer;
    bool scalePlayer;
    bool sortPlayer2;
    bool scalePlayer2;
    bool sortPlayer3;
    bool scalePlayer3;
    bool playerOut;
    bool collectStone;
    bool buildTower;
    bool back;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        int skip = PlayerPrefs.GetInt("Skip_Btn");
        if (skip == 1)
        {
            skipBtn.SetActive(true);
        }

        StartCoroutine(Rally());
    }

    // Update is called once per frame
    void Update()
    {
        if(bird.transform.position != birdNewPos.position)
        {
            bird.transform.position = Vector2.MoveTowards(bird.transform.position, birdNewPos.position, Time.deltaTime * moveSpeed);
        }

        if (rally)
        {
            //if (!player[0].activeSelf) { player[0].SetActive(true); }
            if (player[0].transform.position.x > initialPos[0])
            {
                player[0].transform.Translate(-0.5f * Time.deltaTime * moveSpeed, 0, 0);
                player[0].GetComponent<Animator>().SetBool("isWalking", true);
            }
            else if (player[0].transform.position.x < initialPos[0])
            {
                player[0].GetComponent<Animator>().SetBool("isWalking", false);
                //if (!player[3].activeSelf) { player[3].SetActive(true); }

                if (player[3].transform.position.x > initialPos[3])
                {
                    player[3].transform.Translate(-0.5f * Time.deltaTime * moveSpeed, 0, 0);
                    player[3].GetComponent<Animator>().SetBool("isWalking", true);
                }
                else if (player[3].transform.position.x < initialPos[3])
                {
                    player[3].GetComponent<Animator>().SetBool("isWalking", false);
                    //if (!player[1].activeSelf) { player[1].SetActive(true); }

                    if (player[1].transform.position.x > initialPos[1])
                    {
                        player[1].transform.Translate(-0.5f * Time.deltaTime * moveSpeed, 0, 0);
                        player[1].GetComponent<Animator>().SetBool("isWalking", true);
                    }
                    else if (player[1].transform.position.x < initialPos[1])
                    {
                        player[1].GetComponent<Animator>().SetBool("isWalking", false);
                        //if (!player[4].activeSelf) { player[4].SetActive(true); }

                        if (player[4].transform.position.x > initialPos[4])
                        {
                            player[4].transform.Translate(-0.5f * Time.deltaTime * moveSpeed, 0, 0);
                            player[4].GetComponent<Animator>().SetBool("isWalking", true);
                        }
                        else if (player[4].transform.position.x < initialPos[4])
                        {
                            player[4].GetComponent<Animator>().SetBool("isWalking", false);
                            //if (!player[2].activeSelf) { player[2].SetActive(true); }

                            if (player[2].transform.position.x > initialPos[2])
                            {
                                player[2].transform.Translate(-0.5f * Time.deltaTime * moveSpeed, 0, 0);
                                player[2].GetComponent<Animator>().SetBool("isWalking", true);
                            }
                            else if (player[2].transform.position.x < initialPos[2])
                            {
                                player[2].GetComponent<Animator>().SetBool("isWalking", false);
                                //if (!player[5].activeSelf) { player[5].SetActive(true); }

                                if (player[5].transform.position.x > initialPos[5])
                                {
                                    player[5].transform.Translate(-0.5f * Time.deltaTime * moveSpeed, 0, 0);
                                    player[5].GetComponent<Animator>().SetBool("isWalking", true);
                                }
                                else if (player[5].transform.position.x < initialPos[5])
                                {
                                    player[5].GetComponent<Animator>().SetBool("isWalking", false);

                                    if (!playerOut)
                                    {
                                        playerOut = true;
                                        rally = false;
                                        StartCoroutine(StartTalk());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        if (collectStone)
        {   
	        print("Collect_Stone_1");
            player[1].GetComponent<Animator>().SetBool("isWalking", true);
            player[1].transform.position = Vector2.MoveTowards(player[1].transform.position, stones[1].transform.position, Time.deltaTime * moveSpeed);

            if(player[1].transform.position == stones[1].transform.position)
            {
                collectStone = false;
                if(player[1].transform.localScale.x != 0.45f)
                {
                    player[1].transform.localScale += new Vector3(0.05f, 0.05f, 1f);
					
	                print("Collect_Stone_2");
                }
                
                player[1].GetComponent<Animator>().SetBool("isWalking", false);
                buildTower = true;
            }
        }
        if (buildTower)
        {
            wait += Time.deltaTime;
            if (wait > 3)
            {
                
	        print("Collect_Stone_3");
				stones[1].SetActive(false);
                player[1].GetComponent<Animator>().SetBool("isWalking", true);
                player[1].transform.rotation = Quaternion.Slerp(player[1].transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 100);
                player[1].transform.position = Vector2.MoveTowards(player[1].transform.position, stones[2].transform.position, Time.deltaTime * moveSpeed);
                if (player[1].transform.position == stones[2].transform.position)
                {
                    player[1].transform.rotation = Quaternion.Slerp(player[1].transform.rotation, Quaternion.Euler(0, 0, 0), 100);
                    player[1].GetComponent<Animator>().SetBool("isWalking", false);
                    buildTower = false;
                    back = true;
	        print("Collect_Stone_4");
                }
            }
        }
        if (back)
        {
            wait2 += Time.deltaTime;
            if (wait > 2 && wait < 10)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 2.5f, speed);
                cam.transform.localScale = Vector3.Lerp(cam.transform.localScale, target.transform.localPosition, speed);

                cam.transform.localPosition = new Vector3(0.51f, -2.75f, -10f);
                stones[0].SetActive(true);
            }
            if (wait2 > 10)
            {   
	        print("Collect_Stone_5");
               player[1].GetComponent<Animator>().SetBool("isWalking", true);
                player[1].transform.position = Vector2.MoveTowards(player[1].transform.position, new Vector2(-0.53f, -1.79f), Time.deltaTime * moveSpeed);
                if (player[1].transform.position == new Vector3(-0.53f, -1.79f, 0))
                {
                    if (player[1].transform.localScale.x != 0.3f)
                    {
                        player[1].transform.localScale -= new Vector3(0.05f, 0.05f, 1f);
                    }
                    back = false;
                    zoomOut = true;
                    player[1].GetComponent<Animator>().SetBool("isWalking", false);
                    StartCoroutine(EndTalk());
	        print("Collect_Stone_6");
                }
            }
        }
        if (zoomOut)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5.1f, speed);
            cam.transform.localScale = Vector3.Lerp(cam.transform.localScale, new Vector3(0f, 0f, -10f), speed);
            cam.transform.localPosition = new Vector3(0f, 0f, -10f);
        }

        if (sortPlayer)
        {
            player[0].GetComponent<Animator>().SetBool("isWalking", true);
            player[3].GetComponent<Animator>().SetBool("isWalking", true);
            player[0].transform.position = Vector2.MoveTowards(player[0].transform.position, enemyPosition[0].transform.position, Time.deltaTime * moveSpeed);
            player[3].transform.position = Vector2.MoveTowards(player[3].transform.position, enemyPosition[3].transform.position, Time.deltaTime * moveSpeed);
            player[3].transform.rotation = Quaternion.Slerp(player[3].transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 100);

            if (player[0].transform.position == enemyPosition[0].transform.position)
            {
                player[0].transform.rotation = Quaternion.Slerp(player[0].transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 100);
                player[0].GetComponent<Animator>().SetBool("isWalking", false);

            }
            if (player[3].transform.position == enemyPosition[3].transform.position)
            {
                player[3].transform.rotation = Quaternion.Slerp(player[3].transform.rotation, Quaternion.Euler(0, 0, 0), 100);
                player[3].GetComponent<Animator>().SetBool("isWalking", false);
            }
            if(player[0].transform.position == enemyPosition[0].transform.position && player[3].transform.position == enemyPosition[3].transform.position)
            {
                sortPlayer = false;
                sortPlayer2 = true;
                scalePlayer = true;
            }
        }
        if (player[0].transform.localScale.x <= 0.45f && scalePlayer)
        {
            player[0].transform.localScale += new Vector3(0.05f, 0.05f, 1f);
            player[3].transform.localScale += new Vector3(0.05f, 0.05f, 1f);
        }
        if (sortPlayer2)
        {
	        print("Collect_Stone_7");
            player[1].GetComponent<Animator>().SetBool("isWalking", true);
            player[4].GetComponent<Animator>().SetBool("isWalking", true);
            player[1].transform.position = Vector2.MoveTowards(player[1].transform.position, enemyPosition[1].transform.position, Time.deltaTime * moveSpeed);
            player[4].transform.position = Vector2.MoveTowards(player[4].transform.position, enemyPosition[4].transform.position, Time.deltaTime * moveSpeed);
            player[4].transform.rotation = Quaternion.Slerp(player[4].transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 100);

            if (player[1].transform.position == enemyPosition[1].transform.position)
            {
                player[1].transform.rotation = Quaternion.Slerp(player[1].transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 100);
                player[1].GetComponent<Animator>().SetBool("isWalking", false);
            }
            if (player[4].transform.position == enemyPosition[4].transform.position)
            {
                player[4].transform.rotation = Quaternion.Slerp(player[4].transform.rotation, Quaternion.Euler(0, 0, 0),  100);
                player[4].GetComponent<Animator>().SetBool("isWalking", false);
            }
            if (player[1].transform.position == enemyPosition[1].transform.position && player[4].transform.position == enemyPosition[4].transform.position)
            {
                sortPlayer2 = false;
                scalePlayer2 = true;
                sortPlayer3 = true;
            }
        }
        if (player[1].transform.localScale.x <= 0.45f && scalePlayer2)
        {
            player[1].transform.localScale += new Vector3(0.05f, 0.05f, 1f);
            player[4].transform.localScale += new Vector3(0.05f, 0.05f, 1f);
        }
        if (sortPlayer3)
        {
            player[2].GetComponent<Animator>().SetBool("isWalking", true);
            player[5].GetComponent<Animator>().SetBool("isWalking", true);
            player[2].transform.position = Vector2.MoveTowards(player[2].transform.position, enemyPosition[2].transform.position, Time.deltaTime * moveSpeed);
            player[5].transform.position = Vector2.MoveTowards(player[5].transform.position, enemyPosition[5].transform.position, Time.deltaTime * moveSpeed);
            player[5].transform.rotation = Quaternion.Slerp(player[5].transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 100);

            if (player[2].transform.position == enemyPosition[2].transform.position)
            {
                player[2].transform.rotation = Quaternion.Slerp(player[2].transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 100);
                player[2].GetComponent<Animator>().SetBool("isWalking", false);
            }
            if (player[5].transform.position == enemyPosition[5].transform.position)
            {
                player[5].transform.rotation = Quaternion.Slerp(player[5].transform.rotation, Quaternion.Euler(0, 0, 0), 100);
                player[5].GetComponent<Animator>().SetBool("isWalking", false);
            }
            if (player[2].transform.position == enemyPosition[2].transform.position && player[5].transform.position == enemyPosition[5].transform.position)
            {
                sortPlayer3 = false;
                scalePlayer3 = true;
                teacher[2].transform.localScale = new Vector3(0.3f, 0.3f, 1f);
                playerBall.SetActive(true);
                playBtn.SetActive(true);
            }
        }
        if (player[2].transform.localScale.x <= 0.45f && scalePlayer3)
        {
            player[2].transform.localScale += new Vector3(0.05f, 0.05f, 1f);
            player[5].transform.localScale += new Vector3(0.05f, 0.05f, 1f);
        }
    }

    IEnumerator StartTalk()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 14; i++)
        {
            instructions[i].SetActive(true);
            yield return new WaitForSeconds(4f);
            instructions[i].SetActive(false);
        }
        collectStone = true;
    }

    IEnumerator EndTalk()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 14; i < instructions.Length; i++)
        {
            if (i == 15)
            {
                ball.SetActive(true);
            }
            instructions[i].SetActive(true);
            yield return new WaitForSeconds(4f);
            instructions[i].SetActive(false);
        }
        ball.SetActive(false);
        sortPlayer = true;
    }

    IEnumerator Rally()
    {
        yield return new WaitForSeconds(3f);
        startIns.SetActive(true);
        yield return new WaitForSeconds(1f);
        teacher[0].SetActive(false);
        teacher[1].SetActive(true);
        rally = true;
        yield return new WaitForSeconds(3f);
        startIns.SetActive(false);
    }

    public void OnClickPlay()
    {
        PlayerPrefs.SetInt("Menu_Stat", 0);
        PlayerPrefs.SetInt("Skip_Btn", 1);
        SceneManager.LoadScene("Menu");
    }
}
