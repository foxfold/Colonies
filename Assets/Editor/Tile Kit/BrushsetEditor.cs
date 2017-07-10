using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Brushset))]
public class BrushsetEditor : Editor {

    public override void OnInspectorGUI()
    {
        //http://answers.unity3d.com/questions/8633/how-do-i-programmatically-assign-a-gameobject-to-a.html
        Brushset bs = (Brushset)target;
        GUILayout.Label("Brushset settings");
        bs.width = EditorGUILayout.IntSlider("Width", bs.width, 1, 32);
        bs.height = EditorGUILayout.IntSlider("Height", bs.height, 1, 32);



        GUILayout.BeginHorizontal();

        GUILayout.Box("", GUILayout.Width(16f), GUILayout.Height(16f));
        GameObject selGo;

        if (bs.selection)
        {
            selGo = bs.selection as GameObject;
            Sprite sprite = selGo.GetComponent<SpriteRenderer>().sprite;
            GUI.DrawTextureWithTexCoords(new Rect(GUILayoutUtility.GetLastRect().x,
                        GUILayoutUtility.GetLastRect().y,
                        GUILayoutUtility.GetLastRect().width,
                        GUILayoutUtility.GetLastRect().height),
                sprite.texture,
                new Rect(sprite.textureRect.x / (float)sprite.texture.width,
                        sprite.textureRect.y / (float)sprite.texture.height,
                        sprite.textureRect.width / (float)sprite.texture.width,
                        sprite.textureRect.height / (float)sprite.texture.height));

            string newShortName = sprite.texture.name;
            if (newShortName.Length > 10)
                newShortName = newShortName.Substring(0, 10) + "…";
            GUILayout.Label(newShortName);
            GUILayout.FlexibleSpace();
            GUILayout.Label("⇒");
            GUILayout.FlexibleSpace();
        }
        else
        {
            GUILayout.Label("Invalid selection");
            GUILayout.FlexibleSpace();
            //GUILayout.Label("⇒");
            //GUILayout.FlexibleSpace();
        }
        bs.selection = EditorGUILayout.ObjectField(bs.selection, typeof(GameObject), false);

        if (GUILayout.Button(" + "))
        {
            //add to dictionary
        }
        GUILayout.EndHorizontal();

        if (bs.selection != null)
        {
            selGo = bs.selection as GameObject;
            if (!selGo.GetComponent<TileData>() ||
                !selGo.GetComponent<SpriteRenderer>().sprite)
            {
                bs.selection = null;
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No valid tile selected. The tile prefab must have a sprite set and a TileData component.", MessageType.Warning);
        }
        



        //selection = pair.Value;
        //GUI.enabled = false;
        //
        //GUI.enabled = isGridSelected;
        //if (GUILayout.Button("-", GUILayout.Height(16f)))
        //{
        //    toRemove.Add(pair.Key);
        //    Repaint();
        //}
        //GUILayout.EndHorizontal();
    }
}
