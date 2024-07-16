using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class foodgeneratorState : State
{
    [SerializeField] private GameStateManager gameStateManager;
    public override State RunCurrentState()
    {
        if (gameStateManager.targetState != this)
        {
            resetState();
            return gameStateManager.targetState;
        }
        Debug.Log("in food generator");
        return this;
    }
    private void resetState()
    {
        
    }
}
