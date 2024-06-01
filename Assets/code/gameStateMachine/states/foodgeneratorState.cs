using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class foodgeneratorState : State
{
    public override State RunCurrentState()
    {
        Debug.Log("in food generator");
        return this;
    }
}
