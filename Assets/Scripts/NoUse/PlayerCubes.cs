using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCubes : MonoBehaviour {


    private Color playerColor;
    /*
    public float colorR;
    public float colorG;
    public float colorB;
    */
    public float speed = 7.5f;

    public Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    public float hsvIncrease = 0.1f;

    public float hF, sF, vF;

    public float maxLeft = -4.0f;
    public float maxRight = 4.0f;

    private GameObject hitObject;
    public GameObject hitSpace;



    // Use this for initialization
    void Start () {

        playerColor = this.GetComponent<Renderer>().material.color;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        colorR = playerColor.r;
        colorG = playerColor.g;
        colorB = playerColor.b;
        */
        
        Color.RGBToHSV(this.GetComponent<Renderer>().material.color, out hF, out sF, out vF);

        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && (hit.collider.name == hitSpace.name))
            {

                Debug.Log(this.gameObject.name + "touch");

                hitObject = hit.collider.gameObject;

                
                


            }

        }

        if (Input.GetMouseButton(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (Combination.isPaused == false && Player.finishedGame == false)
                {
                    if (hit.collider.name == hitSpace.name)
                    {
                        
                        if (hit.point.x > this.transform.position.x)
                        {
                            if (this.transform.position.x < maxRight)
                            {
                                this.transform.Translate(speed * Time.deltaTime, 0, 0);
                            }
                                
                            }
                         else if (hit.point.x < this.transform.position.x)
                         {
                             if (this.transform.position.x > maxLeft)
                             {
                                 this.transform.Translate(-speed * Time.deltaTime, 0, 0);
                             }
                                
                         }
                         else
                         {
                            this.transform.Translate(0, 0, 0);
                         }
                    }                    
                }
            }
        }



        if (Input.GetMouseButtonUp(0))
        {
            hitObject = null;
        }

        /*
        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && (hit.collider.name == this.gameObject.name))
            {

                Debug.Log(this.gameObject.name + "touch");

                hitObject = hit.collider.gameObject;

                //this.transform.position = new Vector3(firstPosition.x, firstPosition.y, firstPosition.z + 1.4f);


            }

        }


        if (hitObject)
        {
            if (Input.GetMouseButton(0))
            {
                ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (Combination.isPaused == false && Player.finishedGame == false)
                    {
                        if (this.transform.position.x >= maxLeft && this.transform.position.x <= maxRight)
                        {
                            this.transform.position = new Vector3(hit.point.x, hitObject.transform.position.y, hitObject.transform.position.z);
                        }

                        else
                        {

                            if (this.transform.position.x < maxLeft)
                            {
                                this.transform.position = new Vector3(maxLeft + 0.1f, hitObject.transform.position.y, hitObject.transform.position.z);
                            }

                            else if (this.transform.position.x > maxRight)
                            {
                                this.transform.position = new Vector3(maxRight - 0.1f, hitObject.transform.position.y, hitObject.transform.position.z);
                            }

                        }
                    }

                    
                }

            }

            if (Input.GetMouseButtonUp(0))
            {
                hitObject = null;
            }

            
        }
        */

    }


    void OnTriggerEnter(Collider other)
    {




        if (other.GetType() == typeof(SphereCollider))
        {
            if (other.gameObject.name == "ball")
            {
                //Debug.Log("collision");

                Renderer rend = other.GetComponent<Renderer>();


                float h, s, v;
                float hBall, sBall, vBall;
                Color.RGBToHSV(playerColor, out h, out s, out v);

                Color.RGBToHSV(rend.material.color, out hBall, out sBall, out vBall);


                hsvIncrease = other.transform.localScale.x / 10;


                if (rend.material.color == Color.white)
                {

                    
                    if (v + hsvIncrease <= 1.0f)
                    {
                        v = v + hsvIncrease;
                    }
                    else
                    {
                        v = 1.0f;

                        if (s - hsvIncrease >= 0f)
                        {

                            s = s - hsvIncrease;
                        }

                        else
                        {
                            s = 0.0f;



                        }
                    }


                }

                else if (rend.material.color == Color.black)
                {

                    if (v - hsvIncrease >= 0.0f)
                    {
                        v = v - hsvIncrease;
                    }

                    else
                    {
                        v = 0.0f;

                    }

                }


                else if (rend.material.color == Color.magenta)
                {
                   

                    if (s == 0)
                    {
                        h = hBall;
                        s = 0.5f;
                    }

                    else
                    {

                        if (v != 0)
                        {
                            if (h == hBall)
                            {

                                h = hBall;

                                s = s + hsvIncrease;

                                if (s > 1.0f)
                                {
                                    s = 1.0f;
                                }

                                v = v + (hsvIncrease / 2);

                                if (v > 1.0f)
                                {
                                    v = 1.0f;
                                }

                            }


                            else if (h > 0.45 && h < hBall)
                            {
                                h = h + hsvIncrease;

                                if (h > 1)
                                {
                                    h = 0f + (h - 1f);
                                }

                                if (h > hBall)
                                {
                                    h = hBall;

                                    
                                }

                                s = s + hsvIncrease;

                                if (s > 1.0f)
                                {
                                    s = 1.0f;
                                }

                                v = v + (hsvIncrease / 2);

                                if (v > 1.0f)
                                {
                                    v = 1.0f;
                                }
                            }

                            else if (h > 0.45 && h > hBall)
                            {
                                h = h - hsvIncrease;

                                if (h < 0)
                                {
                                    h = 1f - (0f - h);
                                }

                                if (h < hBall)
                                {
                                    h = hBall;

                                }

                                s = s + hsvIncrease;

                                if (s > 1.0f)
                                {
                                    s = 1.0f;
                                }

                                v = v + (hsvIncrease / 2);

                                if (v > 1.0f)
                                {
                                    v = 1.0f;
                                }
                            }

                            else if (h < 0.25)
                            {
                                h = h - hsvIncrease;

                                if (h < 0)
                                {
                                    h = 1f - (0f - h);

                                    if (h < hBall)
                                    {
                                        h = hBall;

                                    }
                                }


                                s = s + hsvIncrease;

                                if (s > 1.0f)
                                {
                                    s = 1.0f;
                                }

                                v = v + (hsvIncrease / 2);

                                if (v > 1.0f)
                                {
                                    v = 1.0f;
                                }

                            }

                            else
                            {

                                if (h < 0.333f)
                                {
                                    h = 0.15f;
                                }

                                else
                                {
                                    h = 0.75f;
                                }
                                
                                v = v - hsvIncrease - 0.3f;

                                if (v < 0.0f)
                                {
                                    v = 0.0f;
                                }

                                s = s - hsvIncrease - 0.15f;

                                if (s < 0.0f)
                                {
                                    s = 0.0f;
                                }

               
                            }

                        }


                    }
                }

                else if (rend.material.color == Color.yellow)
                {

                    

                    if (s == 0)
                    {
                        h = hBall;
                        s = 0.5f;
                    }

                    else
                    {

                        if (v != 0)
                        {
                            if (h == hBall)
                            {

                                h = hBall;

                                s = s + hsvIncrease;

                                if (s > 1.0f)
                                {
                                    s = 1.0f;
                                }

                                v = v + (hsvIncrease/2);

                                if (v > 1.0f)
                                {
                                    v = 1.0f;
                                }

                            }


                            else if (h > 0.75f)
                            {
                                h = h + hsvIncrease;
                                
                                if (h > 1)
                                {
                                    h = 0f + (h - 1f);

                                    if (h > hBall)
                                    {
                                        h = hBall;


                                    }
                                }

                                s = s + hsvIncrease;

                                if (s > 1.0f)
                                {
                                    s = 1.0f;
                                }

                                v = v + (hsvIncrease / 2);

                                if (v > 1.0f)
                                {
                                    v = 1.0f;
                                }



                            }

                            else if (h < 0.55f && h < hBall)
                            {
                                h = h + hsvIncrease;
                                

                                if (h > 1)
                                {
                                    h = 0f + (h - 1f);
                                }

                                if (h > hBall)
                                {
                                    h = hBall;


                                }

                                s = s + hsvIncrease;

                                if (s > 1.0f)
                                {
                                    s = 1.0f;
                                }

                                v = v + (hsvIncrease / 2);

                                if (v > 1.0f)
                                {
                                    v = 1.0f;
                                }
                            }

                            else if (h < 0.55f && h > hBall)
                            {
                                h = h - hsvIncrease;
                                
                                if (h < hBall)
                                {
                                    h = hBall;


                                }

                                s = s + hsvIncrease;

                                if (s > 1.0f)
                                {
                                    s = 1.0f;
                                }

                                v = v + (hsvIncrease / 2);

                                if (v > 1.0f)
                                {
                                    v = 1.0f;
                                }
                            }

                            else
                            {
                                if (h < 0.67f)
                                {
                                    h = 0.4f;
                                }

                                else
                                {
                                    h = 0.7f;
                                }


                                s = s - (hsvIncrease)-0.1f;

                                v = v - (hsvIncrease)-0.05f;

                                if ( v < 0.0f)
                                {
                                    v = 0.0f;
                                }

                                if (s < 0.0f)
                                {
                                    s = 0.0f;
                                }


                            }

                        }


                    }
                }

                else if (rend.material.color == Color.cyan)
                {

                    
                    if (s == 0)
                    {
                        h = hBall;
                        s = 0.5f;
                    }

                    else
                    {

                        if (v != 0)
                        {
                            if (h == hBall)
                            {

                                h = hBall;

                                s = s + hsvIncrease;

                                if (s > 1.0f)
                                {
                                    s = 1.0f;
                                }

                                v = v + (hsvIncrease / 2);

                                if (v > 1.0f)
                                {
                                    v = 1.0f;
                                }

                            }


                            else if (h < hBall && h > 0.13f)
                            {
                                h = h + hsvIncrease;

                                if (h > 1)
                                {
                                    h = 0f + (h - 1f);
                                }

                                if (h > hBall)
                                {
                                    h = hBall;


                                }

                                s = s + hsvIncrease;

                                if (s > 1.0f)
                                {
                                    s = 1.0f;
                                }

                                v = v + (hsvIncrease / 2);

                                if (v > 1.0f)
                                {
                                    v = 1.0f;
                                }
                            }

                            else if (h > hBall && h < 0.93f)
                            {
                                h = h - hsvIncrease;

                                if (h < 0)
                                {
                                    h = 1f - (0f - h);
                                }

                                if (h < hBall)
                                {
                                    h = hBall;


                                }

                                s = s + hsvIncrease;

                                if (s > 1.0f)
                                {
                                    s = 1.0f;
                                }

                                v = v + (hsvIncrease / 2);

                                if (v > 1.0f)
                                {
                                    v = 1.0f;
                                }
                            }

                            else
                            {
                                if (h <= 0.13 && h >= 0.03f)
                                {
                                    h = 0.15f;
                                    v = v - hsvIncrease - 0.3f;

                                    if (v < 0.0f)
                                    {
                                        v = 0.0f;
                                    }

                                    s = s - hsvIncrease - 0.2f;

                                    if (s < 0.0f)
                                    {
                                        s = 0.0f;
                                    }
                                }

                                else
                                {
                                        h = 0.8f;
                                    


                                    v = v - hsvIncrease - 0.3f;

                                    if (v < 0.0f)
                                    {
                                        v = 0.0f;
                                    }

                                    s = s - hsvIncrease - 0.2f;

                                    if (s < 0.0f)
                                    {
                                        s = 0.0f;
                                    }
                                }

                            }

                        }


                    }
                }




                playerColor = Color.HSVToRGB(h, s, v);
                this.GetComponent<Renderer>().material.color = playerColor;


            }

        }

    }




}
