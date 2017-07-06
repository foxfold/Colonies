using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileData : MonoBehaviour {
    //public Vector2 myCoords;]
    [ReadOnly] public bool occupied = false;
    [ReadOnly] public int x, y;
    [ReadOnly] public SortingLayer layer;
    public bool impassible;

    public void OnMouseDown()
    {
        Debug.Log("(" + x + ", " + y + ", " + layer.name + ")");
    }
}

public struct TileCoords : IEquatable<TileCoords> {

    public int x, y;
    public SortingLayer layer;

    public TileCoords(int x, int y, SortingLayer layer)
    {
        this.x = x;
        this.y = y;
        this.layer = layer;
    }

    public bool Equals(TileCoords o)
    {
        return x == o.x &&
               y == o.y &&
               layer.name == o.layer.name;            
    }

    public override bool Equals(object o)
    {
        return false;
        // Can't equal anything but a TileCoords, which would run the other overload
    }

    public static bool operator ==(TileCoords a, TileCoords b)
    {
        return a.x == b.x && a.y == b.y && a.layer.name == b.layer.name;
    }

    public static bool operator !=(TileCoords a, TileCoords b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return string.Format("{0}-{1}-{2}", x, y, layer).GetHashCode();
    }
} 
