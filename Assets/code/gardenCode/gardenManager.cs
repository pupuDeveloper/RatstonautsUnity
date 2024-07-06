using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class gardenManager : MonoBehaviour
{
    [SerializeField] private Transform[] plantSpots;
    private List<Plant> plantsInSpots = new List<Plant>();
    private Plant blank;
    private List<Plant> allPlants = new List<Plant>();
    public List<Plant> unlockedPlants { get; private set; }
    [SerializeField] private GameObject[] plantsUI;
    [Range(0, 3)] private int unlockedSlots;
    public GameObject scrollableList;
    public GameObject closePlantListButton;



    private void Start()
    {
        unlockedPlants = new List<Plant>();
        //TODO: read these plants from file, for now creating them in start
        Plant BonsaiTree = new Plant("Bonsai tree", "Boost all minigame effectiveness by x%", false);
        if (!allPlants.Contains(BonsaiTree)) allPlants.Add(BonsaiTree);
        Plant Dandelion = new Plant("Dandelion", "Boost autopilot effectiveness by 5%", true);
        if (!allPlants.Contains(Dandelion)) allPlants.Add(Dandelion);
        Plant Clover = new Plant("Clover", "Boost food generator by 5%", false);
        if (!allPlants.Contains(Clover)) allPlants.Add(Clover);
        Plant Violet = new Plant("Violet", "Boost turets by 5%", false);
        if (!allPlants.Contains(Violet)) allPlants.Add(Violet);
        Plant SeaMayweed = new Plant("Sea Mayweed", "Boost well slept buff by 5%", false);
        if (!allPlants.Contains(SeaMayweed)) allPlants.Add(SeaMayweed);
        Plant Nettle = new Plant("Nettle", "decrease minigame cooldown by 50%, increase all minigame effectiveness by 10%", false);
        if (!allPlants.Contains(Nettle)) allPlants.Add(Nettle);
        Plant ArcticStarflower = new Plant("Arctic Starflower", "increase all minigame cooldown by 50%, decrease effectiveness by 5%", false);
        if (!allPlants.Contains(ArcticStarflower)) allPlants.Add(ArcticStarflower);
        Plant SolomonsSeal = new Plant("Solomon's Seal", "rats need 1-2 hours less sleep per night", false);
        if (!allPlants.Contains(SolomonsSeal)) allPlants.Add(SolomonsSeal);
        Plant GoldenRod = new Plant("Goldenrod", "Increase XP received after succesfull minigame by 10%", false);
        if (!allPlants.Contains(GoldenRod)) allPlants.Add(GoldenRod);
        Plant Harebell = new Plant("Harebell", "Boost rats happiness boost from eating by x%", false);
        if (!allPlants.Contains(Harebell)) allPlants.Add(Harebell);
        Plant Daffodil = new Plant("Daffodil", "Disable one room of spaceship, other rooms gain x% efficiency and xp boost", false);
        if (!allPlants.Contains(Daffodil)) allPlants.Add(Daffodil);


        //lvl 1 unlocks
        if (!unlockedPlants.Contains(Dandelion)) unlockedPlants.Add(Dandelion);
        unlockedSlots = 1;
        //

        //blank plant
        blank = new Plant("Blank", "Blank plant", false);

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
    }

    public void addPlant(string plantName)
    {

    }
    public void removePlant(string plantName)
    {

    }
}
