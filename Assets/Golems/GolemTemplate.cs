using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemTemplate : MonoBehaviour
{
    public string Name = "Golem Name";
    public Arms arms;
    public Core core;
    public Frame frame;
    public Head head;
    public Legs legs;
    public Torso torso;
    public Weapon weapon1;
    public Weapon weapon2;

    public GolemStruct stats;
    public GolemEnums GE;
    public SpriteRenderer baseRenderer;
    public PlayerControls player;
    public TileCoords currentCoords;

    GridCreator gridCreator;
    TileData currentTile;

    public bool moved = false;
    public bool attacked = false;

    public Faction faction;

    public int x;
    public int y;

    SortingLayer[] sortingLayers;
    SortingLayer movementLayer; 


    public void Start()
    {
        // TEMPORARY CODE to get a layer
        SortingLayer[] sortingLayers = SortingLayer.layers;
        foreach (SortingLayer sl in sortingLayers)
        {
            if (sl.name == "Ground") { movementLayer = sl; } 
        }


        baseRenderer = this.GetComponent<SpriteRenderer>();
        player = (PlayerControls)FindObjectOfType(typeof(PlayerControls));
        gridCreator = (GridCreator)FindObjectOfType(typeof(GridCreator));
        GE = (GolemEnums)FindObjectOfType(typeof(GolemEnums));
        stats = GE.StructGen(arms, core, frame, head, legs, torso, weapon1, weapon2);
        currentCoords = new TileCoords(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), movementLayer);


        if (faction == Faction.Player)
        {
            baseRenderer.color = new Color(0.5f, 0.5f, 1f, 1f);
        }
        else if (faction == Faction.Enemy1 || faction == Faction.Enemy2)
        {
            baseRenderer.color = new Color(1f, 0f, 0f, 1f);
        }

        player.unitDict.Add(currentCoords, this.gameObject);
    }

    private void OnMouseDown()
    {

        if (faction == Faction.Player)
        {
            if (player.selectedUnit != null)
            {
                player.selectedUnit.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1f, 1f);
                foreach (GameObject Obj in player.movementSquareList)
                {
                    DestroyImmediate(Obj);
                }
            }

            player.selectedUnit = this.gameObject;
            player.selectedUnitMovement = stats.Movement;

            if (!moved)
            {
                player.Movement(currentCoords, currentCoords);
                player.unitDict.Remove(currentCoords);
                currentCoords = new TileCoords(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), movementLayer);
                player.unitDict.Add(currentCoords, this.gameObject);
                currentTile = gridCreator.tileDict[currentCoords].GetComponent<TileData>();
                currentTile.occupied = true;
            }
            else if (!attacked)
            {
                player.Attack(currentCoords, currentCoords);
            }
        }
    }

}