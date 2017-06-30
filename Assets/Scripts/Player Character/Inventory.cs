using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<GameObject> Inv;
    public int numInRow = 5;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "pickupable")
        {
            transform.SetParent(Camera.main.gameObject.transform);
            Inv.Add(col.gameObject);
            col.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 999;
            col.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    void Update()
    {
        int row = Inv.Count / numInRow + 1;
        int column = 1;
        if (Inv.Count % numInRow == 0)
        {
            row = Inv.Count / numInRow;
        }

        for (int i = 0; i < Inv.Count; i++)
        {
            if (i >= numInRow && i % numInRow == 0)
            {
                row--;
                column = 1;
            }

            // Adjusts the position of the item to its inventory world-position
            Inv[i].gameObject.transform.position = Vector3.Lerp(
                Inv[i].gameObject.transform.position, 
                Camera.main.ScreenToWorldPoint(
                    new Vector3(column * 30, ( (row) * 30), 5)),
                0.1f);

            // Adjusts the scaling to avoid "GUI" scaling issues with different zoom levels
            Inv[i].gameObject.transform.localScale = Vector3.Lerp(
                Inv[i].gameObject.transform.localScale, 
                new Vector3(Camera.main.orthographicSize / 6, Camera.main.orthographicSize / 6, 1),
                0.1f);

            column++;
        }
    }
}
