using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase { Movement, Attacking, None}

public class PlayerControls : MonoBehaviour {

    public GameObject selectedUnit;
    public int selectedUnitMovement;
    public GameObject movementSquare;
    public GameObject attackSquare;
    public GameObject LOSSquare;

    public float speed;
    private Phase phase;

    GridCreator gridCreator;
    GameObject clickedTile;
    public List<TileCoords> availableCoords = new List<TileCoords>();
    public Dictionary<TileCoords, bool> moveableArea = new Dictionary<TileCoords, bool>();
    public List<GameObject> movementSquareList;
    public List<GameObject> attackSquareList;
    public List<GameObject> losSquareList;

    public Dictionary<TileCoords, GameObject> unitDict = new Dictionary<TileCoords, GameObject>();

    float deltaX;
    float deltaY;
    float deltaErr;
    float error; 

    SortingLayer[] sortingLayers;
    SortingLayer movementLayer;

    // Use this for initialization
    void Start()
    {
        // Note from TJ: This returns only one grid (with all its layers). If you ever use more than one grid for any reason it will return the first.
        gridCreator = (GridCreator)FindObjectOfType(typeof(GridCreator));

        //TEMPORARY?
        SortingLayer[] sortingLayers = SortingLayer.layers;
        foreach (SortingLayer sl in sortingLayers)
        {
            if (sl.name == "Ground") { movementLayer = sl; }
        }
    }

    private void FixedUpdate()
    {
        float inputUp = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * inputUp * speed);
        float inputRight = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * inputRight * speed);
    }

    public void Movement(TileCoords coordinates, TileCoords originalCoords)
    {
        List<TileCoords> CheckedCoords = new List<TileCoords>();
        CheckedCoords.Add(coordinates);
        List<TileCoords> PreviouslyCheckedCoords = new List<TileCoords>();
        List<TileCoords> NextWave = new List<TileCoords>();
        NextWave.Add(coordinates);
        availableCoords.Clear();
        availableCoords = new List<TileCoords>();

        foreach (KeyValuePair<TileCoords, GameObject> Pair in gridCreator.tileDict)
        {
            if (Pair.Value.gameObject.GetComponent<TileData>().occupied)
            {
            }
        }

        for (int i = 0; i < selectedUnitMovement; i++)
        {
            foreach (TileCoords Coords in NextWave)
            {
                PreviouslyCheckedCoords.Add(Coords);

                TileCoords North = new TileCoords(Coords.x, Coords.y + 1, movementLayer);

                if (gridCreator.tileDict.ContainsKey(North) && !CheckedCoords.Contains(North) && !gridCreator.tileDict[North].GetComponent<TileData>().occupied)
                {
                    availableCoords.Add(North);
                }

                TileCoords South = new TileCoords(Coords.x, Coords.y - 1, movementLayer);
                if (gridCreator.tileDict.ContainsKey(South) && !CheckedCoords.Contains(South) && !gridCreator.tileDict[South].GetComponent<TileData>().occupied)
                {
                    availableCoords.Add(South);
                }

                TileCoords East = new TileCoords(Coords.x + 1, Coords.y, movementLayer);
                if (gridCreator.tileDict.ContainsKey(East) && !CheckedCoords.Contains(East) && !gridCreator.tileDict[East].GetComponent<TileData>().occupied)
                {
                    availableCoords.Add(East);
                }

                TileCoords West = new TileCoords(Coords.x - 1, Coords.y, movementLayer);
                if (gridCreator.tileDict.ContainsKey(West) && !CheckedCoords.Contains(West) && !gridCreator.tileDict[West].GetComponent<TileData>().occupied)
                {
                    availableCoords.Add(West);
                }
                CheckedCoords.Add(North);
                CheckedCoords.Add(South);
                CheckedCoords.Add(East);
                CheckedCoords.Add(West);
            }
            NextWave.Clear();
            NextWave = new List<TileCoords>();
            foreach (TileCoords Coords in availableCoords)
            {
                if (!PreviouslyCheckedCoords.Contains(Coords))
                {
                    NextWave.Add(Coords);
                }
            }
        }

        movementSquareList = new List<GameObject>();
        foreach (TileCoords Coords in availableCoords)
        {
            if (!gridCreator.tileDict[Coords].GetComponent<TileData>().occupied)
            {
                GameObject NewSquare = (Instantiate(
                movementSquare,
                new Vector3(
                    Coords.x,
                    Coords.y,
                    -7),
                Quaternion.identity));
                movementSquareList.Add(NewSquare);
                NewSquare.GetComponent<MovementTileScript>().Unit = selectedUnit.gameObject;
                NewSquare.GetComponent<MovementTileScript>().MyCoords = new TileCoords(Coords.x, Coords.y, movementLayer);
            }
        }
    }


