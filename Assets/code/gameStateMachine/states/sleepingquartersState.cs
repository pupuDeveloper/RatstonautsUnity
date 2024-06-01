using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class sleepingquartersState : State
{
    public override State RunCurrentState()
    {
        Debug.Log("in sleeping quarters");
        return this;
    }
}
