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
        AudioManager.instance.Play("UI1");
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
        _gardenManager.instantiatePlants();
        gameStateManager.targetState = this;
        oxygenGardenBG1.SetActive(true);
        oxygenGardenBG2.SetActive(false);
        toGardenButton.gameObject.SetActive(true);
        stateIsReady = true;
    }
    public Plant[] getPlantsInSpots()
    {
        return _gardenManager.plantsInSpots;
    }
    public void checkSpotsForDryPlants()
    {
        if (GameManager.Instance.gardenBoostOn)
        {
            return;
        }
        int i = 0;
        foreach (Plant plant in getPlantsInSpots())
        {
            if (plant.name == "Blank")
            {
                i++;
            }
            else
            {
                _wateringEvent.gardenEvent(true);
            }
        }
        if (i == 3) _wateringEvent.gardenEvent(false);
    }
    public bool areAllPlantsBlank()
    {
        Plant[] plants = getPlantsInSpots();
        for (int i = 0; i < plants.Length; i++)
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
        Plant[] plants = getPlantsInSpots();
        switch (roomName)
        {
            case "cockpit":
            for (int i = 0; i < plants.Length; i++)
            {
                if (plants[i].plantId == 1 || plants[i].plantId == 5 || plants[i].plantId == 6 || plants[i].plantId == 11)
                return true;
            }
            break;
            case "turrets":
            for (int i = 0; i < plants.Length; i++)
            {
                if (plants[i].plantId == 3 || plants[i].plantId == 5 || plants[i].plantId == 6 || plants[i].plantId == 11)
                return true;
            }
            break;
            //TODO: add rest of rooms
        }
        return false;
    }
}
