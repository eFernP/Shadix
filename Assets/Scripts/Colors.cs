using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{

    // Use this for initialization

    //public float h, s, v;

    public const int NUMBER_COLORS = 15;
    public const float INCREASE_COLOR = 0.067f;

    public static float[] hueColor;
    public static float[] lightColor;
    

    public int numH;
    public int numL;
    

    void Start()
    {

        //Starts a list for all the possible hues and another list for the light intensity
        hueColor = new float[NUMBER_COLORS];

        float hNumber = 0;
        for (int i = 0; i < hueColor.Length; i++)
        {
            hueColor[i] = hNumber;
            hNumber += INCREASE_COLOR;
        }
        lightColor = new float[5];

        lightColor[0] = 0.2f;
        lightColor[1] = 0.4f;
        lightColor[2] = 0.6f;
        lightColor[3] = 0.75f;
        lightColor[4] = 0.55f;

        chooseColor();
        //defaultColor();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void chooseColor()
    {
        float h, s, v;

        //Assign a random color for the cube according the hue and light lists
        numH = Random.Range(0, NUMBER_COLORS);
        numL = Random.Range(0, 5);

        h = hueColor[numH];


        if (numL < 3)
        {
            s = lightColor[numL];
            v = 0.95f;
        }
        else
        {
            s = 0.6f;
            v = lightColor[numL];
        }


        this.GetComponent<Renderer>().material.color = Color.HSVToRGB(h, s, v);


    }

    
    void defaultColor()
    {
        float h, s, v;

        numH = 10;
        numL = 3;

        h = hueColor[numH];


        if (numL < 3)
        {
            s = lightColor[numL];
            v = 0.95f;
        }
        else
        {
            s = 0.6f;
            v = lightColor[numL];
        }


        this.GetComponent<Renderer>().material.color = Color.HSVToRGB(h, s, v);

    }

}