public void Attack(TileCoords coordinates, TileCoords originalCoords)
    {
        List<TileCoords> CheckedCoords = new List<TileCoords>();
        CheckedCoords.Add(coordinates);

        List<TileCoords> PreviouslyCheckedCoords = new List<TileCoords>();

        List<TileCoords> NextWave = new List<TileCoords>();
        NextWave.Add(coordinates);
        availableCoords.Clear();
        availableCoords = new List<TileCoords>();

        for (int i = 0; i < selectedUnitMovement; i++)
        {
            foreach (TileCoords Coords in NextWave)
            {
                PreviouslyCheckedCoords.Add(Coords);

                TileCoords North = new TileCoords(Coords.x, Coords.y + 1, movementLayer);

                if (gridCreator.tileDict.ContainsKey(North) && !CheckedCoords.Contains(North) && !gridCreator.tileDict[North].GetComponent<TileData>().occupied)
                {
                    availableCoords.Add(North);
                }

                TileCoords South = new TileCoords(Coords.x, Coords.y - 1, movementLayer);
                if (gridCreator.tileDict.ContainsKey(South) && !CheckedCoords.Contains(South) && !gridCreator.tileDict[South].GetComponent<TileData>().occupied)
                {
                    availableCoords.Add(South);
                }

                TileCoords East = new TileCoords(Coords.x + 1, Coords.y, movementLayer);
                if (gridCreator.tileDict.ContainsKey(East) && !CheckedCoords.Contains(East) && !gridCreator.tileDict[East].GetComponent<TileData>().occupied)
                {
                    availableCoords.Add(East);
                }

                TileCoords West = new TileCoords(Coords.x - 1, Coords.y, movementLayer);
                if (gridCreator.tileDict.ContainsKey(West) && !CheckedCoords.Contains(West) && !gridCreator.tileDict[West].GetComponent<TileData>().occupied)
                {
                    availableCoords.Add(West);
                }
                CheckedCoords.Add(North);
                CheckedCoords.Add(South);
                CheckedCoords.Add(East);
                CheckedCoords.Add(West);
            }
            NextWave.Clear();
            NextWave = new List<TileCoords>();
            foreach (TileCoords Coords in availableCoords)
            {
                if (!PreviouslyCheckedCoords.Contains(Coords))
                {
                    NextWave.Add(Coords);
                }
            }
        }

        attackSquareList = new List<GameObject>();
        foreach (TileCoords Coords in availableCoords)
        {
            foreach (KeyValuePair<TileCoords, GameObject> Unit in unitDict)
            {
                if (Coords == Unit.Key)
                {
                    if (Unit.Value.GetComponent<GolemTemplate>().faction == Faction.Enemy1)
                    {
                    GameObject NewSquare = (Instantiate(
                                            attackSquare,
                                            new Vector3(
                                                Coords.x,
                                                Coords.y,
                                                       -7),
                                    Quaternion.identity));
                    attackSquareList.Add(NewSquare);
                    }
                }
            }
        }
    }
    // REPLACE ALL Vector2 WITH TileCoords. You can get TileCoords.x, .y, and .layer.
    //public void BresenhamTrace(Vector2 One, Vector2 Two)
    //{
    //    deltaX = One.x - Two.x;
    //    deltaY = One.y - Two.y;
    //    deltaErr = Mathf.Abs(deltaX / deltaY);
    //    error = deltaErr - .5f;
    //    int y = Mathf.RoundToInt(One.y);
    //    availableCoords = new List<TileCoords>();
    //    int Difference = Mathf.RoundToInt(Mathf.Abs(One.x - Two.x));

    //    for (int x = 0; x <= Difference;)
    //    {
    //        availableCoords.Add(new Vector2((x +Mathf.RoundToInt(One.x)), y));
    //        Debug.Log("Added Coords");
    //        error = error + deltaErr;
    //        if (error >= .5f)
    //        {
    //            y++;
    //            error = error - 1.0f;
    //        }
    //        x++;
    //    }
    //}

    //    public void LOS()
    //{
    //    Debug.Log("LOS Ran");
    //    foreach (Vector2 Coords in availableCoords)
    //    {
    //                    GameObject NewSquare = (Instantiate(
    //                                            LOSSquare,
    //                                            new Vector3(
    //                                                Coords.x,
    //                                                Coords.y,
    //                                                       -7),
    //                                    Quaternion.identity));
    //                    losSquareList.Add(NewSquare);
    //    }
    //}

}