using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject TrackedShip;
    public GameObject Player;
    public bool RotateCameraToTrackedObject;

    void FixedUpdate()
    {
        if (TrackedShip != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(TrackedShip.transform.position.x, TrackedShip.transform.position.y, -100), 0.5f);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 8, 0.1f);
            transform.parent = TrackedShip.transform;
            if (RotateCameraToTrackedObject)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, TrackedShip.transform.rotation, 0.5f);
            }            
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y, -100), 0.5f);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 32, 0.1f);
            transform.parent = Player.transform;
            if (RotateCameraToTrackedObject)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Player.transform.rotation, 0.5f);
            }                
        }        
    }
}