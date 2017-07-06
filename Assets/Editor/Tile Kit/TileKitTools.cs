using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[Serializable]
public struct Layer
{
    public string name;
    public bool collision;
}

public class TileKitTools : EditorWindow
{
    // References to relevant grid
    public GameObject selectedGrid;
    public GridCreator selectedGridCreator;

    // Info passed to tiles on creation
    public enum oldLayersEnum { Ground, Middle, Top };
    //oldLayersEnum selectedLayer = oldLayersEnum.Ground;
    public Vector2 scale = new Vector2(1, 1);

    // References to sprite information
    public UnityEngine.Object myPriorTexture;
    public UnityEngine.Object myTexture;
    public Sprite[] sprites = null;

    // Editor variables
    public string[] toolbarStrings = new string[] { "Draw", "Erase", "Fill", "Clear" };
    public int toolbarInt = 0;
    public int selTileInt = 0;
    public Vector2 scrollPosition;
    bool isGridSelected = false;
    public bool compiled = false;
    public int selectedGridInt = 0;

    // Editor styles
    public GUIStyle textureStyle;
    public GUIStyle textureStyleAct;
    public GUIStyle rotButton;

    // For the layer editor
    public List<Layer> Layers = new List<Layer>();
    public string[] LayerNames = null;

    // For prefab selection
    public UnityEngine.Object selection;
    public UnityEngine.Object newSelectionObj;
    public GameObject newSelectionGameObject;
    public TileData selectedTD;
    public bool showPrefabSetup = false;

