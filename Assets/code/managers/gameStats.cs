using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class gameStats : MonoBehaviour
{
    public int spaceShipSpeed; //spaceships speed km/per second
    public int distanceTraveled; // total distance traveled
    [SerializeField] private shipManager _shipManager;
    [SerializeField] private cockpitMiniGame _cockpitMinigame;
    [SerializeField] private cockpitState _cockPitState;
    [SerializeField] private oxygengardenState _oxygenGardenState;
    [SerializeField] private foodgeneratorState _foodgeneratorState;
    [SerializeField] private turretsMiniGame _turretMiniGame;
    //
    [SerializeField] private wateringEvent _wateringEvent;
    private bool speedBoost; //when all available minigames are done add boostMultiplier to shipspeed
    private int boostMultiplier;
    public TMP_Text shipSpeedText;
    public TMP_Text traveledText;
    public plantBoostSingleton plantEffects;


    private void Update()
    {
        updateSpeedUI();
    }
    private void Start()
    {
        spaceShipSpeed = GameManager.Instance.spaceShipSpeed;
        distanceTraveled = GameManager.Instance.totalDistanceTraveled;
        StartCoroutine("distanceCalculator");
    }

    private int getShipSpeed()
    {
        spaceShipSpeed = 0;
        //TODO:get minigame levels to determine speed if none of them are on
        //TODO:get each minigames speedboost

        //this is a placeholder
        //if no games are played and all levels are 1:
        if (_shipManager.isShipStopped == false)
        {
            if (GameManager.Instance.cockpitBoostOn)
            {
                //TODO: check all possible buffs to spaceShipSpeed speed, well rested bonus, plants, foods, etc
                //TODO: after above todo, add all xp boosts, and with the outcoming number, do methods below.
                spaceShipSpeed += getCockPitSpeedBoost();
            }
            if (GameManager.Instance.gardenBoostOn && _oxygenGardenState.areAllPlantsBlank() == false)
            {
                spaceShipSpeed += _wateringEvent.getBoostAmount();
            }
            if (GameManager.Instance.turretsBoostOn)
            {
                spaceShipSpeed += getTurretSpeedBoost();
            }
        }

        return spaceShipSpeed;

        //TODO:save and load all these values from file
    }

    //below functions calculate each rooms contribution (if any) to the spaceships speed.
    //these are done separately for each room, because each room's XP has to be calculated separately.
    //figure out a good way to work with speed, and XPmanager script, since not all rooms contribute directly
    //to spaceship speed.


    public int getCockPitSpeedBoost()
    {
        int addedSpeed = 0;
        addedSpeed += _cockpitMinigame.cockpitBoost();
        if (GameManager.Instance.gardenBoostOn)
        {
            for (int i = 0; i < _oxygenGardenState.getPlantsInSpots().Length; i++)
            {
                switch (_oxygenGardenState.getPlantsInSpots()[i].plantId)
                {
                    case 1:
                        addedSpeed = (int)(addedSpeed * 1.05);
                        break;
                    case 5:
                        addedSpeed = (int)(addedSpeed * 1.1);
                        break;
                    case 6:
                        addedSpeed = (int)(addedSpeed * 0.95);
                        break;
                    case 0:
                        break;
                    case 11:
                        addedSpeed = (int)(addedSpeed * 1.1);
                        break;
                    default:
                        Debug.Log("didnt find any plants helping this room");
                        break;
                }
            }
        }
        /*switch (_foodgeneratorState.getFoodInSpot().foodId)
        {
            case 1:
                addedSpeed = (int)(addedSpeed * 1.01);
                break;
            case 0:
                break;
            default:
                Debug.LogError("ERROR!!! DIDNT FIND ANY FOOD ID'S");
                break;
        }*/
        return addedSpeed;
    }

    public int getTurretSpeedBoost()
    {
        int addedSpeed = 0;
        addedSpeed += _turretMiniGame.getBoost();
        if (GameManager.Instance.gardenBoostOn)
        {
            for (int i = 0; i < _oxygenGardenState.getPlantsInSpots().Length; i++)
            {
                switch (_oxygenGardenState.getPlantsInSpots()[i].plantId)
                {
                    case 3:
                        addedSpeed = (int)(addedSpeed * 1.05);
                        break;
                    case 5:
                        addedSpeed = (int)(addedSpeed * 1.1);
                        break;
                    case 6:
                        addedSpeed = (int)(addedSpeed * 0.95);
                        break;
                    case 11:
                        addedSpeed = (int)(addedSpeed * 1.1);
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }
            }
        }
        switch (_foodgeneratorState.getFoodInSpot().foodId)
        {
            case 1:
                addedSpeed = (int)(addedSpeed * 1.01);
                break;
            case 0:
                break;
            default:
                Debug.LogError("ERROR!!! DIDNT FIND ANY FOOD ID'S");
                break;
        }
        return addedSpeed;
    }
    //TODO: make above-like functions for other rooms too





    //

    public void updateSpeedUI()
    {
        int shipSpeed = getShipSpeed();
        string shipSpeedDisplay = shipSpeed.ToString();
        shipSpeedDisplay = shipSpeedDisplay.Insert(shipSpeedDisplay.Length - 1, ".");

        shipSpeedText.SetText(shipSpeedDisplay + " km/s");

        string distance = distanceTraveled.ToString();
        distance = distance.Insert(distance.Length - 1, ".");

        traveledText.SetText(distance + " km traveled");
    }
    private IEnumerator distanceCalculator()
    {
        while (true)
        {
            distanceTraveled += spaceShipSpeed;
            GameManager.Instance.totalDistanceTraveled = distanceTraveled;
            yield return new WaitForSeconds(1f);
        }
    }
}
