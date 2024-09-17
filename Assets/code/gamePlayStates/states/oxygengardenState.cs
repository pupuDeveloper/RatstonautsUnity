using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class oxygengardenState : State
{
    [SerializeField] private GameStateManager gameStateManager;
    private bool stateIsReady;

    [Header("UI stuff like backgrounds")]
    [SerializeField] private Button toGardenButton;
    [SerializeField] private GameObject oxygenGardenBG1;
    [SerializeField] private GameObject oxygenGardenBG2;
    private bool isMinigameUnlocked;
    [SerializeField] private gardenManager _gardenManager;
    [SerializeField] private wateringEvent _wateringEvent;


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
    private void Start()
    {
        resetState();
    }

    void CustomUpdate()
    {
        checkSpotsForDryPlants();
    }

    public void toGarden()
    {
        toGardenButton.gameObject.SetActive(false);
        oxygenGardenBG1.SetActive(false);
        oxygenGardenBG2.SetActive(true);
    }
    private void resetState()
    {
        oxygenGardenBG2.SetActive(false);
        _gardenManager.scrollableList.SetActive(false);
        _gardenManager.closePlantListButton.SetActive(false);
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
    public List<Plant> getPlantsInSpots()
    {
        return _gardenManager.plantsInSpots;
    }
    public void checkSpotsForDryPlants()
    {
        if (GameManager.Instance.gardenBoostOn)
        {
            return;
        }
        foreach (Plant plant in getPlantsInSpots())
        {
            if (plant != _gardenManager.blank)
            {
                _wateringEvent.gardenEvent();
            }
        }
    }
    public bool arePlantsWatered()
    {
        return _gardenManager.arePlantsWatered;
    }
    public bool areAllPlantsBlank()
    {
        List<Plant> plants = getPlantsInSpots();
        for (int i = 0; i < plants.Count; i++)
        {
            if (plants[i].plantId != 0)
            {
                return false;
            }
        }
        return true;
    }
    public bool doPlantsAffectRoom(string roomName)
    //checks if plants currently active affect given room. Bunch of loops and switch cases
    //goes through each spot, then each relevant plant id

    {
        List<Plant> plants = getPlantsInSpots();
        switch (roomName)
        {
            case "cockpit":
            for (int i = 0; i < plants.Count; i++)
            {
                if (plants[i].plantId == 1 || plants[i].plantId == 5 || plants[i].plantId == 6 || plants[i].plantId == 11)
                return true;
            }
            return false;
            break;
            case "turrets":
            for (int i = 0; i < plants.Count; i++)
            {
                if (plants[i].plantId == 3 || plants[i].plantId == 5 || plants[i].plantId == 6 || plants[i].plantId == 11)
                return true;
            }
            return false;
            break;
            //TODO: add rest of rooms
        }
        Debug.LogError("ERROR!!! SHOULDNT GET HERE!");
        return false;
    }
}
