using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridCreator))]
public class TileKitGridEditor : Editor
{

    GridCreator gridCreator;
    GameObject clickedTile;
    public GameObject selectedGrid;
    GameObject previousSelection;

    void OnSceneGUI()
    {
        // Takes control of the Unity scene when a grid is selected
        //int controlID = ;
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

        gridCreator = Selection.activeGameObject.GetComponent<GridCreator>();

        if ((Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown) && !EditorApplication.isCompiling) 
        {
            //Undo.RecordObject(gridCreator, "Edited grid creator");

            Vector3 mousePos = Event.current.mousePosition;                                                 // Gets mouse position
            mousePos.y = Screen.height - mousePos.y - 36.0f;                                                // Corrects for odd Unity issue
            Vector3 mouseInWorld = SceneView.lastActiveSceneView.camera.ScreenToWorldPoint(mousePos);       // Converts mouse to world
            Vector2 clickedCoords = new Vector2(Mathf.RoundToInt(mouseInWorld.x), Mathf.RoundToInt(mouseInWorld.y));  // Converst to Vector2 coordinate system

            if (Event.current.button == 0)
            {
                gridCreator.UseTool(clickedCoords);
            }
            else if (Event.current.button == 1)
            { 
                gridCreator.PickColor(clickedCoords);
            }
        }
    }
}
