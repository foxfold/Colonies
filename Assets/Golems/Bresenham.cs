using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bresenham : MonoBehaviour {
    float deltaX;
    float deltaY;
    float deltaErr;
    float error;
    List<Vector2> availableCoords;

public void BresenhamTrace(Vector2 One, Vector2 Two)
    {
        deltaX = One.x - Two.x;
        deltaY = One.y - Two.y;
        deltaErr = Mathf.Abs(deltaX / deltaY);
        error = deltaErr - .5f;
        int y = Mathf.RoundToInt(One.y);
        availableCoords = new List<Vector2>();
        int Difference = Mathf.RoundToInt(Mathf.Abs(One.x - Two.x));

        for (int x = 0; x <= Difference;)
        {
            availableCoords.Add(new Vector2((x + Mathf.RoundToInt(One.x)), y));
            Debug.Log("Added Coords");
            error = error + deltaErr;
            if (error >= .5f)
            {
                y++;
                error = error - 1.0f;
            }
            x++;
        }
    }

public void BresenhamTrace2(Vector2 One, Vector2 Two) 
    {

    }

}
