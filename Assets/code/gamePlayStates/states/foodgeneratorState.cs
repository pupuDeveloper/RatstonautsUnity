using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class foodgeneratorState : State
{

    //thinking notes:
    // Choose what food to make out of destroyed asteroid pieces,
    // there are different foods to choose from, all give different kind of boosts/buffs

    // There are 2 Active events:

    // 1: fill foodgenerator with asteroid pieces. Filling always works for x amount of hours
    // when foodgenerator levels up, choose to upgrade either size or efficiency
    // Efficiency -> foodgenerator is more efficient with given asteroid pieces, uses them faster but gives more XP
    // Size -> can fit more asteroid pieces, so it doesn't empty so fast.

    // 2: Every random hour, food generator's gears get stuck, and they need to be cleared.
    // active gameplay is probably just swiping the rocks from the gears away

    // TODO: come up with a couple of foods
    // TODO: figure out minigame for how to "clear" jammed foodgen machine, similar time/challenge than cockpit minigame
    [SerializeField] private GameStateManager gameStateManager;
    private bool stateIsReady;
    [SerializeField] private GameObject BG1;
    [SerializeField] private GameObject BG2;
    [SerializeField] private GameObject toMiniGameButton;
    [SerializeField] private foodGen _foodGenScript;
    [SerializeField] private GameObject gear1;
    [SerializeField] private GameObject gear2;
    [SerializeField] private GameObject gear3;
    private gearTurning _gearTurning1;
    private gearTurning _gearTurning2;
    private gearTurning _gearTurning3;
    private bool inMinigame;
    public override State RunCurrentState()
    {
        if (!stateIsReady)
        {
            setupState();
        }

        customUpdate();

        if (gameStateManager.targetState != this)
        {
            resetState();
            return gameStateManager.targetState;
        }

        return this;
    }

    private void Start()
    {
        resetState();
        _gearTurning1 = gear1.GetComponent<gearTurning>();
        _gearTurning2 = gear2.GetComponent<gearTurning>();
        _gearTurning3 = gear3.GetComponent<gearTurning>();
        inMinigame = false;
    }
    void customUpdate()
    {
        if (inMinigame)
        {
            if (GameManager.Instance.foodGenBoostOn)
            {
                _gearTurning1.turnMotion();
                _gearTurning2.turnMotion();
                _gearTurning3.turnMotion();
            }
            else
            {
                _gearTurning1.stuckMotion();
                _gearTurning2.stuckMotion();
                _gearTurning3.stuckMotion();
            }
        }
    }
    public void toMiniGame()
    {
        AudioManager.instance.Play("UI1");
        toMiniGameButton.gameObject.SetActive(false);
        BG1.SetActive(false);
        BG2.SetActive(true);
        inMinigame = true;
    }
    private void resetState()
    {
        BG2.SetActive(false);
        _foodGenScript.scrollableList.SetActive(false);
        stateIsReady = false;
        inMinigame = false;
    }
    public void setupState()
    {
        gameStateManager.targetState = this;
        BG1.SetActive(true);
        BG2.SetActive(false);
        toMiniGameButton.gameObject.SetActive(true);
        stateIsReady = true;
        inMinigame = false;
    }
    public food getFoodInSpot()
    {
        return _foodGenScript.foodInSpot;
    }
}
