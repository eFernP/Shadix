using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour {


    public GameObject topInterface;
    public GameObject bottomInterface;
	// Use this for initialization
	void Start () {

        
        if (Screen.height >= 1900 && Screen.height < 2000)
        {
            bottomInterface.transform.position = new Vector3(bottomInterface.transform.position.x, bottomInterface.transform.position.y+150, bottomInterface.transform.position.z);
            topInterface.transform.position = new Vector3(topInterface.transform.position.x, topInterface.transform.position.y - 160, topInterface.transform.position.z);
        }


        if (Screen.height >= 1100 && Screen.height < 1900)
        {
            bottomInterface.transform.position = new Vector3(bottomInterface.transform.position.x, bottomInterface.transform.position.y + 165, bottomInterface.transform.position.z);
            topInterface.transform.position = new Vector3(topInterface.transform.position.x, topInterface.transform.position.y - 180, topInterface.transform.position.z);
        }



        if (Screen.height > 900 && Screen.height < 1100)
        {
            bottomInterface.transform.position = new Vector3(bottomInterface.transform.position.x, bottomInterface.transform.position.y + 100, bottomInterface.transform.position.z);
            topInterface.transform.position = new Vector3(topInterface.transform.position.x, topInterface.transform.position.y - 120, topInterface.transform.position.z);
        }

        if (Screen.height > 800 && Screen.height <= 900)
        {
            bottomInterface.transform.position = new Vector3(bottomInterface.transform.position.x, bottomInterface.transform.position.y + 70, bottomInterface.transform.position.z);
            topInterface.transform.position = new Vector3(topInterface.transform.position.x, topInterface.transform.position.y - 80, topInterface.transform.position.z);
        }
        if (Screen.height <= 800)
        {
            bottomInterface.transform.position = new Vector3(bottomInterface.transform.position.x, bottomInterface.transform.position.y + 85, bottomInterface.transform.position.z);
            topInterface.transform.position = new Vector3(topInterface.transform.position.x, topInterface.transform.position.y - 100, topInterface.transform.position.z);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
