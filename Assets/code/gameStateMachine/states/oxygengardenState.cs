using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class oxygengardenState : State
{
    public override State RunCurrentState()
    {
        Debug.Log("in oxygen garden");
        return this;
    }
}
