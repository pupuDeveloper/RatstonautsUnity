using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant
{
    public string name {get; private set;}
    public string effectDescription {get; private set;}
    public bool isUnlocked {get; private set;}

    public Plant(string name, string effectDescription, bool isUnlocked)
    {
        this.name = name;
        this.effectDescription = effectDescription;
        this.isUnlocked = isUnlocked;
    }
}
