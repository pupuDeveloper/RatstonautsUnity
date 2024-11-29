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
    [SerializeField] private GameObject toMiniGameButton;
    [SerializeField] private GameObject turretBG;
    public GameObject minigameUI;
    public GameObject minigameBG, minigameFrame, backButton;
    [SerializeField] private turretsMiniGame _turretsMinigame;
    [SerializeField] private GameObject allRoomUI;
    [SerializeField] private SpriteRenderer roomBG;
    private Animator bgAnimator;
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
        backButton.SetActive(false);
        stateIsReady = false;
        allRoomUI.SetActive(false);
    }
    public void SetupState()
    {
        _turretsMinigame.resetMiniGame();
        roomBG.sortingOrder = 5;
        allRoomUI.SetActive(true);
        gameStateManager.targetState = this;
        turretBG.SetActive(true);
        minigameUI.SetActive(false);
        minigameBG.SetActive(false);
        minigameFrame.SetActive(false);
        backButton.SetActive(false);
        toMiniGameButton.SetActive(true);
        checkBGanimation();
        stateIsReady = true;
    }
    
    public void toMiniGame()
    {
        AudioManager.instance.Play("UI1");
        toMiniGameButton.SetActive(false);
        turretBG.SetActive(false);
        minigameBG.SetActive(true);
        minigameUI.SetActive(true);
        backButton.SetActive(true);
        minigameFrame.SetActive(true);
        _turretsMinigame.checkForAsteroids();
    }
    public void checkBGanimation()
    {
        bgAnimator = turretBG.GetComponent<Animator>();
        if (GameManager.Instance.turretsBoostOn)
        {
            bgAnimator.SetBool("minigameTriggered", false);
        }
        else
        {
            bgAnimator.SetBool("minigameTriggered", true);
        }
        
    }
}
