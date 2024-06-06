using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class cockpitState : State
{
    [SerializeField] private GameStateManager gameStateManager;
    private bool stateIsReady;

    //visual stuff like background etc to of the minigame

    [Header("UI stuff like backgrounds")]
    [SerializeField] private Button toMiniGameButton;
    [SerializeField] private GameObject cockPitBackground;
    [SerializeField] private GameObject cockPitMiniGameBG;

    public override State RunCurrentState()
    {
        if (!stateIsReady)
        {
            setupState();
        }
        if (gameStateManager.targetState != this)
        {
            resetState();
            return gameStateManager.targetState;
        }
        return this;
    }
    private void Start()
    {
        setupState();
    }
    public void toMiniGame()
    {
        toMiniGameButton.gameObject.SetActive(false);
        cockPitBackground.SetActive(false);
        cockPitMiniGameBG.SetActive(true);
    }
    private void resetState()
    {
        cockPitBackground.SetActive(false);
        cockPitMiniGameBG.SetActive(false);
        toMiniGameButton.gameObject.SetActive(false);
        stateIsReady = false;
    }
    public void setupState()
    {
        gameStateManager.targetState = this;
        cockPitBackground.SetActive(true);
        cockPitMiniGameBG.SetActive(false);
        toMiniGameButton.gameObject.SetActive(true);
        stateIsReady = true;
    }
}
