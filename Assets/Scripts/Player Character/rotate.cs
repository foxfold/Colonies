using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

    float speedx = 0;
    float speedy = 0;
    float speedz = 0;

    private void Start()
    {
        speedx = Random.Range(-0.05f, 0.05f);
        speedy = Random.Range(-0.05f, 0.05f);
        speedz = Random.Range(-0.05f, 0.05f);
    }

	void Update () {
        transform.Rotate(speedx, speedy, speedz);
	}
}
