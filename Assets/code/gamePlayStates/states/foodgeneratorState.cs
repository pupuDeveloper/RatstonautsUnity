using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class foodgeneratorState : State
{

    //thinking notes:
    // foodgen mechanic could be like this:
    // Choose what food to make out of destroyed asteroid pieces,
    // there are different foods to choose from, all give different kind of boosts/buffs
    // gameplay is just after cooldown of after choosing a food, that food keeps being made and fed to rats
    // gameplay event appears after cooldown is ended (x amount of hours), which is just to clear a jam in the foodgen machine
    // after this is done, it continues making said food (and spaceship continues to fly)
    // "lore" reasoning why it doesn't fly without it, is that when it doesn't work rats are resorted to use icky ratstonaut goo that isn't good
    // and makes them upset so they don't wanna work or something 

    // TODO: come up with a couple of foods
    // TODO: figure out minigame for how to "clear" jammed foodgen machine, similar time/challenge than cockpit minigame
    [SerializeField] private GameStateManager gameStateManager;
    private bool stateIsReady;
    [SerializeField] private GameObject BG1;
    [SerializeField] private GameObject BG2;
    [SerializeField] private GameObject toMiniGameButton;
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

        Debug.Log("in food generator");
        return this;
    }

    private void Start()
    {
        resetState();
    }
    public void toMiniGame()
    {
        toMiniGameButton.gameObject.SetActive(false);
        BG1.SetActive(false);
        BG2.SetActive(true);
    }
    private void resetState()
    {
        BG1.SetActive(false);
        BG2.SetActive(false);
        toMiniGameButton.gameObject.SetActive(false);
        stateIsReady = false;
    }
    public void setupState()
    {
        gameStateManager.targetState = this;
        BG1.SetActive(true);
        BG2.SetActive(false);
        toMiniGameButton.gameObject.SetActive(true);
        stateIsReady = true;
    }
}
