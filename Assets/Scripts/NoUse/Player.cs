using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public static bool finishedGame;
    public static int scoreRecord;

    public static float upLimit = 4.57f;
    public static float downLimit = -4.26f;
    public static float leftLimit = 11.0f;
    public static float rightLimit = 18.0f;

    public GameObject[] playerCubes;
    public float[] cubesPositions;
    public int numberLevel;
    public static int currentLevel;
    public GameObject allCubes;


    public float[] lastPositions;
    public bool[] finishMovement;
    private bool lastPosAssigned = false;
    
    private float speed = 10.0f;

    public static bool cubesLine = false;

    // Use this for initialization
    void Start()
    {
        currentLevel = numberLevel;
        finishedGame = false;
        scoreRecord = PlayerPrefs.GetInt("score record "+currentLevel);
        Debug.Log("Current record: " + scoreRecord);
    }

    // Update is called once per frame
    void Update()
    {


        if (finishedGame == true)
        {
            int i;
            bool line = true;

            if (lastPosAssigned == false)
            {
                lastPositionCubes();
            }

            

            for (i = 0; i < playerCubes.Length; i++)
            {
                //this.transform.position = new Vector3(cubesPositions[i], -1.5f, 0f);

                if (lastPositions[i] < cubesPositions[i])
                {
                    if (playerCubes[i].transform.position.x <= cubesPositions[i])
                    {
                        playerCubes[i].transform.Translate(speed * Time.deltaTime, 0, 0);
                    }
                    else
                    {
                        finishMovement[i] = true;
                    }
                }

                if (lastPositions[i] > cubesPositions[i])
                {
                    if (playerCubes[i].transform.position.x >= cubesPositions[i])
                    {
                        playerCubes[i].transform.Translate(-speed * Time.deltaTime, 0, 0);
                    }
                    else
                    {
                        finishMovement[i] = true;
                    }
                }

                if (lastPositions[i] == cubesPositions[i])
                {
                    finishMovement[i] = true;
                }
            }

            
            for (i = 0; i < finishMovement.Length; i++)
            {
                if (finishMovement[i] == false)
                {
                    line = false;
                }

            }

            if (line == true)
            {
                cubesLine = true;
                Animator animator = allCubes.GetComponent<Animator>();
                animator.enabled = true;
                if (animator != null)
                {
                    animator.SetBool("finishedGame", true);
                }
            }
        }     
    }


    void lastPositionCubes()
    {
        int i;
        for (i = 0; i < playerCubes.Length; i++)
        {
            //this.transform.position = new Vector3(cubesPositions[i], -1.5f, 0f);
            lastPositions[i] = playerCubes[i].transform.position.x;

        }

        lastPosAssigned = true;
    }




}

