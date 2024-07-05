using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class gardenManager : MonoBehaviour
{
    [SerializeField] Transform[] plantSpots;
    private Plant blank;
    private List<Plant> allPlants = new List<Plant>();
    public List<Plant> unlockedPlants {get; private set;}
    [Range (0, 3)] private int unlockedSlots; 



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

        if (!unlockedPlants.Contains(Dandelion)) unlockedPlants.Add(Dandelion);
    }

    
}
