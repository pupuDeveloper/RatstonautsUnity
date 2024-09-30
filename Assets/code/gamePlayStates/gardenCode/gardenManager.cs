using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.EventSystems;
using TMPro;

public class gardenManager : MonoBehaviour
{
    [SerializeField] private Transform[] plantSpots;
    public List<Plant> plantsInSpots = new List<Plant>();
    public Plant blank;
    public List<Plant> allPlants = new List<Plant>();
    public List<Plant> unlockedPlants { get; private set; }
    [SerializeField] private GameObject[] plantsUI;
    [SerializeField] [Range(0, 3)] private int unlockedSlots;
    public GameObject scrollableList;
    public GameObject closePlantListButton;
    [SerializeField] private GameObject[] removeButtons;
    public bool arePlantsWatered;
    private xpManager _xpManager;

    private void Start()
    {
        unlockedPlants = new List<Plant>();
        //TODO: read these plants from file, for now creating them in start
        Plant BonsaiTree = new Plant("Bonsai tree", 11, "Boost all minigame effectiveness by x%", false, false, 92);
        if (!allPlants.Contains(BonsaiTree)) allPlants.Add(BonsaiTree);

        Plant Dandelion = new Plant("Dandelion", 1, "Boost autopilot effectiveness by 5%", true, false, 0);
        if (!allPlants.Contains(Dandelion)) allPlants.Add(Dandelion);

        Plant Clover = new Plant("Clover", 2, "Boost food generator by 5%", false, false, 100);
        if (!allPlants.Contains(Clover)) allPlants.Add(Clover);

        Plant Violet = new Plant("Violet", 3, "Boost turrets by 5%", false, false, 5);
        if (!allPlants.Contains(Violet)) allPlants.Add(Violet);

        Plant SeaMayweed = new Plant("Sea Mayweed", 4, "Boost well slept buff by 5%", false, false, 100);
        if (!allPlants.Contains(SeaMayweed)) allPlants.Add(SeaMayweed);

        Plant Nettle = new Plant("Nettle", 5, "decrease minigame cooldown by 50%, increase all minigame effectiveness by 10%", false, false, 20);
        if (!allPlants.Contains(Nettle)) allPlants.Add(Nettle);

        Plant ArcticStarflower = new Plant("Arctic Starflower", 6, "increase all minigame cooldown by 50%, decrease effectiveness by 5%", false, false, 30);
        if (!allPlants.Contains(ArcticStarflower)) allPlants.Add(ArcticStarflower);

        Plant SolomonsSeal = new Plant("Solomon's Seal", 7, "rats need 1-2 hours less sleep per night", false, false, 100);
        if (!allPlants.Contains(SolomonsSeal)) allPlants.Add(SolomonsSeal);

        Plant GoldenRod = new Plant("Goldenrod", 8, "Increase XP received after succesfull minigame by 10%", false, false, 60);
        if (!allPlants.Contains(GoldenRod)) allPlants.Add(GoldenRod);

        Plant Harebell = new Plant("Harebell", 9, "think of a new effect", false, false, 100);
        if (!allPlants.Contains(Harebell)) allPlants.Add(Harebell);

        Plant Daffodil = new Plant("Daffodil", 10, "Disable one room of spaceship, other rooms gain x% efficiency and xp boost", false, false, 85);
        if (!allPlants.Contains(Daffodil)) allPlants.Add(Daffodil);


        //lvl 1 unlocks
        if (!unlockedPlants.Contains(Dandelion)) unlockedPlants.Add(Dandelion);
        unlockedSlots = 1;
        //

        //blank plant
        blank = new Plant("Blank", 0, "Blank plant", false, false, 0);
        if (!allPlants.Contains(blank)) allPlants.Add(blank);
        //current plants in spots TODO: read this from file later
        if (GameManager.Instance.plantsInSpots.Length != 0)
        {
            for (int i = 0; i < GameManager.Instance.plantsInSpots.Length; i++)
            {
                Plant myplant = allPlants.Find(x => x.plantId == GameManager.Instance.plantsInSpots[i]);
                Debug.Log(myplant);
                plantsInSpots[i] = myplant;
            }
        }
        else
        {
            GameManager.Instance.plantsInSpots = new int[3];
            for (int i = 0; i < 3; i++)
            {
                plantsInSpots.Add(blank);
                GameManager.Instance.plantsInSpots[i] = 0;
            }
            Debug.Log(plantsInSpots);
        }


        foreach (Plant p in allPlants)
        {
            foreach (GameObject g in plantsUI)
            {
                string name = p.name.ToLower();
                name = name.Trim();
                string name2 = g.name.ToLower();
                name2 = name2.Trim();
                if (name == name2)
                {
                    if (p.isUnlocked == false)
                        g.transform.GetChild(3).gameObject.SetActive(true);
                    else
                        g.transform.GetChild(3).gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("Plant name did not match gameobject!");
                }
            }
        }

        switch (unlockedSlots)
        {
            case 1:
                plantSpots[0].GetChild(2).gameObject.SetActive(false);
                break;
            case 2:
                plantSpots[0].GetChild(2).gameObject.SetActive(false);
                plantSpots[1].GetChild(2).gameObject.SetActive(false);
                break;
            case 3:
                plantSpots[0].GetChild(2).gameObject.SetActive(false);
                plantSpots[1].GetChild(2).gameObject.SetActive(false);
                plantSpots[2].GetChild(2).gameObject.SetActive(false);
                break;
        }

        _xpManager = GameObject.Find("xpmanager").GetComponent<xpManager>();
        whatPlantsAreUnlocked();

        arePlantsWatered = GameManager.Instance.gardenBoostOn;
    }
    private void whatPlantsAreUnlocked()
    {
        foreach (Plant p in allPlants)
        {
            if (p.unlockedAtThisLevel >= _xpManager.oxygenGardenLvl)
            {
                p.isUnlocked = true;
                if (!unlockedPlants.Contains(p)) unlockedPlants.Add(p);
            }
        }
    }
    public void addPlant(Plant plant)
    {
        for (int i = 0; i < unlockedSlots; i++)
        {
            if (plantsInSpots[i] == plant)
            {
                Debug.LogWarning("plant already in one of the slots!");
                return;
            }
            if (plantsInSpots[i] == blank)
            {
                plantsInSpots[i] = plant;
                removeButtons[i].SetActive(true);
                plantSpots[i].GetChild(1).transform.GetComponent<TextMeshProUGUI>().text = plant.name;
                updateGameManagerList();
                break;
            }
            if (i == 2)
            {
                Debug.LogWarning("All spots are taken! Remove a plant before adding a new one");
            }
        }
    }
    public void removePlant(Plant plant)
    {
        for (int i = 0; i < unlockedSlots; i++)
        {
            if (plantsInSpots[i] == plant)
            {
                plantsInSpots[i] = blank;
                removeButtons[i].SetActive(false);
                plantSpots[i].GetChild(1).transform.GetComponent<TextMeshProUGUI>().text = "";
                updateGameManagerList();
                break;
            }
            if (i == 3)
            {
                Debug.LogWarning("Plant not found in spots! THIS IS AN ERROR!");
            }
        }
    }
    public void removeButton()
    {
        Plant plantToBeRemoved = blank;
        switch (EventSystem.current.currentSelectedGameObject.transform.parent.name)
        {
            case "spot1":
                plantToBeRemoved = plantsInSpots[0];
                break;
            case "spot2":
                plantToBeRemoved = plantsInSpots[1];
                break;
            case "spot3":
                plantToBeRemoved = plantsInSpots[2];
                break;
            default:
                Debug.LogWarning("plant was not found. THIS IS AN ERROR");
                break;
        }
        removePlant(plantToBeRemoved);

    }
    public void addButton()
    {
        Plant plantToBeAdded = blank;

        foreach (Plant p in allPlants)
        {
            string plantName = p.name.ToLower();
            plantName = plantName.Trim();
            string selectedPlantName = EventSystem.current.currentSelectedGameObject.transform.parent.name.ToLower();
            selectedPlantName = selectedPlantName.Trim();

            if (selectedPlantName == plantName)
            {
                plantToBeAdded = p;
                addPlant(plantToBeAdded);
                break;
            }
        }
    }
    private void updateGameManagerList()
    {
        int i = 0;
        foreach (Plant p in plantsInSpots)
        {
            GameManager.Instance.plantsInSpots[i] = p.plantId;
            i++;
        }
    }
}
