using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class tPlayer : MonoBehaviour {


    private static GameObject currentObj;
    public static int objPos;
    public static bool canRight;
    public static bool canLeft;

    public static bool[] board;
    public static int nCubes;


    public static bool correctColumn = false;
    public static bool correctLine = false;
    public static int numberColumn = -1;
    public static int numberColumnCubes = 0;
    public Text scoreText;
    
    public static int score;

    public AudioClip correctCubes;

    public static bool lineDestroyed;
    public static int nLineDestroyed;

    private float timer = 0.0f;
    private float animationWaitingTime = 2.0f;

    // Use this for initialization
    void Start () {


        //Creates a list withe all the possible cubes positions and indicates that there are no cubes in any of them 
        nCubes = 0;
        board = new bool[45];
        
        for (int i = 0; i < 45; i++)
        {
            board[i] = false;
        }


        //Saves the first cube or ball to control it
        currentObj = GameObject.Find("Cube");

        if (currentObj == null)
        {
            currentObj = GameObject.Find("Color");
        }

        objPos = 3;
        canLeft = true;
        canRight = true;
        score = 0;
        lineDestroyed = false;

    }

    // Update is called once per frame
    void Update () {

        //Check if the last cube or ball has disappeared and saves the new one
        if (currentObj == null)
        {
            currentObj = GameObject.Find("Cube");
        }

        if (currentObj == null)
        {
            currentObj = GameObject.Find("Color");
        }

        //Scores points when a line or column has been destroy
        if (correctColumn == true || correctLine == true)
        {
            /*StartCoroutine(WaitOneSec());
            animateVanish(numberColumn, numberColumnCubes);
            StartCoroutine(WaitOneSec());
            destroyColumn(numberColumn, numberColumnCubes);
            correctColumn = false;*/
            scoreText.text = "SCORE: " + score;
            correctColumn = false;
            correctLine = false;

            this.gameObject.GetComponent<AudioSource>().PlayOneShot(correctCubes);
            

        }

        if (lineDestroyed == true)
        {
            timer += Time.deltaTime;
            if (timer >= animationWaitingTime)
            {
                timer = 0f;

                StartCoroutine(WaitForAnimation());
                downOneLine(nLineDestroyed);
                lineDestroyed = false;


            }
            
        }
    }


    //Moves the current ball or cube
    public void goLeft()
    {

        if (objPos < 5 && canLeft == true)
        {
            objPos++;
            checkPosition();
        }
       
    }

    public void goRight()
    {
        if (objPos > 1 && canRight == true)
        {
            objPos--;
            checkPosition();
        }
    }


    public void goDown()
    {
        int j = 0;
        for (int i = objPos - 1; i < 45; i += 5)
        {
            if (board[i] == false)
            {
                currentObj.transform.position = new Vector3(currentObj.transform.position.x, (-4.0f + (1.2f * j))+0.1f, currentObj.transform.position.z);
                break;
            }
            j++;
        }
    }

    //To know where has to move the ball or cube
    void checkPosition()
    {
        if (objPos == 1)
        {
            currentObj.transform.position = new Vector3(-2.4f, currentObj.transform.position.y, currentObj.transform.position.z);
        }
        else if (objPos == 2)
        {
            currentObj.transform.position = new Vector3(-1.2f, currentObj.transform.position.y, currentObj.transform.position.z);
        }
        else if (objPos == 3)
        {
            currentObj.transform.position = new Vector3(0f, currentObj.transform.position.y, currentObj.transform.position.z);
        }
        else if (objPos == 4)
        {
            currentObj.transform.position = new Vector3(1.2f, currentObj.transform.position.y, currentObj.transform.position.z);
        }
        else if (objPos == 5)
        {
            currentObj.transform.position = new Vector3(2.4f, currentObj.transform.position.y, currentObj.transform.position.z);
        }
    }


    //Assign a name for the cube according its new position, activates the right and left collider so the new balls and cubes can't move through it, and reset the controls and position for the new object will be controled
    public static void resetObject(int pos)
    {   


        if (currentObj.name == "Cube")
        {
            currentObj.name = "cube"+pos;
            CapsuleCollider cCollider = currentObj.GetComponent<CapsuleCollider>();
            cCollider.enabled = true;
            SphereCollider sCollider = currentObj.GetComponent<SphereCollider>();
            sCollider.enabled = true;
            
        }
       
        tPlayer.canRight = true;
        tPlayer.canLeft = true;

        currentObj = null;
        /*
        currentObj = GameObject.Find("Cube");
        if (currentObj == null)
        {
            currentObj = GameObject.Find("Color");
        }*/

        objPos = 3;

    }


    //Check if a line has a correct gradient
    public static void checkLine(int pos)
    {
        int line = pos/5;
        int count = 0;
        int correctCubes = 0;

        //Check if the line is full
        for (int i = line * 5; i < (line*5)+5; i++)
        {
            if (board[i] == true)
            {
                count++;
            }
            else
            {
                break;
            }
        }

        if (count == 5)
        {
            int hIncrease = 0;
            int lIncrease = 0;

            //Check the gradient comparing each cube with the next one
            for (int i = line * 5; i < (line * 5) + 4; i++)
            {
                GameObject currentCube = GameObject.Find("cube"+i);
                GameObject nextCube = GameObject.Find("cube" +(i+1));

                Debug.Log("Comparing " + currentCube.name+ " with " +nextCube.name);

                int hue = currentCube.GetComponent<Colors>().numH;
                int light = currentCube.GetComponent<Colors>().numL;
                int nextHue = nextCube.GetComponent<Colors>().numH;
                int nextLight = nextCube.GetComponent<Colors>().numL;
                int correctHueLight = 0;

                if (i == line*5)
                {
                    Debug.Log("Checking first and second");
                    if (light + 1 == nextLight) 
                    {
                        lIncrease = 2;
                        correctHueLight++;
                        Debug.Log("1");
                    }
                    else if (light - 1 == nextLight)
                    {
                        lIncrease = 1;
                        correctHueLight++;
                    }
                    else if (light == nextLight)
                    {
                        lIncrease = 0;
                        correctHueLight++;
                    }

                    if ((hue + 1 == nextHue) || (hue == Colors.NUMBER_COLORS - 1 && nextHue == 0))
                    {
                        hIncrease = 2;
                        correctHueLight++;
                    }
                    else if ((hue - 1 == nextHue) || (hue == 0 && nextHue == Colors.NUMBER_COLORS - 1))
                    {
                        hIncrease = 1;
                        correctHueLight++;
                    }
                    else if (hue == nextHue)
                    {
                        hIncrease = 0;
                        correctHueLight++;
                        Debug.Log("2");
                    }

                    if (correctHueLight == 2)
                    {
                        Debug.Log("Hue and light correct");
                    }

                    if (correctHueLight == 2 && (hue != nextHue || light != nextLight))
                    {
                        correctCubes++;
                        Debug.Log("Hue and light correct and they are not the same cube");
                    }
                    else
                    {
                        break;
                    }
                 
                }

                else
                {
                    if (lIncrease == 0 && hue != nextHue)
                    {
                        if (light + 1 == nextLight)
                        {
                            lIncrease = 2;
                            correctHueLight++;
                        }
                        else if (light - 1 == nextLight)
                        {
                            lIncrease = 1;
                            correctHueLight++;
                        }
                        else if (light == nextLight)
                        {
                            lIncrease = 0;
                            correctHueLight++;
                        }
                    }
                    else if (lIncrease == 1)
                    {
                        if (light - 1 == nextLight)
                        {
                            correctHueLight++;
                        }
                        else if (light == nextLight && hue != nextHue)
                        {
                            correctHueLight++;
                        }
                    }
                    else if (lIncrease == 2)
                    {
                        if (light + 1 == nextLight)
                        {
                            correctHueLight++;
                        }
                        else if (light == nextLight && hue != nextHue)
                        {
                            correctHueLight++;
                        }
                    }

                    if (hIncrease == 0)
                    {
                        if ((hue + 1 == nextHue) || (hue == Colors.NUMBER_COLORS - 1 && nextHue == 0))
                        {
                            hIncrease = 2;
                            correctHueLight++;
                        }
                        else if ((hue - 1 == nextHue) || (hue == 0 && nextHue == Colors.NUMBER_COLORS - 1))
                        {
                            hIncrease = 1;
                            correctHueLight++;
                        }
                        else if (hue == nextHue)
                        {
                            hIncrease = 0;
                            correctHueLight++;
                        }
                    }
                    else if (hIncrease == 1)
                    {
                        if ((hue - 1 == nextHue) || (hue == 0 && nextHue == Colors.NUMBER_COLORS - 1))
                        {
                            
                            correctHueLight++;
                        }
                        else if (hue == nextHue)
                        {
                            correctHueLight++;
                        }
                    }
                    else if (hIncrease == 2)
                    {
                        if ((hue + 1 == nextHue) || (hue == Colors.NUMBER_COLORS - 1 && nextHue == 0))
                        {
                            correctHueLight++;
                        }
                        else if (hue == nextHue)
                        {
                            correctHueLight++;
                        }
                    }

                    if (correctHueLight == 2 && (hue != nextHue || light != nextLight))
                    {
                        correctCubes++;
                        Debug.Log("Hue and light correct and they are not the same cube");
                    }
                    else
                    {
                        break;
                    }
                }

            }

        }

        if (correctCubes == 4)
        {
            Debug.Log("CORRECT LINE");
            correctLine = true;

            //Destroy the column if it's correct, free the position and increase the score

            for (int i = line * 5; i < (line * 5) + 5; i++)
            {
                GameObject currentCube = GameObject.Find("cube" + i);

                Animator animator = currentCube.GetComponent<Animator>();
                animator.enabled = true;
                if (animator != null)
                {
                    animator.SetBool("correctLine", true);
                }
            }



            for (int i = line * 5; i < (line * 5) + 5; i++)
            {
                GameObject currentCube = GameObject.Find("cube" + i);
                board[i] = false;
                currentCube.GetComponent<BoxCollider>().enabled = false;
                currentCube.GetComponent<SphereCollider>().enabled = false;
                currentCube.GetComponent<CapsuleCollider>().enabled = false;

                Destroy(currentCube.gameObject, 2f);
            }

           
           downOneLine(line);
                    
           tPlayer.score += 100;

            
            
        }

    }


    //Check if a column has a correct gradient
    public static void checkColumn(int pos)
    {
        int column = pos % 5;
        int count = 0;
        int correctCubes = 0;
        int line = 0;
        int lastLine = 0;

        //Count the number of cubes of a column
        for (int i = column; i < 45; i = column + (line * 5))
        {
            if (board[i] == true)
            {
                count = i;
                lastLine++;
            }
            else
            {
                break;
            }
            line++;
        }

        
        if (count > column)
        {

            int hIncrease = 0;
            int lIncrease = 0;
            line = 0;

            //Check the gradient comparing each cube with the next one
            for (int i = column; i < count; i = column + (line * 5))
            {
                GameObject currentCube = GameObject.Find("cube" + i);
                GameObject nextCube = GameObject.Find("cube" + (i+5));

                Debug.Log("Comparing " + currentCube.name + " with " + nextCube.name);

                int hue = currentCube.GetComponent<Colors>().numH;
                int light = currentCube.GetComponent<Colors>().numL;
                int nextHue = nextCube.GetComponent<Colors>().numH;
                int nextLight = nextCube.GetComponent<Colors>().numL;
                int correctHueLight = 0;

                if (i == column)
                {
                    ///IGUAL EN LES DOS FUNCIONS
                    Debug.Log("Checking first and second");
                    if (light + 1 == nextLight)
                    {
                        lIncrease = 2;
                        correctHueLight++;
                        Debug.Log("1");
                    }
                    else if (light - 1 == nextLight)
                    {
                        lIncrease = 1;
                        correctHueLight++;
                    }
                    else if (light == nextLight)
                    {
                        lIncrease = 0;
                        correctHueLight++;
                    }

                    if ((hue + 1 == nextHue) || (hue == Colors.NUMBER_COLORS - 1 && nextHue == 0))
                    {
                        hIncrease = 2;
                        correctHueLight++;
                    }
                    else if ((hue - 1 == nextHue) || (hue == 0 && nextHue == Colors.NUMBER_COLORS - 1))
                    {
                        hIncrease = 1;
                        correctHueLight++;
                    }
                    else if (hue == nextHue)
                    {
                        hIncrease = 0;
                        correctHueLight++;
                        Debug.Log("2");
                    }

                    if (correctHueLight == 2)
                    {
                        Debug.Log("Hue and light correct");
                    }

                    if (correctHueLight == 2 && (hue != nextHue || light != nextLight))
                    {
                        correctCubes++;
                        Debug.Log("Hue and light correct and they are not the same cube");
                    }
                    else
                    {
                        break;
                    }

                }

                else
                {
                    if (lIncrease == 0 && hue != nextHue)
                    {
                        if (light + 1 == nextLight)
                        {
                            lIncrease = 2;
                            correctHueLight++;
                        }
                        else if (light - 1 == nextLight)
                        {
                            lIncrease = 1;
                            correctHueLight++;
                        }
                        else if (light == nextLight)
                        {
                            lIncrease = 0;
                            correctHueLight++;
                        }
                    }
                    else if (lIncrease == 1)
                    {
                        if (light - 1 == nextLight)
                        {
                            correctHueLight++;
                        }
                        else if (light == nextLight && hue != nextHue)
                        {
                            correctHueLight++;
                        }
                    }
                    else if (lIncrease == 2)
                    {
                        if (light + 1 == nextLight)
                        {
                            correctHueLight++;
                        }
                        else if (light == nextLight && hue != nextHue)
                        {
                            correctHueLight++;
                        }
                    }

                    if (hIncrease == 0)
                    {
                        if ((hue + 1 == nextHue) || (hue == Colors.NUMBER_COLORS - 1 && nextHue == 0))
                        {
                            hIncrease = 2;
                            correctHueLight++;
                        }
                        else if ((hue - 1 == nextHue) || (hue == 0 && nextHue == Colors.NUMBER_COLORS - 1))
                        {
                            hIncrease = 1;
                            correctHueLight++;
                        }
                        else if (hue == nextHue)
                        {
                            hIncrease = 0;
                            correctHueLight++;
                        }
                    }
                    else if (hIncrease == 1)
                    {
                        if ((hue - 1 == nextHue) || (hue == 0 && nextHue == Colors.NUMBER_COLORS - 1))
                        {

                            correctHueLight++;
                        }
                        else if (hue == nextHue)
                        {
                            correctHueLight++;
                        }
                    }
                    else if (hIncrease == 2)
                    {
                        if ((hue + 1 == nextHue) || (hue == Colors.NUMBER_COLORS - 1 && nextHue == 0))
                        {
                            correctHueLight++;
                        }
                        else if (hue == nextHue)
                        {
                            correctHueLight++;
                        }
                    }

                    if (correctHueLight == 2 && (hue != nextHue || light != nextLight))
                    {
                        correctCubes++;
                        Debug.Log("Hue and light correct and they are not the same cube");
                    }
                    else
                    {
                        break;
                    }
                
                /////////
                }
                line++;
            }

            if (correctCubes == lastLine-1)
            {
                //Destroy the column if it's correct, free the position and increase the score
                Debug.Log("CORRECT COLUMN");
                correctColumn = true;
                
                line = 0;
                for (int i = column; i <= count; i = column + (line * 5))
                {
                    GameObject currentCube = GameObject.Find("cube" + i);

                    Animator animator = currentCube.GetComponent<Animator>();
                    animator.enabled = true;
                    if (animator != null)
                    {
                        animator.SetBool("correctLine", true);
                    }
                    line++;
                }

                line = 0;

                

                for (int i = column; i <= count; i = column + (line * 5))
                {
                    GameObject currentCube = GameObject.Find("cube" + i);
                    board[i] = false;
                    Debug.Log("Destroying " + currentCube.name+"/cube"+i);
                    currentCube.GetComponent<BoxCollider>().enabled = false;
                    currentCube.GetComponent<SphereCollider>().enabled = false;
                    currentCube.GetComponent<CapsuleCollider>().enabled = false;

                    Destroy(currentCube.gameObject, 2f);
                    
                    
                    line++;

                }

                tPlayer.score += (20 * lastLine);

            }
        }

    }

    IEnumerator WaitForAnimation()
    {
        print(Time.time);
        yield return new WaitForSeconds(5.0f);
        print(Time.time);

    }

   
    void animateVanish(int column, int count)
    {
        int line = 0;
        for (int i = column; i <= count; i = column + (line * 5))
        {
            GameObject currentCube = GameObject.Find("cube" + i);

            Animator animator = currentCube.GetComponent<Animator>();
            animator.enabled = true;
            if (animator != null)
            {
                animator.SetBool("correctLine", true);
            }
            line++;
        }
    }

    void destroyColumn(int column, int count)
    {
        int line = 0;
        for (int i = column; i <= count; i = column + (line * 5))
        {
            GameObject currentCube = GameObject.Find("cube" + i);
            board[i] = false;
            Debug.Log("Destroying " + currentCube.name + "/cube" + i);
            Destroy(currentCube.gameObject);
            line++;

        }
    }


   static void downOneLine(int line)
    {
        //Move down all the cubes put on the destroyed line
        for (int i = (line + 1) * 5; i < 45; i++)
        {
            if (board[i] == true)
            {
                GameObject currentCube = GameObject.Find("cube" + i);
                currentCube.transform.position = new Vector3(currentCube.transform.position.x, currentCube.transform.position.y - 1.2f, currentCube.transform.position.z);
                board[i] = false;
                board[i - 5] = true;
                currentCube.gameObject.name = "cube" + (i - 5);
            }
        }
    }
    




    /*
    public static void copyCheckColumn(int pos)
    {
        int column = pos % 5;
        int count = 0;
        int correctCubes = 0;
        int line = 0;
        int lastLine = pos / 5;

        if (lastLine > 0)
        {
            for (int i = column; i <= pos; i = column + (line * 5))
            {
                if (board[i] == true)
                {
                    count++;
                }
                else
                {
                    break;
                }
                line++;
            }

            if (count == lastLine + 1)
            {

                int hIncrease = 0;
                int lIncrease = 0;
                line = 0;
                Debug.Log("FULL LINE");
                for (int i = column; i < pos; i = column + (line * 5))
                {
                    GameObject currentCube = GameObject.Find("cube" + i);
                    GameObject nextCube = GameObject.Find("cube" + (column + (line + 1 * 5)));

                    Debug.Log("Comparing " + currentCube.name + " with " + nextCube.name);

                    int hue = currentCube.GetComponent<Colors>().numH;
                    int light = currentCube.GetComponent<Colors>().numL;
                    int nextHue = nextCube.GetComponent<Colors>().numH;
                    int nextLight = nextCube.GetComponent<Colors>().numL;
                    int correctHueLight = 0;

                    if (i == column)
                    {
                        ///IGUAL EN LES DOS FUNCIONS
                        Debug.Log("Checking first and second");
                        if (light + 1 == nextLight)
                        {
                            lIncrease = 2;
                            correctHueLight++;
                            Debug.Log("1");
                        }
                        else if (light - 1 == nextLight)
                        {
                            lIncrease = 1;
                            correctHueLight++;
                        }
                        else if (light == nextLight)
                        {
                            lIncrease = 0;
                            correctHueLight++;
                        }

                        if (hue + 1 == nextHue)
                        {
                            hIncrease = 2;
                            correctHueLight++;
                        }
                        else if (hue - 1 == nextHue)
                        {
                            hIncrease = 1;
                            correctHueLight++;
                        }
                        else if (hue == nextHue)
                        {
                            hIncrease = 0;
                            correctHueLight++;
                            Debug.Log("2");
                        }

                        if (correctHueLight == 2)
                        {
                            Debug.Log("Hue and light correct");
                        }

                        if (correctHueLight == 2 && (hue != nextHue || light != nextLight))
                        {
                            correctCubes++;
                            Debug.Log("Hue and light correct and they are not the same cube");
                        }
                        else
                        {
                            break;
                        }

                    }

                    else
                    {
                        if (lIncrease == 0 && hue != nextHue)
                        {
                            if (light + 1 == nextLight)
                            {
                                lIncrease = 2;
                                correctHueLight++;
                            }
                            else if (light - 1 == nextLight)
                            {
                                lIncrease = 1;
                                correctHueLight++;
                            }
                            else if (light == nextLight)
                            {
                                lIncrease = 0;
                                correctHueLight++;
                            }
                        }
                        else if (lIncrease == 1)
                        {
                            if (light - 1 == nextLight)
                            {
                                correctHueLight++;
                            }
                            else if (light == nextLight && hue != nextHue)
                            {
                                correctHueLight++;
                            }
                        }
                        else if (lIncrease == 2)
                        {
                            if (light + 1 == nextLight)
                            {
                                correctHueLight++;
                            }
                            else if (light == nextLight && hue != nextHue)
                            {
                                correctHueLight++;
                            }
                        }

                        if (hIncrease == 0)
                        {
                            if (hue + 1 == nextHue)
                            {
                                hIncrease = 2;
                                correctHueLight++;
                            }
                            else if (hue - 1 == nextHue)
                            {
                                hIncrease = 1;
                                correctHueLight++;
                            }
                            else if (hue == nextHue)
                            {
                                hIncrease = 0;
                                correctHueLight++;
                            }
                        }
                        else if (hIncrease == 1)
                        {
                            if (hue - 1 == nextHue)
                            {

                                correctHueLight++;
                            }
                            else if (hue == nextHue)
                            {
                                correctHueLight++;
                            }
                        }
                        else if (hIncrease == 2)
                        {
                            if (hue + 1 == nextHue)
                            {
                                correctHueLight++;
                            }
                            else if (hue == nextHue)
                            {
                                correctHueLight++;
                            }
                        }

                        if (correctHueLight == 2 && (hue != nextHue || light != nextLight))
                        {
                            correctCubes++;
                            Debug.Log("Hue and light correct and they are not the same cube");
                        }
                        else
                        {
                            break;
                        }
                        /////////
                    }
                    line++;

                }

                if (correctCubes == lastLine)
                {
                    Debug.Log("CORRECT LINE");
                    line = 0;
                    for (int i = column; i <= pos; i = column + (line * 5))
                    {
                        GameObject currentCube = GameObject.Find("cube" + i);

                        Animator animator = currentCube.GetComponent<Animator>();
                        animator.enabled = true;
                        if (animator != null)
                        {
                            animator.SetBool("correctLine", true);
                        }
                        line++;
                    }

                    line = 0;
                    for (int i = column; i <= pos; i = column + (line * 5))
                    {
                        GameObject currentCube = GameObject.Find("cube" + i);
                        board[i] = false;
                        Destroy(currentCube.gameObject);
                        line++;
                    }

                }
            }
        }
    }*/

}
