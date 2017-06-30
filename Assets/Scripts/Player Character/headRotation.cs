using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headRotation : MonoBehaviour {

	void Update () {
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation
                (
                transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector3.forward
                ),
            0.5f
            );       

        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }
}
