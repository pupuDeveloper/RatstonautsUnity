using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class oxygengardenState : State
{
    [SerializeField] private GameStateManager gameStateManager;
    private bool stateIsReady;

    [Header("UI stuff like backgrounds")]
    [SerializeField] private Button toGardenButton;
    [SerializeField] private gardenManager _gardenManager;
    [SerializeField] private wateringEvent _wateringEvent;
    [SerializeField] private GameObject allRoomUI;
    [SerializeField] private GameObject SpotItems;
    [SerializeField] private GameObject PlantListItems;
    [SerializeField] private GameObject StaticProps;
    [SerializeField] private SpriteRenderer roomBG;
    private IEnumerator showAnchoredProps;

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

    private void resetState()
    {
        roomBG.sortingOrder = 0;
        _gardenManager.scrollableList.SetActive(false);
        _gardenManager.closePlantListButton.SetActive(false);
        stateIsReady = false;
        SpotItems.SetActive(false);
        PlantListItems.SetActive(false);
        StaticProps.GetComponent<anchorChildObjects>().anchorObjects(false);
    }

    public void setupState()
    {
        _gardenManager.instantiatePlants();
        roomBG.sortingOrder = 5;
        SpotItems.SetActive(true);
        PlantListItems.SetActive(true);
        StaticProps.GetComponent<anchorChildObjects>().anchorObjects(true);
        gameStateManager.targetState = this;
        stateIsReady = true;
    }
    public Plant[] getPlantsInSpots()
    {
        if (_gardenManager.plantsInSpots == null || _gardenManager.plantsInSpots.Length == 0 || _gardenManager.plantsInSpots[0] == null)
        {
            _gardenManager.initialize();
        }
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
                _wateringEvent.gardenEvent(false, i);
                i++;
            }
            else
            {
                _wateringEvent.gardenEvent(true, i);
                i++;
            }
        }
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
                if (plants[i].plantId == 1 || plants[i].plantId == 5 || plants[i].plantId == 6 || plants[i].plantId == 11
                || plants[i].plantId == 8)
                return true;
            }
            break;
            case "turrets":
            for (int i = 0; i < plants.Length; i++)
            {
                if (plants[i].plantId == 2 || plants[i].plantId == 5 || plants[i].plantId == 6 || plants[i].plantId == 11
                || plants[i].plantId == 8)
                return true;
            }
            break;
        }
        return false;
    }
}
