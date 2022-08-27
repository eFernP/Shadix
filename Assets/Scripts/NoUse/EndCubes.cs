using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCubes : MonoBehaviour {


    public float speed = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));

    }
}
