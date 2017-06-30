using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DoorKeyColors : MonoBehaviour {

    public enum LockType { NoLock, Red, Green, Blue, Purple, Yellow };
    public LockType locktype;
    SpriteRenderer ren;
    
    void Start ()
    {
        ren = GetComponent<SpriteRenderer>();
    }

    void Update ()
    {
        if (Application.isPlaying) return;
                
        switch (locktype)
        {
            case LockType.NoLock:
                ren.color = new Color(1.0f, 1.0f, 1.0f);
                break;

            case LockType.Red:
                ren.color = new Color(0.9f, 0.1f, 0.3f);
                break;

            case LockType.Green:
                ren.color = new Color(0.1f, 0.9f, 0.3f);
                break;

            case LockType.Blue:
                ren.color = new Color(0.1f, 0.3f, 0.9f);
                break;

            case LockType.Purple:
                ren.color = new Color(0.5f, 0.3f, 0.9f);
                break;

            case LockType.Yellow:
                ren.color = new Color(0.9f, 0.9f, 0.1f);
                break;
        }
    }
}