    [MenuItem("Window/Foxfold/Tile Kit tools")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TileKitTools), false, "Tile Kit tools");
    }

    void onEnable()
    {

    }

    void OnSelectionChange() // Checks for grid selection change, selects the current object if it is a grid.
    {
        if (Selection.activeGameObject != null)
        {
            if (Selection.activeGameObject.tag == "Grid")
            {
                if (selectedGrid != null)
                {
                    selectedGridCreator.isSelected = false;
                }
                selectedGrid = Selection.activeGameObject;
                selectedGridCreator = selectedGrid.GetComponent<GridCreator>();
                selectedGridCreator.isSelected = true;
                isGridSelected = true;
            }
            else
            {
                if (selectedGrid != null)
                {
                    selectedGridCreator.isSelected = false;
                }
                isGridSelected = false;
            }
        }
        else
        {
            if (selectedGrid != null)
            {
                selectedGridCreator.isSelected = false;
            }
            isGridSelected = false;
        }
        Repaint();
    }

    private void Update()
    {
        // Sets a bool when compiling starts, and when it ends if the bool was tripped it runs init
        if (EditorApplication.isCompiling) { compiled = true; }
        if (!EditorApplication.isCompiling && compiled) { Initialize(); compiled = false; }

        // Allows right-click-to-select to update the UI instantly
        if (selectedGrid != null
            && myPriorTexture == myTexture
            && myPriorTexture != null
            && myTexture != null)
        {
            if (sprites[selTileInt] != selectedGridCreator.selectedSprite)
            {
                for (int i = 0; i < sprites.Length; i++)
                {
                    if (sprites[i] == selectedGridCreator.selectedSprite) { selTileInt = i; }
                    Repaint();
                }
            }
        }
    }

    void Initialize() // Re-selects grid, re-generates sprites, and re-generates tileDict. 
    {
        // This information is typically lost when any of the relevant scripts are recompiled, or windows are opened for the first time.
        OnSelectionChange();

        if (myTexture != null)
        {
            sprites = Resources.LoadAll<Sprite>(myTexture.name);
            myPriorTexture = myTexture;
        }

        if (selectedGridCreator != null)
        {
            selectedGridCreator.tileDict.Clear();
            Transform parent = selectedGrid.transform;
            var children = new List<GameObject>();
            foreach (Transform child in parent) children.Add(child.gameObject);
            for (int i = 0; i < children.Count; i++)
            {
                try
                {
                    selectedGridCreator.tileDict.Add(new TileCoords(children[i].GetComponent<TileData>().x, children[i].GetComponent<TileData>().x, children[i].GetComponent<TileData>().layer), children[i]);
                }
                catch
                {
                    DestroyImmediate(children[i]);
                }
            }
        }

    }

    void OnGUI()
    {

        Texture2D icon = EditorGUIUtility.Load("Assets/Resources/foxfold2.png") as Texture2D;
        titleContent.image = icon;

        //Layers = new ReorderableList();

        //Layers = new ReorderableList(Layers, Laer, true, true, true, true);




        //public ReorderableList(SerializedObject serializedObject,
        //    SerializedProperty elements, bool draggable, bool displayHeader,
        //    bool displayAddButton, bool displayRemoveButton)


        textureStyle = new GUIStyle(GUI.skin.button);
        textureStyle.margin = new RectOffset(4, 4, 4, 4);
        textureStyle.normal.background = null;
        textureStyle.fixedWidth = 28f;
        textureStyle.fixedHeight = 28f;
        textureStyleAct = new GUIStyle(textureStyle);
        textureStyleAct.margin = new RectOffset(4, 4, 4, 4);
        textureStyleAct.normal.background = textureStyle.active.background;


        if (selectedGrid != null) { myTexture = selectedGridCreator.spriteTexture2D; }

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Add new grid"))
        {
            selectedGrid = new GameObject("Grid");
            selectedGrid.AddComponent<GridCreator>();
            selectedGridCreator = selectedGrid.GetComponent<GridCreator>();
            selectedGrid.tag = "Grid";
            GameObject[] creationSelection = new GameObject[1] { selectedGrid };
            Selection.objects = creationSelection;
        }

        if (FindObjectsOfType(typeof(GridCreator)).Length == 0 || selectedGrid == null)
        {
            GUILayout.EndHorizontal();
            GUILayout.Label("Please use the controls above to create a new grid.\n\nIf you already have a grid, make sure the parent object has a GridCreator attached.", EditorStyles.wordWrappedLabel);
        }
        else
        {
            if (myPriorTexture != myTexture && myTexture != null)
            {
                sprites = Resources.LoadAll<Sprite>(myTexture.name); 
                myPriorTexture = myTexture;
                selTileInt = 0;
            }
            GUI.enabled = isGridSelected;


            GUILayout.EndHorizontal();

            // Allows selection of a texture, which is then converted into sprites for in-game use
            // and then into an array of textures for button use.
            GUILayout.BeginHorizontal(); // All tools
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical(GUILayout.Width(80f)); // Rot/Flip/Preview tools
            rotButton = new GUIStyle(GUI.skin.button);
            rotButton.fontSize = 16;
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            if (GUILayout.Button("↺", rotButton))
            {
                selectedGridCreator.angle -= 90;
                if (selectedGridCreator.angle == -90) { selectedGridCreator.angle = 270; }
            }

            if (GUILayout.Button("↻", rotButton))
            {
                selectedGridCreator.angle += 90;
                if (selectedGridCreator.angle == 360) { selectedGridCreator.angle = 0; }
            }
            GUILayout.EndVertical();

            Matrix4x4 matrixBackup = GUI.matrix;

            GUILayout.BeginVertical();
            GUILayout.Box("", GUILayout.Width(44f), GUILayout.Height(44f));

            Vector2 adjPos = new Vector2(
                GUILayoutUtility.GetLastRect().position.x + (GUILayoutUtility.GetLastRect().width * 0.5f),
                GUILayoutUtility.GetLastRect().position.y + (GUILayoutUtility.GetLastRect().height * 0.5f));



            if (selectedGridCreator.flipX) { scale.x = -1; } else { scale.x = 1; }
            if (selectedGridCreator.flipY) { scale.y = -1; } else { scale.y = 1; }

            GUIUtility.ScaleAroundPivot(scale, adjPos);
            GUIUtility.RotateAroundPivot(selectedGridCreator.angle, adjPos);


            if (sprites.Length > 0)
            {
                GUI.DrawTextureWithTexCoords(new Rect(GUILayoutUtility.GetLastRect().x + 2f,
                                                  GUILayoutUtility.GetLastRect().y + 2f,
                                                  GUILayoutUtility.GetLastRect().width - 4f,
                                                  GUILayoutUtility.GetLastRect().height - 4f),
                                         sprites[selTileInt].texture,
                                         new Rect(sprites[selTileInt].textureRect.x / (float)sprites[selTileInt].texture.width, // - sprites[selTileInt].textureRectOffset.x,
                                                  sprites[selTileInt].textureRect.y / (float)sprites[selTileInt].texture.height, // - sprites[selTileInt].textureRectOffset.y,
                                                  sprites[selTileInt].textureRect.width / (float)sprites[selTileInt].texture.width,
                                                  sprites[selTileInt].textureRect.height / (float)sprites[selTileInt].texture.height));
            }
            GUI.matrix = matrixBackup;
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            selectedGridCreator.flipX = GUILayout.Toggle(selectedGridCreator.flipX, "⇄", rotButton);
            selectedGridCreator.flipY = GUILayout.Toggle(selectedGridCreator.flipY, "⇅", rotButton);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(selectedGridCreator.angle + "°");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.FlexibleSpace();

            // TOOLBAR to select drawing mode.
            GUILayout.BeginVertical();
            toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);
            if (toolbarInt == 0) // Draw
            {
                selectedGridCreator.interaction = GridCreator.Interaction.Draw;
                //GUILayout.Label("This will replace all tiles in a given radius");
            }
            else if (toolbarInt == 1) // Erase
            {
                selectedGridCreator.interaction = GridCreator.Interaction.Erase;
                //GUILayout.Label("Click (and drag) to erase tiles on the selected grid.");

            }
            else if (toolbarInt == 2) // Fill
            {
                selectedGridCreator.interaction = GridCreator.Interaction.Fill;
                //GUILayout.Label("Replaces the entire shape of the tile you click on with the selected tile.");
            }
            else if (toolbarInt == 3) // Clear
            {
                selectedGridCreator.interaction = GridCreator.Interaction.Clear;
                //GUILayout.Label("Erases the entire shape of the tile you click on.");
            }



            bool isBrushEnabled = false;
            if (toolbarInt == 0 && isGridSelected) { isBrushEnabled = true; }
            GUI.enabled = isBrushEnabled;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Radius");
            selectedGridCreator.brushSize = EditorGUILayout.IntSlider(selectedGridCreator.brushSize, 1, 4);
            GUILayout.EndHorizontal();
            GUI.enabled = isGridSelected;

            GUILayout.BeginHorizontal();
            GUILayout.Label("Layer");

            SortingLayer[] sortingLayerArray = SortingLayer.layers;
            String[] sortingLayerNames = new String[sortingLayerArray.Length];
            int[] sortingLayerIndexes = new int[sortingLayerArray.Length];

            for (int i = 0; i < sortingLayerArray.Length; i++)
            {
                sortingLayerNames[i] = sortingLayerArray[i].name;
                sortingLayerIndexes[i] = i;
            }

            selectedGridCreator.selectedLayer = EditorGUILayout.IntPopup(selectedGridCreator.selectedLayer, sortingLayerNames, sortingLayerIndexes);

            if (GUILayout.Button("Erase", GUILayout.Height(14)))
            {
                if (EditorUtility.DisplayDialog("Erase this sheet?", "Are you sure you want to delete every tile of the currently selected grid? This can not be undone.\n\nIf you wish to try and recover a deleted grid, loading an earlier copy of the scene may work.", "Delete", "Cancel"))
                {
                    selectedGridCreator.tileDict.Clear();
                    Transform parent = selectedGrid.transform;
                    var children = new List<GameObject>();
                    foreach (Transform child in parent) children.Add(child.gameObject);
                    children.ForEach(child => DestroyImmediate(child));
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            // Creates textures from spritesheet for buttons, prepares spritesheet


            if (myPriorTexture == myTexture && myPriorTexture != null && myTexture != null)
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition);

                GUILayout.BeginHorizontal(GUILayout.Width(position.width - 50));

                float ctr = 0.0f;
                int index = 0;
                for (int i = 0; i < sprites.Length; i++)
                {
                    if (ctr > sprites[i].textureRect.x)
                    {
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                    }
                    ctr = sprites[i].textureRect.x;
                    if (i == selTileInt)
                    {
                        GUILayout.Button("", textureStyleAct, GUILayout.Width(20f), GUILayout.Height(20f));
                        GUI.DrawTextureWithTexCoords(new Rect(GUILayoutUtility.GetLastRect().x + 4f,
                                                              GUILayoutUtility.GetLastRect().y + 4f,
                                                              GUILayoutUtility.GetLastRect().width,
                                                              GUILayoutUtility.GetLastRect().height),
                                                     sprites[i].texture,
                                                     new Rect(sprites[i].textureRect.x / (float)sprites[i].texture.width,
                                                              sprites[i].textureRect.y / (float)sprites[i].texture.height,
                                                              sprites[i].textureRect.width / (float)sprites[i].texture.width,
                                                              sprites[i].textureRect.height / (float)sprites[i].texture.height));
                    }
                    else
                    {
                        if (GUILayout.Button("", textureStyle, GUILayout.Width(20f), GUILayout.Height(20f)))
                        {
                            //selectedGridCreator.selectedSprite = sprites[i];
                            selTileInt = i;
                        }
                        GUI.DrawTextureWithTexCoords(new Rect(GUILayoutUtility.GetLastRect().x + 4f,
                                                              GUILayoutUtility.GetLastRect().y + 4f,
                                                              GUILayoutUtility.GetLastRect().width,
                                                              GUILayoutUtility.GetLastRect().height),
                                                     sprites[i].texture,
                                                     new Rect(sprites[i].textureRect.x / (float)sprites[i].texture.width,
                                                              sprites[i].textureRect.y / (float)sprites[i].texture.height,
                                                              sprites[i].textureRect.width / (float)sprites[i].texture.width,
                                                              sprites[i].textureRect.height / (float)sprites[i].texture.height));
                    }
                    index++;
                }

                GUILayout.EndHorizontal();
                GUILayout.EndScrollView();


                if (selTileInt < sprites.Length)
                {
                    selectedGridCreator.selectedSprite = sprites[selTileInt];
                }
            }

            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Select spritesheet");
            GUILayout.FlexibleSpace();
            selectedGridCreator.spriteTexture2D = EditorGUILayout.ObjectField(selectedGridCreator.spriteTexture2D, typeof(Texture2D), false);
            GUILayout.EndHorizontal();

            showPrefabSetup = EditorGUILayout.Foldout(showPrefabSetup, "Show prefab setup", true);
            if (showPrefabSetup)
            {
                List<Sprite> toRemove = new List<Sprite>();
                foreach (KeyValuePair<Sprite, GameObject> pair in selectedGridCreator.prefabDict)
                {

                    GUILayout.BeginHorizontal();

                    GUILayout.Box("", GUILayout.Width(16f), GUILayout.Height(16f));
                    GUI.DrawTextureWithTexCoords(new Rect(GUILayoutUtility.GetLastRect().x,
                                          GUILayoutUtility.GetLastRect().y,
                                          GUILayoutUtility.GetLastRect().width,
                                          GUILayoutUtility.GetLastRect().height),
                                 sprites[selTileInt].texture,
                                 new Rect(pair.Key.textureRect.x / (float)pair.Key.texture.width,
                                          pair.Key.textureRect.y / (float)pair.Key.texture.height,
                                          pair.Key.textureRect.width / (float)pair.Key.texture.width,
                                          pair.Key.textureRect.height / (float)pair.Key.texture.height));

                    //GUILayout.Label(pair.Key.name);
                    string newShortName = pair.Key.name;
                    if (newShortName.Length > 10)
                        newShortName = newShortName.Substring(0, 10) + "…";
                    GUILayout.Label(newShortName);
                    GUILayout.FlexibleSpace();
                    GUILayout.Label("⇒");
                    GUILayout.FlexibleSpace();

                    selection = pair.Value;
                    GUI.enabled = false;
                    selection = EditorGUILayout.ObjectField(selection, typeof(GameObject), false);
                    GUI.enabled = isGridSelected;
                    if (GUILayout.Button("-", GUILayout.Height(16f)))
                    {
                        toRemove.Add(pair.Key);
                        Repaint();
                    }
                    GUILayout.EndHorizontal();
                }

                for (int i = 0; i < toRemove.Count; i++)
                {
                    selectedGridCreator.prefabDict.Remove(toRemove[i]);
                }

                GUILayout.Space(16);
                GUILayout.BeginHorizontal();

                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                GUILayout.Box("", GUILayout.Width(16f), GUILayout.Height(16f));
                GUI.DrawTextureWithTexCoords(new Rect(GUILayoutUtility.GetLastRect().x,
                                      GUILayoutUtility.GetLastRect().y,
                                      GUILayoutUtility.GetLastRect().width,
                                      GUILayoutUtility.GetLastRect().height),
                             sprites[selTileInt].texture,
                             new Rect(sprites[selTileInt].textureRect.x / (float)sprites[selTileInt].texture.width,
                                      sprites[selTileInt].textureRect.y / (float)sprites[selTileInt].texture.height,
                                      sprites[selTileInt].textureRect.width / (float)sprites[selTileInt].texture.width,
                                      sprites[selTileInt].textureRect.height / (float)sprites[selTileInt].texture.height));

                string shortName = sprites[selTileInt].name;
                if (shortName.Length > 35)
                    shortName = shortName.Substring(0, 35) + "…";
                GUILayout.Label(shortName);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                newSelectionObj = EditorGUILayout.ObjectField(newSelectionObj, typeof(GameObject), false);
                if (newSelectionObj != null)
                {
                    newSelectionGameObject = newSelectionObj as GameObject;
                    selectedTD = newSelectionGameObject.GetComponent<TileData>();
                }
                if (selectedTD == null)
                {
                    newSelectionObj = null;
                    newSelectionGameObject = null;
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();

                

                if (selectedTD != null) { GUI.enabled = isGridSelected; } else { GUI.enabled = false; }
                if (GUILayout.Button("+", GUILayout.Height(32f)))
                {
                    if (selectedTD != null)
                    {
                        if (selectedGridCreator.prefabDict.ContainsKey(sprites[selTileInt]))
                            selectedGridCreator.prefabDict[sprites[selTileInt]] = newSelectionGameObject;
                        else
                            selectedGridCreator.prefabDict.Add(sprites[selTileInt], newSelectionGameObject);
                    }                    
                }

                GUILayout.EndHorizontal();



            }
        }
    }
}
