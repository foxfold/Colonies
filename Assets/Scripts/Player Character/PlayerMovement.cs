using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject head;
    float speed;
    public bool insideship = false;

    void FixedUpdate()
    {
        
        if (insideship) { speed = 250f; } else { speed = 1f; }
        float distanceFromMouse = Vector3.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) - 10f; //Camera is at -10 for occlusion reasons
        if (distanceFromMouse < 0.125f)
        {
            speed *= Mathf.Lerp(0.4f, 1f, distanceFromMouse * 8f);
        }            
        if (distanceFromMouse > 0.0005f)
        {
            float inputUp = Input.GetAxis("Vertical");
            GetComponent<Rigidbody2D>().AddForce(head.transform.up * speed * inputUp);
            float inputRight = Input.GetAxis("Horizontal");
            GetComponent<Rigidbody2D>().AddForce(head.transform.right * speed * inputRight);
        }
        
    }

    void OnTriggerStay2D(Collider2D ship)
    {
        if (ship.gameObject.tag == "shipInside")
        {
            GetComponent<Rigidbody2D>().velocity = ship.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity;
        }
    }
}