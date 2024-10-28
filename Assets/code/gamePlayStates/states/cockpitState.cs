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
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject minigameUI;
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
        if (!GameManager.Instance.cockpitBoostOn && minigameUI.gameObject.activeSelf)
        {
            _cockpitMinigame.runMiniGame();
        }
        else if (GameManager.Instance.cockpitBoostOn)
        {
            //if boost is on and minigame not available
        }
    }
    private void Start()
    {
        resetState();
    }
    public void toMiniGame()
    {
        AudioManager.instance.Play("UI1");
        toMiniGameButton.gameObject.SetActive(false);
        mainUI.SetActive(false);
        minigameUI.SetActive(true);
    }
    private void resetState()
    {
        _cockpitMinigame.resetMinigamescript();
        minigameUI.SetActive(false);
        stateIsReady = false;
    }
    public void setupState()
    {
        _cockpitMinigame.resetMinigamescript();
        gameStateManager.targetState = this;
        mainUI.SetActive(true);
        minigameUI.SetActive(false);
        toMiniGameButton.gameObject.SetActive(true);
        stateIsReady = true;
    }
}
