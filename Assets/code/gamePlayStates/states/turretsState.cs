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
    public GameObject minigameUI;
    public GameObject minigameBG, minigameFrame;
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
        minigameBG.SetActive(false);
        minigameFrame.SetActive(false);
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
        minigameBG.SetActive(false);
        minigameFrame.SetActive(false);
        toMiniGameButton.gameObject.SetActive(true);
        stateIsReady = true;
    }
    
    public void toMiniGame()
    {
        AudioManager.instance.Play("UI1");
        toMiniGameButton.gameObject.SetActive(false);
        mainUI.SetActive(false);
        minigameBG.SetActive(true);
        minigameUI.SetActive(true);
        minigameFrame.SetActive(true);
        _turretsMinigame.checkForAsteroids();
    }
}
