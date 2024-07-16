using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class turretsState : State
{
    [SerializeField] private GameStateManager gameStateManager;

    [Header("UI stuff like backgrounds")]
    [SerializeField] private Button toMiniGameButton;
    [SerializeField] private GameObject turretBG;
    [SerializeField] private GameObject turretsMiniGameBG;
    [SerializeField] private turretsMinigame _turretsMinigame;
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
