using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Brushset", menuName = "Tile Kit/Brushset", order = 1)]
public class Brushset : ScriptableObject {
    public Dictionary<Vector2, GameObject> brushes;
    public int width = 12;
    public int height = 8;
    public UnityEngine.Object selection;
}
