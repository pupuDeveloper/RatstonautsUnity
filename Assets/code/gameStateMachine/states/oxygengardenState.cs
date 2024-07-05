using UnityEngine;
using System;
using UnityEngine.UI;

public class oxygengardenState : State
{
    [SerializeField] private GameStateManager gameStateManager;
    private bool stateIsReady;

    [Header("UI stuff like backgrounds")]
    [SerializeField] private Button toGardenButton;
    [SerializeField] private GameObject oxygenGardenBG1;
    [SerializeField] private GameObject oxygenGardenBG2;
    private bool isMinigameUnlocked;
    private bool minigameLevel;



    public override State RunCurrentState()
    {
        if (!stateIsReady)
        {
            setupState();
        }

        CustomUpdate();

        if (gameStateManager.targetState != this)
        {
            resetState();
            return gameStateManager.targetState;
        }

        return this;
    }

    private void CustomUpdate()
    {

    }

    private void Start()
    {
        resetState();
    }

    public void toGarden()
    {
        toGardenButton.gameObject.SetActive(false);
        oxygenGardenBG1.SetActive(false);
        oxygenGardenBG2.SetActive(true);
    }
    private void resetState()
    {
        toGardenButton.gameObject.SetActive(false);
        oxygenGardenBG1.SetActive(false);
        oxygenGardenBG2.SetActive(false);
        stateIsReady = false;
    }

    public void setupState()
    {
        gameStateManager.targetState = this;
        oxygenGardenBG1.SetActive(true);
        oxygenGardenBG2.SetActive(false);
        toGardenButton.gameObject.SetActive(true);
        stateIsReady = true;
    }
}
