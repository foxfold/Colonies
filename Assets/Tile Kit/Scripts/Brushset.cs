using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Brushset", menuName = "Tile Kit/Brushset", order = 1)]
public class Brushset : ScriptableObject {
    public List<GameObject> Brushes;
}
