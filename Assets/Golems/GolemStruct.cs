using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GolemStruct
{
    public int Health, Melee, Dexterity, Ranged,
        Aim, Mundane, MundaneDefense, Magical,
        MagicalDefense, Speed, Luck, Movement,
        Power, MaxPower, Weight, MaxWeight;

    // Constructor:
    public GolemStruct(int Health, int Melee, int Dexterity, int Ranged, int Aim, int Mundane, int MundaneDefense, int Magical, int MagicalDefense, int Speed, int Luck, int Movement, int Power, int MaxPower, int Weight, int MaxWeight)
    {
        this.Health = Health;
        this.Melee = Melee;
        this.Dexterity = Dexterity;
        this.Ranged = Ranged;
        this.Aim = Aim;
        this.Mundane = Mundane;
        this.MundaneDefense = MundaneDefense;
        this.Magical = Magical;
        this.MagicalDefense = MagicalDefense;
        this.Speed = Speed;
        this.Luck = Luck;
        this.Movement = Movement;
        this.Power = Power;
        this.MaxPower = MaxPower;
        this.Weight = Weight; 
        this.MaxWeight = MaxWeight;
    }

    //Handles Golemstruct Addition
    public static GolemStruct operator +(GolemStruct a, GolemStruct b)
    {
        return new GolemStruct(a.Health + b.Health, a.Melee + b.Melee, a.Dexterity + b.Dexterity, 
            a.Ranged + b.Ranged, a.Aim + b.Aim, a.Mundane + b.Mundane, 
            a.MundaneDefense + b.MundaneDefense, a.Magical + b.Magical, 
            a.MagicalDefense + b.MagicalDefense, a.Speed + b.Speed, a.Luck + b.Luck, 
            a.Movement + b.Movement, a.Power + b.Power, a.MaxPower + b.MaxPower, 
            a.Weight + b.Weight, a.MaxWeight + b.MaxWeight);
    }

    //Handles Golemstruct Subtration
    public static GolemStruct operator -(GolemStruct a, GolemStruct b)
    {
        return new GolemStruct(a.Health - b.Health, a.Melee - b.Melee, a.Dexterity - b.Dexterity,
            a.Ranged - b.Ranged, a.Aim - b.Aim, a.Mundane - b.Mundane,
            a.MundaneDefense - b.MundaneDefense, a.Magical - b.Magical,
            a.MagicalDefense - b.MagicalDefense, a.Speed - b.Speed, a.Luck - b.Luck,
            a.Movement - b.Movement, a.Power - b.Power, a.MaxPower - b.MaxPower,
            a.Weight - b.Weight, a.MaxWeight - b.MaxWeight);
    }

}
