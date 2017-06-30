using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class doorScript : MonoBehaviour {

    public GameObject player;
    public bool Open = false;


    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        float autoOpenDistance = Vector2.Distance(this.transform.position, player.gameObject.transform.position);
        if (autoOpenDistance < 1.0f)
        {
            if (GetComponent<DoorKeyColors>().locktype != DoorKeyColors.LockType.NoLock)
            {
                for (int i = 0; i < player.GetComponent<Inventory>().Inv.Count; i++)
                {
                    if (player.GetComponent<Inventory>().Inv[i].gameObject.GetComponent<DoorKeyColors>().locktype 
                        == gameObject.GetComponent<DoorKeyColors>().locktype)
                    {
                        Open = true;
                        GetComponent<Animator>().SetBool("Open", true);
                    }
                }
            }
            else
            {
                Open = true;
                GetComponent<Animator>().SetBool("Open", true);
            }

        }
        else
        {
            Open = false;
            GetComponent<Animator>().SetBool("Open", false);
        }
    }

    //Called by animation

    void EnableCollision()
    {
        GetComponent<Collider2D>().isTrigger = false;
    }

    void DisableCollision()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
