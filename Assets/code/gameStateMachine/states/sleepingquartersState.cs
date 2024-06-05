using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class sleepingquartersState : State
{
    [SerializeField] private GameStateManager gameStateManager;
    public override State RunCurrentState()
    {
        if (gameStateManager.targetState != this)
        {
            resetState();
            return gameStateManager.targetState;
        }
        Debug.Log("in sleeping quarters");
        return this;
    }
    private void resetState()
    {
        
    }
}
