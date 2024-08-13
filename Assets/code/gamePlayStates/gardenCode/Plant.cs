using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant
{
    public string name {get; private set;}
    public int plantId {get; private set;} //this is for saving system, instead of saving the object, we save the id corresponding to the object.
    public string effectDescription {get; private set;}
    public bool isUnlocked {get; private set;}
    public bool isActivated {get; private set;}

    public Plant(string name, int plantId, string effectDescription, bool isUnlocked, bool isActivated)
    {
        this.name = name;
        this.plantId = plantId;
        this.effectDescription = effectDescription;
        this.isUnlocked = isUnlocked;
        this.isActivated = isActivated;
    }
}
