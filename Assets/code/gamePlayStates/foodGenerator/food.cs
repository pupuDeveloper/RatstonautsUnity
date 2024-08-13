using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food
{
    public string name {get; private set;}
    public int foodId {get; private set;} //this is for saving system, instead of saving the object, we save the id corresponding to the object.
    public string effectDescription {get; private set;}
    public bool isUnlocked {get; private set;}
    public bool isActivated {get; private set;}

    public food(string name,int foodId, string effectDescription, bool isUnlocked, bool isActivated)
    {
        this.name = name;
        this.foodId = foodId;
        this.effectDescription = effectDescription;
        this.isUnlocked = isUnlocked;
        this.isActivated = isActivated;
    }
}
