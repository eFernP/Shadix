using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnTetris : MonoBehaviour
{

    //private float timer;
    //public float colorsWaitingTime = 4.0f;
    public GameObject colorBall;
    public GameObject cube;
    public int randomColors;
    public static bool makeSpawn;
    private int totalSpawns;
    public static bool gameOver = false;
    public static bool pauseGame = false;

    public GameObject gameButtons;
    public GameObject gameOverMenu;
    public GameObject pauseScreen;

    private int currentRecord;
    public Text recordText;
    public Text finalScoreText;

    public int ballPercentage;

    // Use this for initialization
    void Start()
    {
        //timer = 0.0f;
        makeSpawn = true;
        totalSpawns = 0;

        if (SceneManager.GetActiveScene().name == "LevelTetris")
        {
            currentRecord = PlayerPrefs.GetInt("record");
        }
        else
        {
            currentRecord = PlayerPrefs.GetInt("recordExpert");
        }
        

        gameOver = false;
        pauseGame = false;


    }

    // Update is called once per frame
    void Update()
    {

        //Allow spawn or not depending if the game is over or is paused
        if (makeSpawn == true && (gameOver == false || pauseGame == false))
        {
            
            spawn();
            makeSpawn = false;
            
        }

       
    }


    void spawn()
    {
        totalSpawns++;

        //Spawn only cubes if there have been less than three spawns
        if (totalSpawns < 3)
        {
            GameObject obj = Instantiate(cube) as GameObject;
            obj.transform.position = new Vector3(0, 5.6f, -1);
            obj.name = "Cube";
        }

        else{

            //Finish game if the last cube has occupied the position third position of the last possible line
            if (tPlayer.board[42] == true)
            {
                finishGame();
            }

            else
            {
                int cubeBall = Random.Range(0, 10);
                
                //Spawn ball
                if (cubeBall < ballPercentage)
                {
                    GameObject obj = Instantiate(colorBall) as GameObject;
                    Renderer rend = obj.GetComponent<Renderer>();


                    int numColor = Random.Range(0, 5);

                    if (numColor == 0)
                    {
                        rend.material.color = Color.white;
                    }

                    else if (numColor == 1)
                    {
                        rend.material.color = Color.black;
                    }

                    else if (numColor == 2)
                    {

                        rend.material.color = Color.magenta;
                    }

                    else if (numColor == 3)
                    {

                        rend.material.color = Color.cyan;
                    }

                    else if (numColor == 4)
                    {
                        rend.material.color = Color.yellow;

                    }

                    obj.transform.position = new Vector3(0, 5.5f, -1);
                    obj.name = "Color";
                }

                //Spawn cube
                else
                {
                    GameObject obj = Instantiate(cube) as GameObject;
                    obj.transform.position = new Vector3(0, 5.6f, -1);
                    obj.name = "Cube";

                }
            }
            
        }
        

    }


    //Voids for the buttons of the game interface
    public void retryLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void pause()
    {
        pauseGame = true;
        Time.timeScale = 0;
        pauseScreen.gameObject.SetActive(true);
    }


    public void continueGame()
    {
        Time.timeScale = 1;
        pauseGame = false;
        pauseScreen.gameObject.SetActive(false);

    }

    public void finishGame()
    {
        gameOver = true;
        Time.timeScale = 0;
        gameOverMenu.gameObject.SetActive(true);
        gameButtons.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(false);

        finalScoreText.text = "Your final score: " + tPlayer.score;
        if (tPlayer.score > currentRecord)
        {
            recordText.text = "New record!";
            

            if (SceneManager.GetActiveScene().name == "LevelTetris")
            {
                PlayerPrefs.SetInt("record", tPlayer.score);
            }
            else
            {
                PlayerPrefs.SetInt("recordExpert", tPlayer.score);
            }
        }
        else
        {
            recordText.text = "Current record: " + currentRecord;
        }

    }



   
}
