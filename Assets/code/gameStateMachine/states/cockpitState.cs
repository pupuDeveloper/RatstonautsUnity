using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cockpitState : State
{
    public override State RunCurrentState()
    {
        Debug.Log("in cockpit");
        return this;
    }
}
