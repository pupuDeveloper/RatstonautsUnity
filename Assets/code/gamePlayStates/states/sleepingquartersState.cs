using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class sleepingquartersState : State
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private GameObject background;
    private bool stateIsReady;
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
        Debug.Log("in sleeping quarters");
        return this;
    }

    private void Start()
    {
        resetState();
    }
    private void resetState()
    {
        stateIsReady = false;
        background.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
    private void setupState()
    {
        gameStateManager.targetState = this;
        background.SetActive(true);
        background.GetComponent<SpriteRenderer>().sortingOrder = 5;
        stateIsReady = true;
    }
}
