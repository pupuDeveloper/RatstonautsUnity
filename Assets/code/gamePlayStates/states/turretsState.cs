using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class turretsState : State
{
    [SerializeField] private GameStateManager gameStateManager;
    private bool stateIsReady;

    [Header("UI stuff like backgrounds")]
    [SerializeField] private Button toMiniGameButton;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject minigameUI;
    [SerializeField] private turretsMiniGame _turretsMinigame;
    [SerializeField] private GameObject allRoomUI;
    [SerializeField] private SpriteRenderer roomBG;
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
        return this;
    }

    private void resetState()
    {
        _turretsMinigame.resetMiniGame();
        roomBG.sortingOrder = 0;
        minigameUI.SetActive(false);
        stateIsReady = false;
        allRoomUI.SetActive(false);
    }
    public void SetupState()
    {
        _turretsMinigame.resetMiniGame();
        roomBG.sortingOrder = 5;
        allRoomUI.SetActive(true);
        gameStateManager.targetState = this;
        mainUI.SetActive(true);
        minigameUI.SetActive(false);
        toMiniGameButton.gameObject.SetActive(true);
        stateIsReady = true;
    }
    
    public void toMiniGame()
    {
        AudioManager.instance.Play("UI1");
        toMiniGameButton.gameObject.SetActive(false);
        mainUI.SetActive(false);
        minigameUI.SetActive(true);
        _turretsMinigame.checkForAsteroids();
    }
}
