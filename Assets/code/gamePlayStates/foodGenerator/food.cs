using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food
{
    public string name {get; private set;}
    public string effectDescription {get; private set;}
    public bool isUnlocked {get; private set;}
    public bool isActivated {get; private set;}

    public food(string name, string effectDescription, bool isUnlocked, bool isActivated)
    {
        this.name = name;
        this.effectDescription = effectDescription;
        this.isUnlocked = isUnlocked;
        this.isActivated = isActivated;
    }
}
