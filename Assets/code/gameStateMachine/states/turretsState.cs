using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class turretsState : State
{
    public override State RunCurrentState()
    {
        Debug.Log("in turrets");
        return this;
    }
}
