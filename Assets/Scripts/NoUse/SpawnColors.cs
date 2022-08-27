using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColors : MonoBehaviour {


    private float timer;
    public float colorsWaitingTime = 2.0f;
    public GameObject colorBall;

    //public int randomColors;
    public int whiteProb;
    public int blackProb;
    public int cyanProb;
    public int magentaProb;
    public int yellowProb;
    public float minScale;
    public float maxScale;

    public int numCubes;

    public int[] cubesHistory;
    private int indexColor = 0;

    // Use this for initialization
    void Start () {
        timer = 0.0f;

        Player.finishedGame = false;
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (timer >= colorsWaitingTime)
        {
            timer = 0f;

            if (Player.finishedGame == false)
            {
                spawn();
            }
            

        }

    }

    void spawn()
    {

        float scale;

        GameObject obj = Instantiate(colorBall) as GameObject;
        Renderer rend = obj.GetComponent<Renderer>();
        //rend.material.color = Random.ColorHSV(0f, 1f, 0f, 0f, 0f, 1f);
        int numColor = Random.Range(0, 100);

        

        if (numColor < whiteProb)
        {
            rend.material.color = Color.white;

            cubesHistory[indexColor] = 1;
        }

        else if (whiteProb < numColor && numColor < blackProb+whiteProb)
        {
            rend.material.color = Color.black;
            
            cubesHistory[indexColor] = 2;

        }

        else if (blackProb + whiteProb < numColor && numColor < blackProb + whiteProb +cyanProb)
        {

            rend.material.color = Color.cyan;
            
            cubesHistory[indexColor] = 3;
        }

        else if (blackProb + whiteProb+cyanProb < numColor && numColor < blackProb + whiteProb + cyanProb+ magentaProb)
        {

            rend.material.color = Color.magenta;
            
            cubesHistory[indexColor] = 4;
        }

        else if (blackProb + whiteProb + cyanProb + magentaProb < numColor && numColor < 100)
        {
            rend.material.color = Color.yellow;
            
            cubesHistory[indexColor] = 5;

        }


        if (whiteProb >= 20)
        {
            
            checkCubesHistory(1, rend, Color.white);
            

        }
        if (blackProb >= 20)
        {
             checkCubesHistory(2, rend, Color.black);
            
        }
        if (cyanProb >= 20)
        {
            
            checkCubesHistory(3, rend, Color.cyan);
            
        }
        if (magentaProb >= 20)
        {
            
            checkCubesHistory(4, rend, Color.magenta);
            
        }
        if (yellowProb >= 20)
        {
            
            checkCubesHistory(5, rend, Color.yellow);
            
        }

        increaseIndex();

        /*int numColor = Random.Range(0, randomColors);

        if (numColor == 0)
        {
            rend.material.color = Color.white;
        }

        else if(numColor == 1)
        {
            rend.material.color = Color.black;
        }

        else if (numColor == 2)
        {
            
            rend.material.color = Color.cyan;
        }

        else if (numColor == 3)
        {

            rend.material.color = Color.magenta;
        }

        else if (numColor == 4)
        {
            rend.material.color = Color.yellow;
            
        }

        */

        scale = Random.Range(0, 10);

        if (scale < 3)
        {
            scale = minScale;
        } else if (scale > 3 && scale < 8)
        {
            scale = (minScale + maxScale) / 2; 
        } else
        {
            scale = maxScale;
        }


        obj.transform.localScale= new Vector3(scale, scale, scale);

        float positionBall = Random.Range(0, numCubes);

        if (numCubes == 2)
        {
            if (positionBall == 0)
            {
                positionBall = -0.75f;
            }
            else
            {
                positionBall = 0.75f;
            }
        }

        else if (numCubes == 3)
        {
            if (positionBall == 0)
            {
                positionBall = -1.5f;
            }
            else if (positionBall ==1)
            {
                positionBall = 0f;
            }
            else
            {
                positionBall = 1.5f;
            }
        }

        else if (numCubes == 4)
        {
            if (positionBall == 0)
            {
                positionBall = -2.25f;
            }
            else if (positionBall == 1)
            {
                positionBall = -0.75f;
            }
            else if(positionBall == 2)
            {
                positionBall = 0.75f;
            }
            else
            {
                positionBall = 2.25f;
            }
        }


        obj.transform.position = new Vector3(positionBall, 20, 0);
        obj.name = "ball";

    }

    void increaseIndex()
    {
        if (indexColor < 4)
        {
            indexColor++;
        }
        else
        {
            indexColor = 0;
        }
    }

    void checkCubesHistory(int color, Renderer rend, Color colorObj)
    {
        bool foundColor = false;

        for (int i = 0; i<5; i++)
        {
            if (color == cubesHistory[i])
            {
                foundColor = true;
            }
          
        }
        
        if (foundColor == false)
        {
            rend.material.color = colorObj;
            cubesHistory[indexColor] = color;
        }

        

    }
}
