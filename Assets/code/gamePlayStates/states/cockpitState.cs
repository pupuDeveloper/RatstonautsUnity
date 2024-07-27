using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class cockpitState : State
{
    [SerializeField] private GameStateManager gameStateManager;
    private bool stateIsReady;
    public bool boost;

    //visual stuff like background etc to of the minigame

    [Header("UI stuff like backgrounds")]
    [SerializeField] private Button toMiniGameButton;
    [SerializeField] private GameObject cockPitBackground;
    [SerializeField] private GameObject cockPitMiniGameBG;
    [SerializeField] private cockpitMiniGame _cockpitMinigame;

    public override State RunCurrentState()
    {
        if (!stateIsReady)
        {
            setupState();
        }
        CustomUpdate();
        if (gameStateManager.targetState != this)
        {
            _cockpitMinigame.resetMinigamescript();
            resetState();
            return gameStateManager.targetState;
        }
        return this;
    }

    private void CustomUpdate()
    {
        boost = _cockpitMinigame.checkBoost();
        if (!boost && cockPitMiniGameBG.gameObject.activeSelf)
        {
            _cockpitMinigame.runMiniGame();
        }
        else if (_cockpitMinigame.checkBoost())
        {
            Debug.Log("not running minigame1");
            //if boost is on and minigame not available
        }
    }
    private void Start()
    {
        resetState();
    }
    public void toMiniGame()
    {
        toMiniGameButton.gameObject.SetActive(false);
        cockPitBackground.SetActive(false);
        cockPitMiniGameBG.SetActive(true);
    }
    private void resetState()
    {
        _cockpitMinigame.resetMinigamescript();
        cockPitBackground.SetActive(false);
        cockPitMiniGameBG.SetActive(false);
        toMiniGameButton.gameObject.SetActive(false);
        stateIsReady = false;
    }
    public void setupState()
    {
        _cockpitMinigame.resetMinigamescript();
        gameStateManager.targetState = this;
        cockPitBackground.SetActive(true);
        cockPitMiniGameBG.SetActive(false);
        toMiniGameButton.gameObject.SetActive(true);
        stateIsReady = true;
    }
}
