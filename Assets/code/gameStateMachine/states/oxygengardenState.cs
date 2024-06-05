using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class oxygengardenState : State
{
    [SerializeField] private GameStateManager gameStateManager;
    public override State RunCurrentState()
    {
        if (gameStateManager.targetState != this)
        {
            resetState();
            return gameStateManager.targetState;
        }
        Debug.Log("in oxygen garden");
        return this;
    }
    private void resetState()
    {
        
    }
}
