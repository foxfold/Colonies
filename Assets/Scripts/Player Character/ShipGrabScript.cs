using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGrabScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = gameObject.transform.parent.transform;
            collision.gameObject.GetComponent<PlayerMovement>().insideship = true;
            Camera.main.gameObject.GetComponent<CameraScript>().TrackedShip = gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
            collision.gameObject.GetComponent<PlayerMovement>().insideship = false;
            Camera.main.gameObject.GetComponent<CameraScript>().TrackedShip = null;
        }
    }
}
