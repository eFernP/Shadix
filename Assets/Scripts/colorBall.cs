using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class colorBall : MonoBehaviour
{

    private float speed;
    public GameObject white;
    public GameObject black;
    public GameObject cyan;
    public GameObject yellow;
    public GameObject magenta;
    public AudioClip dropAudio;

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
        


        //Show the sprite for each color
        if (this.GetComponent<Renderer>().material.color == Color.white)
        {
            white.gameObject.SetActive(true);
        }

        if (this.GetComponent<Renderer>().material.color == Color.black)
        {
            black.gameObject.SetActive(true);
        }

        if (this.GetComponent<Renderer>().material.color == Color.cyan)
        {
            cyan.gameObject.SetActive(true);
        }

        if (this.GetComponent<Renderer>().material.color == Color.magenta)
        {
            magenta.gameObject.SetActive(true);
        }

        if (this.GetComponent<Renderer>().material.color == Color.yellow)
        {
            yellow.gameObject.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {

        this.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));


        if (this.transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        
        
        if (other.GetType() == typeof(BoxCollider))
        {
            float h, s, v;

            //When a ball touch a cube changes the color of the cube
            if (other.gameObject.name != "Floor")
            {
                Color.RGBToHSV(other.GetComponent<Renderer>().material.color, out h, out s, out v);

                if (this.GetComponent<Renderer>().material.color == Color.black)
                {
                    
                    //Save the light number of the cube and increase it
                    int l = other.GetComponent<Colors>().numL;

                    if (l < Colors.lightColor.Length - 1)
                    {
                        if (l < 2)
                        {
                            s = Colors.lightColor[l + 1];
                            other.GetComponent<Colors>().numL = l + 1;
                        }
                        else
                        {
                            v = Colors.lightColor[l + 1];
                            other.GetComponent<Colors>().numL = l + 1;
                        }
                        
                        other.GetComponent<Renderer>().material.color = Color.HSVToRGB(h, s, v);
                    }


                }
                else if (this.GetComponent<Renderer>().material.color == Color.white)
                {
                    //Save the light number of the cube and decrease it
                    int l = other.GetComponent<Colors>().numL;

                    if (l > 0)
                    {
                        if (l < 3)
                        {
                            s = Colors.lightColor[l - 1];
                            other.GetComponent<Colors>().numL = l - 1;
                        }
                        else
                        {
                            if (l == 3)
                            {
                                v = 0.95f;
                                other.GetComponent<Colors>().numL = l - 1;
                            }
                            else
                            {
                                v = Colors.lightColor[l - 1];
                                other.GetComponent<Colors>().numL = l - 1;
                            }
                        }
                        
                        other.GetComponent<Renderer>().material.color = Color.HSVToRGB(h, s, v);
                    }

                }
                else if (this.GetComponent<Renderer>().material.color == Color.magenta)
                {
                    //Change the hue number approaching it to the magenta hue
                    int hue = other.GetComponent<Colors>().numH;

                    if (hue < 5 || hue > 13)
                    {
                        if (hue > 0)
                        {
                            h = Colors.hueColor[hue - 1];
                            other.GetComponent<Colors>().numH = hue - 1;
                        }
                        else
                        {
                            h = Colors.hueColor[Colors.hueColor.Length - 1];
                            other.GetComponent<Colors>().numH = Colors.hueColor.Length - 1;
                        }

                    }
                    else
                    {
                        if (hue != 13)
                        {
                            h = Colors.hueColor[hue + 1];
                            other.GetComponent<Colors>().numH = hue + 1;
                        }
                    }
                    other.GetComponent<Renderer>().material.color = Color.HSVToRGB(h, s, v);
                }
                else if (this.GetComponent<Renderer>().material.color == Color.yellow)
                {
                    //Change the hue number approaching it to the yellow hue
                    int hue = other.GetComponent<Colors>().numH;

                    if (hue < 2 || hue > 10)
                    {
                        if (hue == Colors.hueColor.Length - 1)
                        {
                            h = Colors.hueColor[0];
                            other.GetComponent<Colors>().numH = 0;
                        }
                        else
                        {
                            h = Colors.hueColor[hue + 1];
                            other.GetComponent<Colors>().numH = hue + 1;
                        }
                        
                    }
                    else if (hue > 2 && hue <=10)
                    {
                        h = Colors.hueColor[hue - 1];
                        other.GetComponent<Colors>().numH = hue - 1;
                    }

                    other.GetComponent<Renderer>().material.color = Color.HSVToRGB(h, s, v);
                }
                else if (this.GetComponent<Renderer>().material.color == Color.cyan)
                {
                    //Change the hue number approaching it to the cyan hue
                    int hue = other.GetComponent<Colors>().numH;

                    if (hue > 0 && hue < 8)
                    {
                        h = Colors.hueColor[hue + 1];
                        other.GetComponent<Colors>().numH = hue + 1;
                    }
                    else if (hue > 8)
                    {
                        h = Colors.hueColor[hue - 1];
                        other.GetComponent<Colors>().numH = hue - 1;
                    }
                    else if (hue == 0)
                    {
                        h = Colors.hueColor[Colors.hueColor.Length - 1];
                        other.GetComponent<Colors>().numH = Colors.hueColor.Length - 1;
                    }

                    other.GetComponent<Renderer>().material.color = Color.HSVToRGB(h, s, v);

                }

                tPlayer.checkColumn(other.GetComponent<tCube>().boardPosition);
                tPlayer.checkLine(other.GetComponent<tCube>().boardPosition);
            }


            this.GetComponent<AudioSource>().PlayOneShot(dropAudio);

            this.gameObject.name = "setColor";
            this.GetComponent<SphereCollider>().enabled = false;

            black.gameObject.SetActive(false);
            white.gameObject.SetActive(false);
            yellow.gameObject.SetActive(false);
            cyan.gameObject.SetActive(false);
            magenta.gameObject.SetActive(false);

            tPlayer.resetObject(-1);
            SpawnTetris.makeSpawn = true;

        }

    }

}
