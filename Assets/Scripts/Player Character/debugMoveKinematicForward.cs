using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugMoveKinematicForward : MonoBehaviour {

	void Update () {
        transform.position += new Vector3(0.05f, 0.0f);
	}
}
