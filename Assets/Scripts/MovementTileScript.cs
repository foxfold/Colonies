using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTileScript : MonoBehaviour {

    public GameObject Unit;
    public TileCoords MyCoords;
    GridCreator gridCreator;
    PlayerControls player;

    private void Start()
    {
        gridCreator = (GridCreator)FindObjectOfType(typeof(GridCreator));
        player = (PlayerControls)FindObjectOfType(typeof(PlayerControls));
    }

    private void OnMouseDown()
    {
        Unit.transform.position = new Vector3(MyCoords.x, MyCoords.y, -5);
        //Unit.GetComponent<GolemTemplate>().Moved = true;

        foreach(GameObject Square in player.movementSquareList)
        {
            if (Square != this.gameObject)
            {
                DestroyImmediate(Square);
            }
        }
        player.movementSquareList.Clear();
        gridCreator.tileDict[Unit.GetComponent<GolemTemplate>().currentCoords].GetComponent<TileData>().occupied = false;
        Unit.GetComponent<GolemTemplate>().currentCoords = MyCoords;
        gridCreator.tileDict[MyCoords].GetComponent<TileData>().occupied = true;
        player.Attack(player.selectedUnit.GetComponent<GolemTemplate>().currentCoords, player.selectedUnit.GetComponent<GolemTemplate>().currentCoords);
        DestroyImmediate(this.gameObject);
    }
} 
