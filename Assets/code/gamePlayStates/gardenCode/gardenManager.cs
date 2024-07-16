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
    private Plant blank;
    public List<Plant> allPlants = new List<Plant>();
    public List<Plant> unlockedPlants { get; private set; }
    [SerializeField] private GameObject[] plantsUI;
    [Range(0, 3)] private int unlockedSlots;
    public GameObject scrollableList;
    public GameObject closePlantListButton;
    [SerializeField] private GameObject[] removeButtons;


    private void Start()
    {
        unlockedPlants = new List<Plant>();
        //TODO: read these plants from file, for now creating them in start
        Plant BonsaiTree = new Plant("Bonsai tree", "Boost all minigame effectiveness by x%", false, false);
        if (!allPlants.Contains(BonsaiTree)) allPlants.Add(BonsaiTree);

        Plant Dandelion = new Plant("Dandelion", "Boost autopilot effectiveness by 5%", true, false);
        if (!allPlants.Contains(Dandelion)) allPlants.Add(Dandelion);

        Plant Clover = new Plant("Clover", "Boost food generator by 5%", false, false);
        if (!allPlants.Contains(Clover)) allPlants.Add(Clover);

        Plant Violet = new Plant("Violet", "Boost turets by 5%", false, false);
        if (!allPlants.Contains(Violet)) allPlants.Add(Violet);

        Plant SeaMayweed = new Plant("Sea Mayweed", "Boost well slept buff by 5%", false, false);
        if (!allPlants.Contains(SeaMayweed)) allPlants.Add(SeaMayweed);

        Plant Nettle = new Plant("Nettle", "decrease minigame cooldown by 50%, increase all minigame effectiveness by 10%", false, false);
        if (!allPlants.Contains(Nettle)) allPlants.Add(Nettle);

        Plant ArcticStarflower = new Plant("Arctic Starflower", "increase all minigame cooldown by 50%, decrease effectiveness by 5%", false, false);
        if (!allPlants.Contains(ArcticStarflower)) allPlants.Add(ArcticStarflower);

        Plant SolomonsSeal = new Plant("Solomon's Seal", "rats need 1-2 hours less sleep per night", false, false);
        if (!allPlants.Contains(SolomonsSeal)) allPlants.Add(SolomonsSeal);

        Plant GoldenRod = new Plant("Goldenrod", "Increase XP received after succesfull minigame by 10%", false, false);
        if (!allPlants.Contains(GoldenRod)) allPlants.Add(GoldenRod);

        Plant Harebell = new Plant("Harebell", "Boost rats happiness boost from eating by x%", false, false);
        if (!allPlants.Contains(Harebell)) allPlants.Add(Harebell);

        Plant Daffodil = new Plant("Daffodil", "Disable one room of spaceship, other rooms gain x% efficiency and xp boost", false, false);
        if (!allPlants.Contains(Daffodil)) allPlants.Add(Daffodil);


        //lvl 1 unlocks
        if (!unlockedPlants.Contains(Dandelion)) unlockedPlants.Add(Dandelion);
        unlockedSlots = 1;
        //

        //blank plant
        blank = new Plant("Blank", "Blank plant", false, false);

        //current plants in spots TODO: read this from file later
        plantsInSpots.AddRange(Enumerable.Repeat(blank, 3));


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
                        g.transform.GetChild(1).gameObject.SetActive(true);
                    else
                        g.transform.GetChild(1).gameObject.SetActive(false);
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
        switch(EventSystem.current.currentSelectedGameObject.transform.parent.name)
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
}
