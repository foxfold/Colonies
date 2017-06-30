using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torsoRotation : MonoBehaviour {

    public GameObject head;

    int AngleClamp = 45;
    float AngleLerp;

    void Start()
    {
        AngleLerp = 1.0f - (AngleClamp / 360.0f);
    }

    void Update ()
    {
        Quaternion headRot = head.transform.rotation;
        Quaternion torsoRot = transform.rotation;

        // Rotates the torso only if the angle between the head and torso is great enough
        // Else just operates in the other direction
        if (Quaternion.Angle(headRot, torsoRot) > AngleClamp)
        {
            transform.rotation = Quaternion.Lerp(headRot, torsoRot, AngleLerp);
        }
        else if (Quaternion.Angle(torsoRot, headRot) > AngleClamp)
        {
            transform.rotation = Quaternion.Lerp(torsoRot, headRot, AngleLerp);
        }

        // Rotates the torso towards the forward movement while the player is intentionally moving
        if (Input.GetAxis("Vertical") + Input.GetAxis("Horizontal") > 0.0f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, headRot, 0.33f);
        }

       
    }
}
