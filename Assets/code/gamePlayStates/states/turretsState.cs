using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class turretsState : State
{
    [SerializeField] private GameStateManager gameStateManager;
    private bool stateIsReady;
    public bool cooldownOn; //read from file

    [Header("UI stuff like backgrounds")]
    [SerializeField] private Button toMiniGameButton;
    [SerializeField] private GameObject turretBG;
    [SerializeField] private GameObject turretsMiniGameBG;
    [SerializeField] private turretsMiniGame _turretsMinigame;
    public override State RunCurrentState()
    {
        if (!stateIsReady)
        {
            SetupState();
        }
        if (gameStateManager.targetState != this)
        {
            resetState();
            return gameStateManager.targetState;
        }
        Debug.Log("in turrets");
        return this;
    }
    private void Start()
    {
        cooldownOn = false;
    }

    private void resetState()
    {
        _turretsMinigame.resetMiniGame();
        turretBG.SetActive(false);
        turretsMiniGameBG.SetActive(false);
        toMiniGameButton.gameObject.SetActive(false);
        stateIsReady = false;
    }
    public void SetupState()
    {
        _turretsMinigame.resetMiniGame();
        gameStateManager.targetState = this;
        turretBG.SetActive(true);
        turretsMiniGameBG.SetActive(false);
        toMiniGameButton.gameObject.SetActive(true);
        stateIsReady = true;
    }
    
    public void toMiniGame()
    {
        toMiniGameButton.gameObject.SetActive(false);
        turretBG.SetActive(false);
        turretsMiniGameBG.SetActive(true);
        _turretsMinigame.checkForAsteroids();
    }
}
