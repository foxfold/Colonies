using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyRotate : MonoBehaviour {

    public GameObject head;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
	}
}
