using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class GridCreator : MonoBehaviour
{
    public Object baseTile;
    public enum Interaction { Draw, Erase, Fill, Clear };
    public Dictionary<TileCoords, GameObject> tileDict = new Dictionary<TileCoords, GameObject>();
    public Dictionary<Sprite, GameObject> prefabDict = new Dictionary<Sprite, GameObject>();
    public Sprite selectedSprite;
    public Sprite defaultSprite; 
    public Interaction interaction = Interaction.Draw;
    public UnityEngine.Object spriteTexture2D;
    public int selectedSpriteInt = 0;
    public bool isSelected = false;
    public int brushSize = 1;
    public List<TileCoords> ranCoords;

    public bool flipX = false;
    public bool flipY = false;
    public float angle = 0;
    
    public int selectedLayer = 0;
    SortingLayer[] sortingLayerArray;

    PolygonCollider2D polyCollider;
    Sprite colSprite;

    public void Start()
    {
        tileDict.Clear();
        Transform parent = transform;
        var children = new List<GameObject>();
        foreach (Transform child in parent) children.Add(child.gameObject);
        for (int i = 0; i < children.Count; i++)
        {
            tileDict.Add(new TileCoords(children[i].GetComponent<TileData>().x, children[i].GetComponent<TileData>().y, children[i].GetComponent<TileData>().layer), children[i]);
        }
    }

    public void UseTool(Vector2 coordinates)
    {
        sortingLayerArray = SortingLayer.layers;
        TileCoords tileCoords = new TileCoords(Mathf.RoundToInt(coordinates.x), Mathf.RoundToInt(coordinates.y), sortingLayerArray[selectedLayer]);
        switch (interaction)
        {
            case Interaction.Draw:
                if (brushSize == 1)
                {
                    SetTile(tileCoords);
                }
                else
                {
                    ranCoords = new List<TileCoords>();
                    Brush(tileCoords, tileCoords);
                    ranCoords.Clear();
                }
                break;

            case GridCreator.Interaction.Erase:
                EraseTile(tileCoords);
                break;

            case GridCreator.Interaction.Fill:
                if (!tileDict.ContainsKey(tileCoords))
                {
                    SetTile(tileCoords);
                    tileDict[tileCoords].GetComponent<SpriteRenderer>().sprite = null;
                }
                ranCoords = new List<TileCoords>();
                Fill(tileCoords, tileDict[tileCoords].GetComponent<SpriteRenderer>().sprite, tileCoords);
                ranCoords.Clear();
                break;

            case GridCreator.Interaction.Clear:
                if (!tileDict.ContainsKey(tileCoords))
                {
                    SetTile(tileCoords);
                    tileDict[tileCoords].GetComponent<SpriteRenderer>().sprite = null;
                }
                ranCoords = new List<TileCoords>();
                Clear(tileCoords, tileDict[tileCoords].GetComponent<SpriteRenderer>().sprite, tileCoords);
                ranCoords.Clear();
                break;
        }
    }

    public void PickColor(Vector2 coordinates)
    {
        TileCoords tileCoords = new TileCoords(Mathf.RoundToInt(coordinates.x), Mathf.RoundToInt(coordinates.y), sortingLayerArray[selectedLayer]);
        if (tileDict.ContainsKey(tileCoords))
            selectedSprite = tileDict[tileCoords].GetComponent<SpriteRenderer>().sprite;
    }

    public void SetTile(TileCoords coordinates)
    {
        bool spawnedNewTile = false;
        GameObject newTile = null;
        Object fromType;
        Object toType;
        Object baseType = PrefabUtility.FindPrefabRoot(baseTile as GameObject);

        if (tileDict.ContainsKey(coordinates))
        {
            // Because it's an instance, uses GetPrefabParent
            fromType = PrefabUtility.GetPrefabParent(tileDict[coordinates]);

            if (prefabDict.ContainsKey(selectedSprite))
                toType = PrefabUtility.FindPrefabRoot(prefabDict[selectedSprite]);
            else
                toType = baseType;

            if (fromType != toType && (fromType != baseType || toType != baseType)) 
            {
                Undo.DestroyObjectImmediate(tileDict[coordinates]);

                if (prefabDict.ContainsKey(selectedSprite))
                {
                    newTile = (GameObject)PrefabUtility.InstantiatePrefab(prefabDict[selectedSprite]);
                    Undo.RegisterCreatedObjectUndo(newTile, "draw tiles");
                    spawnedNewTile = true;
                }
                else
                {
                    newTile = (GameObject)PrefabUtility.InstantiatePrefab(baseTile);
                    Undo.RegisterCreatedObjectUndo(newTile, "draw tiles");
                    spawnedNewTile = true;
                }

                if (tileDict.ContainsKey(coordinates))
                    tileDict[coordinates] = newTile;
                else
                    tileDict.Add(coordinates, newTile);
            }
            else 
            {
                if (tileDict[coordinates].GetComponent<SpriteRenderer>().sprite != selectedSprite)
                    tileDict[coordinates].GetComponent<SpriteRenderer>().sprite  = selectedSprite;
            }
        }
        else
        {
            if (prefabDict.ContainsKey(selectedSprite))
            {
                newTile = (GameObject)PrefabUtility.InstantiatePrefab(prefabDict[selectedSprite]);
                Undo.RegisterCreatedObjectUndo(newTile, "draw tiles");
                spawnedNewTile = true;
            }
            else
            {
                newTile = (GameObject)PrefabUtility.InstantiatePrefab(baseTile);
                Undo.RegisterCreatedObjectUndo(newTile, "draw tiles");
                spawnedNewTile = true;
            }
            
        }

        if (spawnedNewTile && newTile != null)
        {
            newTile.transform.position = new Vector3(coordinates.x, coordinates.y, 0);
            newTile.GetComponent<SpriteRenderer>().sortingLayerName = coordinates.layer.name;
            newTile.transform.parent = transform;
            newTile.GetComponent<TileData>().x = coordinates.x;
            newTile.GetComponent<TileData>().y = coordinates.y;
            newTile.GetComponent<TileData>().layer = coordinates.layer;
            newTile.GetComponent<SpriteRenderer>().sprite = selectedSprite;

            if (tileDict.ContainsKey(coordinates))
                tileDict[coordinates] = newTile;
            else
                tileDict.Add(coordinates, newTile);
        }
            
        tileDict[coordinates].GetComponent<SpriteRenderer>().flipX = flipX;
        tileDict[coordinates].GetComponent<SpriteRenderer>().flipY = flipY;
        tileDict[coordinates].transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);

        if (tileDict[coordinates].GetComponent<PolygonCollider2D>() != null)
            DestroyImmediate(tileDict[coordinates].GetComponent<PolygonCollider2D>());

        tileDict[coordinates].AddComponent<PolygonCollider2D>();
    }

    public void Brush(TileCoords coordinates, TileCoords originalCoords)
    {
        // TODO - Keep list of all coordinates already run, return if those are re-run

        // Returns if this tile has already been checked
        if (ranCoords.Contains(coordinates)) { return; }

        // Returns if we are too far from the origin (to prevent infinite flooding)
        if (Vector2.Distance(new Vector2(originalCoords.x, originalCoords.y), new Vector2(coordinates.x, coordinates.y)) > brushSize - 1) { return; }

        // If no tile exists in a set of coords, it's created and set to null
        if (!tileDict.ContainsKey(coordinates))
        {
            SetTile(coordinates);
        }

        // Sets the new sprite
        SetTile(coordinates);
        ranCoords.Add(coordinates);

        // Produces the coordinates to test
        TileCoords North = new TileCoords(coordinates.x, coordinates.y + 1, sortingLayerArray[selectedLayer]);
        TileCoords South = new TileCoords(coordinates.x, coordinates.y - 1, sortingLayerArray[selectedLayer]);
        TileCoords East = new TileCoords(coordinates.x + 1, coordinates.y, sortingLayerArray[selectedLayer]);
        TileCoords West = new TileCoords(coordinates.x - 1, coordinates.y, sortingLayerArray[selectedLayer]);

        // Recursively runs testing the directional tiles
        Brush(North, originalCoords);
        Brush(South, originalCoords);
        Brush(East, originalCoords);
        Brush(West, originalCoords);
    }

    public void EraseTile(TileCoords coordinates)
    {
        // ERASE tool - Deletes existant tile, if it exists
        if (tileDict.ContainsKey(coordinates))
        {
            Undo.DestroyObjectImmediate(tileDict[coordinates]);
            tileDict.Remove(coordinates);
        }
    }

    private void Update()
    {
        Undo.undoRedoPerformed += OnUndoRedo; // subscribe to the event
    }

    void OnUndoRedo()
    {
        List<TileCoords> nonexistantCoords = new List<TileCoords>();
        if (tileDict.Count != 0)
        {
            foreach (TileCoords coord in tileDict.Keys)
            {
                if (tileDict[coord] == null) { nonexistantCoords.Add(coord); }
            }
            for (int i = 0; i < nonexistantCoords.Count; i++)
            {
                tileDict.Remove(nonexistantCoords[i]);
            }
        }
    }

    public void Fill(TileCoords coordinates, Sprite originalSprite, TileCoords originalCoords)
    {
        Stack<TileCoords> stack = new Stack<TileCoords>();
        stack.Push(coordinates);

        while (stack.Count != 0)
        {
            TileCoords coords = stack.Pop();
            // Returns if we are too far from the origin (to prevent infinite flooding)
            if (originalSprite == null && Vector2.Distance(new Vector2(originalCoords.x, originalCoords.y), new Vector2(coordinates.x, coordinates.y)) > 32) { continue; }

            // If no tile exists in a set of coords, it's created and set to null
            if (!tileDict.ContainsKey(coords))
            {
                if (originalSprite == null)
                {
                    SetTile(coords);
                    tileDict[coords].GetComponent<SpriteRenderer>().sprite = null;
                }
                else
                {
                    continue;
                }
            }

            // Returns if the tile is already the target sprite, or is not the sprite being replaced.
            if (tileDict[coords].GetComponent<SpriteRenderer>().sprite == selectedSprite) { continue; }
            if (tileDict[coords].GetComponent<SpriteRenderer>().sprite != originalSprite) { continue; }

            // Sets the new sprite
            SetTile(coords);

            // Produces the coordinates to test
            TileCoords North = new TileCoords(coords.x, coords.y + 1, sortingLayerArray[selectedLayer]);
            TileCoords South = new TileCoords(coords.x, coords.y - 1, sortingLayerArray[selectedLayer]);
            TileCoords East = new TileCoords(coords.x + 1, coords.y, sortingLayerArray[selectedLayer]);
            TileCoords West = new TileCoords(coords.x - 1, coords.y, sortingLayerArray[selectedLayer]);

            stack.Push(North);
            stack.Push(South);
            stack.Push(East);
            stack.Push(West);
        }
    }

    public void Clear(TileCoords coordinates, Sprite originalSprite, TileCoords originalCoords)
    {
        if (originalSprite == null) { return; }
        Stack<TileCoords> stack = new Stack<TileCoords>();
        stack.Push(coordinates);

        while (stack.Count != 0)
        {
            TileCoords coords = stack.Pop();

            // If no tile exists in a set of coords, it's created and set to null
            if (!tileDict.ContainsKey(coords)) { continue; }

            // Returns if the tile is already the target sprite, or is not the sprite being replaced.
            if (tileDict[coords].GetComponent<SpriteRenderer>().sprite == null) { continue; }
            if (tileDict[coords].GetComponent<SpriteRenderer>().sprite != originalSprite) { continue; }

            // Sets the new sprite
            EraseTile(coords);

            // Produces the coordinates to test
            TileCoords North = new TileCoords(coords.x, coords.y + 1, sortingLayerArray[selectedLayer]);
            TileCoords South = new TileCoords(coords.x, coords.y - 1, sortingLayerArray[selectedLayer]);
            TileCoords East = new TileCoords(coords.x + 1, coords.y, sortingLayerArray[selectedLayer]);
            TileCoords West = new TileCoords(coords.x - 1, coords.y, sortingLayerArray[selectedLayer]);

            stack.Push(North);
            stack.Push(South);
            stack.Push(East);
            stack.Push(West);
        }
    }
}