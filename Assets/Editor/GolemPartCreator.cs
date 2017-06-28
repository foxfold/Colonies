using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class GolemPartCreator : EditorWindow
{
    public string Type = "Type";
    public string Name = "Name";
    public string Health = "0";
    public string Melee = "0";
    public string Dexterity = "0";
    public string Ranged = "0";
    public string Aim = "0";
    public string Mundane = "0"; 
    public string MundaneDefense = "0";
    public string Magical = "0";
    public string MagicalDefense = "0";
    public string Speed = "0";
    public string Luck = "0";
    public string Movement = "0";
    public string Power = "0";
    public string MaxPower = "0";
    public string Weight = "0";
    public string MaxWeight = "0";
    public string StringtoPrint;

    [MenuItem("Window/Foxfold/Golem Part Creator")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(GolemPartCreator));
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 120, 30), "Type");
        Type = GUI.TextField(new Rect(120, 10, 120, 15), Type);
        GUI.Label(new Rect(10, 30, 120, 30), "Health");
        Health = GUI.TextField(new Rect(120, 30, 120, 15), Health);
        GUI.Label(new Rect(10, 50, 120, 30), "Melee");
        Melee = GUI.TextField(new Rect(120, 50, 120, 15), Melee);
        GUI.Label(new Rect(10, 70, 120, 30), "Dexterity");
        Dexterity = GUI.TextField(new Rect(120, 70, 120, 15), Dexterity);
        GUI.Label(new Rect(10, 90, 120, 30), "Ranged");
        Ranged = GUI.TextField(new Rect(120, 90, 120, 15), Ranged);
        GUI.Label(new Rect(10, 110, 120, 30), "Aim");
        Aim = GUI.TextField(new Rect(120, 110, 120, 15), Aim);
        GUI.Label(new Rect(10, 130, 120, 30), "Mundane");
        Mundane = GUI.TextField(new Rect(120, 130, 120, 15), Mundane);
        GUI.Label(new Rect(10, 150, 120, 30), "Mundane Defense");
        MundaneDefense = GUI.TextField(new Rect(120, 150, 120, 15), MundaneDefense);
        GUI.Label(new Rect(10, 170, 120, 30), "Magical");
        Magical = GUI.TextField(new Rect(120, 170, 120, 15), Magical);
        GUI.Label(new Rect(10, 190, 120, 30), "Magical Defense");
        MagicalDefense = GUI.TextField(new Rect(120, 190, 120, 15), MagicalDefense);
        GUI.Label(new Rect(10, 210, 120, 30), "Speed");
        Speed = GUI.TextField(new Rect(120, 210, 120, 15), Speed);
        GUI.Label(new Rect(10, 230, 120, 30), "Luck");
        Luck = GUI.TextField(new Rect(120, 230, 120, 15), Luck);
        GUI.Label(new Rect(10, 250, 120, 30), "Movement");
        Movement = GUI.TextField(new Rect(120, 250, 120, 15), Movement);
        GUI.Label(new Rect(10, 270, 120, 30), "Power");
        Power = GUI.TextField(new Rect(120, 270, 120, 15), Power);
        GUI.Label(new Rect(10, 290, 120, 30), "Max Power");
        MaxPower = GUI.TextField(new Rect(120, 290, 120, 15), MaxPower);
        GUI.Label(new Rect(10, 310, 120, 30), "Weight");
        Weight = GUI.TextField(new Rect(120, 310, 120, 15), Weight);
        GUI.Label(new Rect(10, 330, 120, 30), "Max Weight");
        MaxWeight = GUI.TextField(new Rect(120, 330, 120, 15), MaxWeight);
        GUI.Label(new Rect(10, 350, 120, 30), "Part Name");
        Name = GUI.TextField(new Rect(120, 350, 120, 15), Name);

        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Print"))
        {
            StringtoPrint = (Type + "Dict.Add(" + Type + "." + Name + ", new GolemStruct(" 
+ Health + ", "
+ Melee + ", "
+ Dexterity + ", "
+ Ranged + ", "
+ Aim + ", "
+ Mundane + ", "
+ MundaneDefense + ", "
+ Magical + ", "
+ MagicalDefense + ", "
+ Speed + ", "
+ Luck + ", "
+ Movement + ", "
+ Power + ", "
+ MaxPower + ", "
+ Weight + ", "
+ MaxWeight + "));");

            Debug.Log(StringtoPrint);
        }
    }

}