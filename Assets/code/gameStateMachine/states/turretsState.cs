using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class turretsState : State
{
    [SerializeField] private GameStateManager gameStateManager;
    public override State RunCurrentState()
    {
        if (gameStateManager.targetState != this)
        {
            resetState();
            return gameStateManager.targetState;
        }
        Debug.Log("in turrets");
        return this;
    }
    private void resetState()
    {
        
    }
}
