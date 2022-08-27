using UnityEngine;
using UnityEngine.SceneManagement;

public class tCube : MonoBehaviour
{


    private float speed;
    
    public int boardPosition;

    public AudioClip cubeAudio;
    

    // Use this for initialization
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "LevelTetris")
        {
            speed = 1.5f;
        }
        else
        {
            speed = 2.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Move down the cube
        this.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));

    }


    void OnTriggerEnter(Collider other)
    {

        if (other.GetType() == typeof(SphereCollider) && other.gameObject.name == "Color")
        {
            
        }


        if (other.GetType() == typeof(BoxCollider) && this.gameObject.name == "Cube")
        {
            stop();
            Debug.Log("COLLISION WITH " + other.gameObject.name);
            int j = 0;
            int boardPos = 0;

            //Puts on the cube that has collided with and saves its new position
            for (int i = tPlayer.objPos - 1; i < 45; i += 5)
            {
                if (tPlayer.board[i] == false)
                {
                    boardPos = i;
                    boardPosition = i;
                    tPlayer.board[i] = true;
                    this.transform.position = new Vector3(this.transform.position.x, -4.0f + (1.2f * j), this.transform.position.z);
                    Debug.Log("The cube will be in position number " + boardPos);
                    break;
                }
                j++;
            }
            this.GetComponent<AudioSource>().PlayOneShot(cubeAudio);
            //Reset the controlled object and check the line and column of the set cube
            tPlayer.resetObject(boardPos);
            tPlayer.checkColumn(boardPos);
            tPlayer.checkLine(boardPos);
            SpawnTetris.makeSpawn = true;

        }

        //Avoid to move through a cube that is in the right
        if (other.GetType() == typeof(CapsuleCollider) && this.gameObject.name =="Cube")
        {

            tPlayer.canRight = false;
            
        }

        //Avoid to move through a cube that is in the left
        if (other.GetType() == typeof(SphereCollider) && this.gameObject.name == "Cube")
        {
           
            tPlayer.canLeft = false;
            

        }


        

    }


    void OnTriggerExit(Collider other)
    {
        //Allow to move right when the cube has passed the cubes that were on its right
        if (other.GetType() == typeof(CapsuleCollider) && this.gameObject.name == "Cube")
        {
            tPlayer.canRight = true;

        }

        //Allow to move left when the cube has passed the cubes that were on its left
        if (other.GetType() == typeof(SphereCollider) && this.gameObject.name == "Cube")
        {
            tPlayer.canLeft = true;

        }
    }


    //Stop the cube
    void stop()
    {
        speed = 0f;

    }


}
