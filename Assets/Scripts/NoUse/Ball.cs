using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{


    private float speed = 5.0f;



    public float h, s, v;



    // Use this for initialization
    void Start()
    {

        if (Player.currentLevel > 5)
        {
            speed = 7.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {


        Color.RGBToHSV(this.GetComponent<Renderer>().material.color, out h, out s, out v);

        this.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));


        if (this.transform.position.y <= -20.0f || Player.finishedGame == true)
        {
            Destroy(this.gameObject);
        }
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.GetType() == typeof(BoxCollider))
        {
        
            Destroy(this.gameObject);
            
            
        }

    }


}
