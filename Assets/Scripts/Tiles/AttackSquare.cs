using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSquare : MonoBehaviour {
    PlayerControls player;

    private void OnMouseDown()
    {
        Debug.Log("Clicked");

        player = (PlayerControls)FindObjectOfType(typeof(PlayerControls));

        if (this.transform.position.x > player.selectedUnit.GetComponent<GolemTemplate>().currentCoords.x)
        {
            //player.BresenhamTrace(player.selectedUnit.GetComponent<GolemTemplate>().currentCoords, new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y)));

        }
        else
        {
            //player.BresenhamTrace(new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y)), player.selectedUnit.GetComponent<GolemTemplate>().currentCoords);

        }

        //player.LOS();

        DestroyImmediate(this.gameObject);


    }

} 
