using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Combination : MonoBehaviour {

    
    public GameObject firstCube, lastCube;

    private Color firstColor, lastColor;


    
    public static bool levelCompleted;

    public float differenceR, differenceG, differenceB;
    public float differenceH, differenceS, differenceV;

    public GameObject[] playerCubes;
    public bool correctR, correctG, correctB;

    //public float firstR, firstG, firstB;
    //public float lastR, lastG, lastB;

    public float fh, fs, fv, lh, ls, lv;

    public float[] rgbCubes;
    public float[] hsvCubes;


    public bool correctGame;
    public int correctCubes;

    public Color[] color;


    public Text timeText;

    
    private float timer;
    private float waitingTime = 1.0f;

    private int levelScore;


    public int numTime = 100;

    public float hPrecision;
    public float sPrecision;
    public float vPrecision;


    public int minScore = 200;

    public Text[] scoreCubesText;
    public Text totalScoreText;
    public Text scoreRecordText;
    public Text scoreRecordFailedText;

    public GameObject completedScreen;
    public GameObject failedScreen;

    public Button finishButton;
    public GameObject pausePanel;

    public static bool isPaused;

    // Use this for initialization
    void Start () {

        firstColor = firstCube.GetComponent<Renderer>().material.color;
        lastColor = lastCube.GetComponent<Renderer>().material.color;
        isPaused = false;
        

        /*
        firstR = firstColor.r;
        firstG = firstColor.g;
        firstB = firstColor.b;

        lastR = lastColor.r;
        lastG = lastColor.g;
        lastB = lastColor.b;
        */


        levelScore = 0;
        correctGame = false;

        calculateDifference();

        timeText.text = "" + numTime;
        timer = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (timer >= waitingTime && Player.finishedGame == false && isPaused == false)
        {
            timer = 0f;
            numTime--;

            timeText.text = "" + numTime;

            if (numTime <= 0)
            {
                finishGame();
            }
        }

        /*
        if (correctGame != true)
        {
            checkPrecisionHSV();
        }

        else { 

            completedText.text = ""+ levelScore + " points";
            SpawnColors.finishedGame = true;
        }
        */

    }


    void calculateDifference()
    {

        int i, j;

        Color.RGBToHSV(firstColor, out fh, out fs, out fv);
        Color.RGBToHSV(lastColor, out lh, out ls, out lv);


        

        if (Mathf.Abs(lh - fh) < 1 - Mathf.Abs(lh - fh))
        {
            differenceH = (lh - fh) / (playerCubes.Length + 1);
        }

        else
        {
            if (fh > lh)
            {
                differenceH = (1 - Mathf.Abs(lh - fh)) / (playerCubes.Length + 1);
            }

            else
            {
                differenceH = (Mathf.Abs(lh - fh)-1) / (playerCubes.Length + 1);
            }
            
        }

        
        differenceS = (ls-fs) / (playerCubes.Length + 1);
        differenceV = (lv-fv) / (playerCubes.Length + 1);

        j = 1;

        for (i = 0; i < playerCubes.Length * 3; i = i + 3)
        {
            hsvCubes[i] = fh + differenceH * (j);

            if (hsvCubes[i] > 1.0f)
            {
                hsvCubes[i] = 0.0f + (hsvCubes[i] - 1);
            }

            if (hsvCubes[i] < 0.0f)
            {
                hsvCubes[i] = 1.0f - (0 - hsvCubes[i]);
            }

            hsvCubes[i + 1] = fs + differenceS * (j);
            hsvCubes[i + 2] = fv + differenceV * (j);
            

            color[j-1] = Color.HSVToRGB(hsvCubes[i], hsvCubes[i + 1], hsvCubes[i + 2]);

            j++;
        }


        //RGB VERSION
        /*
        int i, j; 

        differenceR = (lastColor.r - firstColor.r) / (playerCubes.Length+1);
        differenceG = (lastColor.g - firstColor.g) / (playerCubes.Length+1);
        differenceB = (lastColor.b - firstColor.b) / (playerCubes.Length+1);

        j = 1;

        for (i = 0; i < playerCubes.Length*3; i=i+3)
        {
            rgbCubes[i] = firstColor.r + differenceR * (j);
            rgbCubes[i+1] = firstColor.g + differenceG * (j);
            rgbCubes[i+2] = firstColor.b + differenceB * (j);
            j++;
        }


        */
    }


    public void checkPrecisionHSV()
    {

        int i, j;

        j = 0;

        correctCubes = 0;
        levelScore = 0;

        for (i = 0; i < playerCubes.Length; i++)
        {
            float colorH, colorS, colorV, pH, pS, pV;

            Color color = playerCubes[i].GetComponent<Renderer>().material.color;


            Color.RGBToHSV(color, out colorH, out colorS, out colorV);


            if (Mathf.Abs(hsvCubes[j] - colorH) < 1 - Mathf.Abs(hsvCubes[j] - colorH))
            {
                pH = Mathf.Abs(hsvCubes[j] - colorH);
            }

            else
            {
                if (hsvCubes[j]<colorH)
                {

                    pH = hsvCubes[j] + (Mathf.Abs(1 - colorH));
                }

                else
                {
                    pH = colorH + (Mathf.Abs(1 - hsvCubes[j]));
                }

            }

            if (colorS > 0)
            {
                if (pH == 0f)
                {
                    levelScore = levelScore + 1000;
                    scoreCubesText[j].text = "1000 points";
                    correctCubes++;
                }

                else if (pH < hPrecision/5)
                {
                    levelScore = levelScore + 500;
                    scoreCubesText[j].text = "500 points";
                    correctCubes++;

                }

                else if (pH < hPrecision/4)
                {
                    levelScore = levelScore + 400;
                    scoreCubesText[j].text = "400 points";
                    correctCubes++;

                }

                else if (pH < hPrecision/3)
                {
                    levelScore = levelScore + 300;
                    scoreCubesText[j].text = "300 points";
                    correctCubes++;

                }

                else if (pH < hPrecision/2)
                {
                    levelScore = levelScore + 200;
                    scoreCubesText[j].text = "200 points";
                    correctCubes++;

                }

                else if (pH < hPrecision)
                {
                    levelScore = levelScore + 100;
                    scoreCubesText[j].text = "100 points";
                    correctCubes++;

                }


            }

            else
            {
                levelScore = levelScore + 1000;
                scoreCubesText[j].text = "1000 points";
                correctCubes++;
            }

            pS = Mathf.Abs(hsvCubes[j + 1] - colorS);

            
            if (i == 0)
            {
                if (fs < ls)
                {
                    if (colorS + sPrecision < fs)
                    {
                        pS = sPrecision + 1;
                    }

                }
                else if (fs > ls)
                {
                    if (colorS - sPrecision > fs)
                    {
                        pS = sPrecision + 1;
                    }

                }

            }

            else if (i == playerCubes.Length - 1)
            {
                if (fs < ls)
                {
                    if (colorS - sPrecision > ls)
                    {
                        pS = sPrecision + 1;
                    }

                }
                else if (fs > ls)
                {
                    if (colorS + sPrecision < ls)
                    {
                        pS = sPrecision + 1;
                    }

                }

            }


            if (pS == 0f)
            {
                levelScore = levelScore + 1000;
                scoreCubesText[j + 1].text = "1000 points";
                correctCubes++;
            }

            else if (pS < sPrecision / 5)
            {
                levelScore = levelScore + 500;
                scoreCubesText[j+1].text = "500 points";
                correctCubes++;

            }

            else if (pS < sPrecision / 4)
            {
                levelScore = levelScore + 400;
                scoreCubesText[j + 1].text = "400 points";
                correctCubes++;

            }

            else if (pS < sPrecision / 3)
            {
                levelScore = levelScore + 300;
                scoreCubesText[j + 1].text = "300 points";
                correctCubes++;

            }

            else if (pS < sPrecision / 2)
            {
                levelScore = levelScore + 200;
                scoreCubesText[j + 1].text = "200 points";
                correctCubes++;

            }

            else if (pS < sPrecision)
            {
                levelScore = levelScore + 100;
                scoreCubesText[j + 1].text = "100 points";
                correctCubes++;

            }

            pV = Mathf.Abs(hsvCubes[j + 2] - colorV);

            
            if (i == 0)
            {
                if (fv < lv)
                {
                    if (colorV + vPrecision < fv)
                    {
                        pV = vPrecision + 1;
                    }

                }
                else if (fv > lv)
                {
                    if (colorV - vPrecision > fv)
                    {
                        pV = vPrecision + 1;
                    }

                }

            }

            else if (i == playerCubes.Length - 1)
            {
                if (fv < lv)
                {
                    if (colorV - vPrecision > lv)
                    {
                        pV = vPrecision + 1;
                    }

                }
                else if (fv > lv)
                {
                    if (colorV + vPrecision < lv)
                    {
                        pV = vPrecision + 1;
                    }

                }

            }
            

            if (pV == 0f)
            {
                levelScore = levelScore + 1000;
                scoreCubesText[j + 2].text = "1000 points";
                correctCubes++;
            }

            else if (pV < vPrecision / 5)
            {
                levelScore = levelScore + 500;
                scoreCubesText[j + 2].text = "500 points";
                correctCubes++;

            }

            else if (pV < vPrecision / 4)
            {
                levelScore = levelScore + 400;
                scoreCubesText[j + 2].text = "400 points";
                correctCubes++;

            }

            else if (pV < vPrecision / 3)
            {
                levelScore = levelScore + 300;
                scoreCubesText[j + 2].text = "300 points";
                correctCubes++;

            }

            else if (pV < vPrecision / 2)
            {
                levelScore = levelScore + 200;
                scoreCubesText[j + 2].text = "200 points";
                correctCubes++;

            }

            else if (pV < vPrecision)
            {
                levelScore = levelScore + 100;
                scoreCubesText[j + 2].text = "100 points";
                correctCubes++;

            }



            j = j + 3;


        }


        //completedText.text = "" + levelScore;

    }


    public void finishGame()
    {
        completedScreen.gameObject.SetActive(true);
        checkPrecisionHSV();
        Player.finishedGame = true;

       
            if (correctCubes == playerCubes.Length * 3)
            {

                totalScoreText.text = "Total score: " + levelScore + " points";

                if (levelScore > Player.scoreRecord)
                {
                    scoreRecordText.text = "New record!";
                    Player.scoreRecord = levelScore;
                    PlayerPrefs.SetInt("score record " + Player.currentLevel, Player.scoreRecord);
                }

                else
                {
                    scoreRecordText.text = "Current record: " + Player.scoreRecord + " points";
                }
                failedScreen.gameObject.SetActive(false);
                completedScreen.gameObject.SetActive(true);
                Animator animator = completedScreen.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetBool("completedGame", true);
                }
        }

            else
            {


                if (Player.scoreRecord > 0)
                {
                    scoreRecordFailedText.text = "Current record: " + Player.scoreRecord + " points";
                }

                else
                {
                    scoreRecordFailedText.text = "";
                }
                completedScreen.gameObject.SetActive(false);
                failedScreen.gameObject.SetActive(true);
                Animator animator = failedScreen.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetBool("failedGame", true);
                }

            }

        

    }

    public void retryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void pause()
    {
        if (Player.finishedGame == false)
        {
            Time.timeScale = 0;
            isPaused = true;
            pausePanel.gameObject.SetActive(true);
            finishButton.interactable = false;
        }

    }


    public void continueGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pausePanel.gameObject.SetActive(false);
        finishButton.interactable = true;
    }

    public void checkPrecision()
    {



        Player.finishedGame = true;

        int i, j;

        j = 0;
      

        for (i = 0; i < playerCubes.Length; i++)
        {
            float pR, pG, pB;
            correctR = false;
            correctG = false;
            correctB = false;

            Color color = playerCubes[i].GetComponent<Renderer>().material.color;

            pR = Mathf.Abs(rgbCubes[j] - color.r);
            pG = Mathf.Abs(rgbCubes[j + 1] - color.g);
            pB = Mathf.Abs(rgbCubes[j + 2] - color.b);

            if (pR <= 0.20f)
            {

                
            }

            if (pG <= 0.20f)
            {

            }

            if (pB <= 0.20f)
            {

            }

            /*
            if (correctR == true && correctG == true && correctB == true)
            {
                correctCubes++;
            }*/

            j = j + 3;


        }

        /*
        for (i = 0; i < precisionCubes.Length; i++)
        {
            if (precisionCubes[i] < 0.2f)
            {
                
                correctCubes++;

                if (precisionCubes[i] == 0)
                {
                    levelScore = levelScore + 20.0f;
                }

                else
                {
                    levelScore = levelScore + (1 / precisionCubes[i]);
                }
            }


        completedText.text = ""+ levelScore;
        }*/
        /*
        if (correctCubes == playerCubes.Length)
        {
            correctGame = true;
        }

        else
        {
            correctCubes = 0;
        }
        */

        
    }
    

    










    void checkCombination() {

        int i, j;

        j = 0;

        for (i = 0; i < playerCubes.Length; i++)
        {

            correctR = false;
            correctG = false;
            correctB = false;

            Color color = playerCubes[i].GetComponent<Renderer>().material.color;

            if (differenceR < 0 && differenceR < -0.1f)
            {
                if (i > 0 && i < playerCubes.Length - 1)
                {
                    if (color.r < rgbCubes[j - 3])
                    {
                        correctR = true;
                    }
                }

                else if (i == playerCubes.Length - 1)
                {
                    if (color.r < rgbCubes[j - 3] && color.r > lastColor.r)
                    {
                        correctR = true;
                    }

                }

                else
                {
                    if (color.r < firstColor.r)
                    {
                        correctR = true;
                    }

                }

            }

            else if (differenceR > 0 && differenceR > 0.1f)
            {
                if (i > 0 && i < playerCubes.Length - 1)
                {
                    if (color.r > rgbCubes[j - 3])
                    {
                        correctR = true;
                    }
                }

                else if (i == playerCubes.Length - 1)
                {
                    if (color.r > rgbCubes[j - 3] && color.r < lastColor.r)
                    {
                        correctR = true;
                    }

                }

                else
                {
                    if (color.r > firstColor.r)
                    {
                        correctR = true;
                    }

                }
            }

            else if (differenceR == 0)
            {

                if (color.r > rgbCubes[j] - 0.25 && color.r < rgbCubes[j] + 0.25)
                {
                    correctR = true;
                }

            }

            else
            {
                correctR = true;
            }



            if (differenceG < 0 && differenceG < -0.1f)
            {
                if (i > 0 && i < playerCubes.Length - 1)
                {
                    if (color.g < rgbCubes[j - 2])
                    {
                        correctG = true;
                    }
                }

                else if (i == playerCubes.Length - 1)
                {
                    if (color.g < rgbCubes[j - 2] && color.g > lastColor.g)
                    {
                        correctG = true;
                    }

                }

                else
                {
                    if (color.g < firstColor.g)
                    {
                        correctG = true;
                    }

                }

            }

            else if (differenceG > 0 && differenceG > 0.1f)
            {
                if (i > 0 && i < playerCubes.Length - 1)
                {
                    if (color.g > rgbCubes[j - 2])
                    {
                        correctG = true;
                    }
                }

                else if (i == playerCubes.Length - 1)
                {
                    if (color.g > rgbCubes[j - 2] && color.g < lastColor.g)
                    {
                        correctG = true;
                    }

                }

                else
                {
                    if (color.g > firstColor.g)
                    {
                        correctG = true;
                    }

                }
            }

            else if (differenceG == 0)
            {

                if (color.g > firstColor.g - 0.25 && color.g < firstColor.g + 0.25)
                {
                    correctG = true;
                }

            }

            else
            {
                correctG = true;
            }



            if (differenceB < 0 && differenceB < -0.1f)
            {
                if (i > 0 && i < playerCubes.Length - 1)
                {
                    if (color.b < rgbCubes[j - 1])
                    {
                        correctB = true;
                    }
                }

                else if (i == playerCubes.Length - 1)
                {
                    if (color.b < rgbCubes[j - 1] && color.b > lastColor.b)
                    {
                        correctB = true;
                    }

                }

                else
                {
                    if (color.b < firstColor.b)
                    {
                        correctB = true;
                    }

                }

            }

            else if (differenceB > 0 && differenceB > 0.1f)
            {
                if (i > 0 && i < playerCubes.Length - 1)
                {
                    if (color.b > rgbCubes[j - 1])
                    {
                        correctB = true;
                    }
                }

                else if (i == playerCubes.Length - 1)
                {
                    if (color.b > rgbCubes[j - 1] && color.b < lastColor.b)
                    {
                        correctB = true;
                    }

                }

                else
                {
                    if (color.b > firstColor.b)
                    {
                        correctB = true;
                    }

                }
            }

            else if (differenceB == 0)
            {

                if (color.b > firstColor.b - 0.25 && color.b < firstColor.b + 0.25)
                {
                    correctB = true;
                }

            }

            else
            {
                correctB = true;
            }



            if (correctB == true && correctG == true && correctR == true)
            {
                correctCubes++;
            }

            j = j + 3;

        }

        if (correctCubes == playerCubes.Length)
        {
            correctGame = true;
        }

        else
        {
            correctCubes = 0;
        }




















    }

    


                /*
                int i, j;

                j = 0;

                for (i = 0; i < playerCubes.Length; i++)
                {

                    correctR = false;
                    correctG = false;
                    correctB = false;

                    Color color = playerCubes[i].GetComponent<Renderer>().material.color;

                    if (color.r > rgbCubes[j] - 0.25 && color.r < rgbCubes[j] + 0.25)
                    {
                        if (differenceR < 0 && differenceR < -0.1f)
                        {
                            if (i > 0)
                            {
                                if (color.r < rgbCubes[j - 3])
                                {
                                    correctR = true;
                                }
                            }

                            else
                            {
                                if (color.r < firstColor.r)
                                {
                                    correctR = true;
                                }

                            }

                        }

                        else if (differenceR > 0 && differenceR > 0.1f)
                        {
                            if (i > 0)
                            {
                                if (color.r > rgbCubes[j - 3])
                                {
                                    correctR = true;
                                }
                            }

                            else
                            {
                                if (color.r > firstColor.r)
                                {
                                    correctR = true;
                                }

                            }

                        }

                        else
                        {
                            correctR = true;
                        }
                    }



                    if (color.g > rgbCubes[j + 1] - 0.25 && color.g < rgbCubes[j + 1] + 0.25)
                    {
                        if (differenceG < 0 && differenceG < -0.1f)
                        {
                            if (i > 0)
                            {
                                if (color.g < rgbCubes[j - 2])
                                {
                                    correctG = true;
                                }
                            }

                            else
                            {
                                if (color.g < firstColor.g)
                                {
                                    correctG = true;
                                }

                            }

                        }

                        else if (differenceG > 0 && differenceG > 0.1f)
                        {
                            if (i > 0)
                            {
                                if (color.g > rgbCubes[j - 2])
                                {
                                    correctG = true;
                                }
                            }

                            else
                            {
                                if (color.g > firstColor.g)
                                {
                                    correctG = true;
                                }

                            }

                        }

                        else
                        {
                            correctG = true;
                        }
                    }


                    if (color.b > rgbCubes[j + 2] - 0.25 && color.b < rgbCubes[j + 2] + 0.25)
                    {
                        if (differenceB < 0 && differenceB < -0.1f)
                        {
                            if (i > 0)
                            {
                                if (color.b < rgbCubes[j - 1])
                                {
                                    correctB = true;
                                }
                            }

                            else
                            {
                                if (color.b < firstColor.b)
                                {
                                    correctB = true;
                                }

                            }

                        }

                        else if (differenceB > 0 && differenceB > 0.1f)
                        {
                            if (i > 0)
                            {
                                if (color.b > rgbCubes[j - 1])
                                {
                                    correctB = true;
                                }
                            }

                            else
                            {
                                if (color.b > firstColor.b)
                                {
                                    correctB = true;
                                }

                            }

                        }

                        else
                        {
                            correctB = true;
                        }
                    }


                    if (correctB == true && correctG == true && correctR == true)
                    {
                        correctCubes++;
                    }

                    j = j + 3;

                }

                if (correctCubes == playerCubes.Length)
                {
                    correctGame = true;
                }

                else{
                    correctCubes = 0;
                }


                */




                /*

                if (colorCube[i].b > r1 && colorCube[i].b < r2) {

                    correctB = true;

                    if (colorCube[i].b > posB){
                        scoreB = 100 - (colorCube[i].b-posB);
                    }

                    else{

                        scoreB = 100 - (posB-colorCube[i].b);
                    }

                }

                if (correctR == true && correctG == true && correctB == true){
                    levelCompleted = true;
                }

                  */

}


