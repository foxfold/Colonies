using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Arms { TestArms, TestArms2, TestArms3 }
public enum Core { TestCore, TestCore2, TestCore3 }
public enum Frame { TestFrame, TestFrame2, TestFrame3 }
public enum Head { TestHead, TestHead2, TestHead3 }
public enum Legs { TestLegs, TestLegs2, TestLegs3 }
public enum Torso { TestTorso, TestTorso2, TestTorso3 }
public enum Weapon { TestWeapon, TestWeapon2, TestWeapon3 }


public class GolemEnums : MonoBehaviour {

    private Dictionary<Arms, GolemStruct> ArmsDict;
//    private Dictionary<Arms, Sprite> ArmsSprite = new Dictionary<Arms, Sprite>();

    private Dictionary<Core, GolemStruct> CoreDict;
//    private Dictionary<Core, Sprite> CoreSprite = new Dictionary<Core, Sprite>();

    private Dictionary<Frame, GolemStruct> FrameDict;
//    private Dictionary<Frame, Sprite> FrameSprite = new Dictionary<Frame, Sprite>();

    private Dictionary<Head, GolemStruct> HeadDict;
//    private Dictionary<Head, Sprite> HeadSprite = new Dictionary<Head, Sprite>();

    private Dictionary<Legs, GolemStruct> LegsDict;
//    private Dictionary<Legs, Sprite> LegsSprite = new Dictionary<Legs, Sprite>();

    private Dictionary<Torso, GolemStruct> TorsoDict;
//    private Dictionary<Torso, Sprite> TorsoSprite = new Dictionary<Torso, Sprite>();

    private Dictionary<Weapon, GolemStruct> WeaponDict;
//    private Dictionary<Weapon, Sprite> WeaponSprite = new Dictionary<Weapon, Sprite>(); 

    private void OnEnable()
    {
        ArmsDict = new Dictionary<Arms, GolemStruct>();
        CoreDict = new Dictionary<Core, GolemStruct>();
        FrameDict = new Dictionary<Frame, GolemStruct>();
        HeadDict = new Dictionary<Head, GolemStruct>();
        LegsDict = new Dictionary<Legs, GolemStruct>();
        TorsoDict = new Dictionary<Torso, GolemStruct>();
        WeaponDict = new Dictionary<Weapon, GolemStruct>();


        ArmsDict.Add(Arms.TestArms, new GolemStruct(15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 0, 10, 0));
        ArmsDict.Add(Arms.TestArms2, new GolemStruct(15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 15, 0));
        ArmsDict.Add(Arms.TestArms3, new GolemStruct(15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 5, 0));

        CoreDict.Add(Core.TestCore, new GolemStruct(20, 0, 0, 0, 0, 0, 0, 10, 10, 0, 10, 0, 0, 100, 20, 0));
        CoreDict.Add(Core.TestCore2, new GolemStruct(20, 0, 0, 0, 0, 0, 0, 10, 10, 0, 10, 0, 0, 120, 30, 0));
        CoreDict.Add(Core.TestCore3, new GolemStruct(20, 0, 0, 0, 0, 0, 0, 10, 10, 0, 10, 0, 0, 80, 10, 0));

        FrameDict.Add(Frame.TestFrame, new GolemStruct(50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 0, 100));
        FrameDict.Add(Frame.TestFrame2, new GolemStruct(50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 110));
        FrameDict.Add(Frame.TestFrame3, new GolemStruct(50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 90));

        HeadDict.Add(Head.TestHead, new GolemStruct(5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 5, 0));
        HeadDict.Add(Head.TestHead2, new GolemStruct(10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 10, 0));
        HeadDict.Add(Head.TestHead3, new GolemStruct(15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 15, 0));

        LegsDict.Add(Legs.TestLegs, new GolemStruct(15, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 7, 15, 0, 10, 0));
        LegsDict.Add(Legs.TestLegs2, new GolemStruct(20, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 5, 20, 0, 10, 0));
        LegsDict.Add(Legs.TestLegs3, new GolemStruct(30, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 3, 30, 0, 10, 0));

        TorsoDict.Add(Torso.TestTorso, new GolemStruct(30, 10, 0, 0, 0, 0, 0, 10, 10, 0, 0, 0, 0, 0, 25, 0));
        TorsoDict.Add(Torso.TestTorso2, new GolemStruct(25, 10, 0, 0, 0, 0, 0, 10, 10, 0, 0, 0, 0, 0, 20, 0));
        TorsoDict.Add(Torso.TestTorso3, new GolemStruct(20, 10, 0, 0, 0, 0, 0, 10, 10, 0, 0, 0, 0, 0, 10, 0));

        WeaponDict.Add(Weapon.TestWeapon, new GolemStruct(0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25, 0, 15, 0));
        WeaponDict.Add(Weapon.TestWeapon2, new GolemStruct(0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30, 0, 15, 0));
        WeaponDict.Add(Weapon.TestWeapon3, new GolemStruct(0, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 35, 0, 15, 0));
    }

    public GolemStruct StructGen(Arms Arms, Core Core, Frame Frame, Head Head, Legs Legs, Torso Torso, Weapon Weapon1, Weapon Weapon2)
    {
        return (ArmsDict[Arms] + CoreDict[Core] + FrameDict[Frame] + HeadDict[Head] + LegsDict[Legs] + TorsoDict[Torso] + WeaponDict[Weapon1] + WeaponDict[Weapon2]);
    }

}
