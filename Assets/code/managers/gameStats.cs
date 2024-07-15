using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class gameStats : MonoBehaviour
{
    public float spaceShipSpeed { get; private set; } //spaceships speed km/per second
    public float distanceTraveled { get; private set;} // total distance traveled
    [SerializeField] private cockpitMiniGame _cockpitMinigame;
    [SerializeField] private cockpitState _cockPitState;
    [SerializeField] private oxygengardenState _oxygenGardenState;
    private bool speedBoost; //when all available minigames are done add boostMultiplier to shipspeed
    private int boostMultiplier;
    public TMP_Text shipSpeedText;
    public TMP_Text traveledText;
    public bool calcRunning;
    public plantBoostSingleton plantEffects;



    private void Update()
    {
        if (!calcRunning)
        {
            StartCoroutine("distanceCalculator");
        }
    }
    private void Start()
    {
        calcRunning = false;
        spaceShipSpeed = 0;
        updatedSpeed = 0;
    }

    private float getShipSpeed()
    {
        spaceShipSpeed = 0;
        //TODO:get minigame levels to determine speed if none of them are on
        //TODO:get each minigames speedboost

        //this is a placeholder
        //if no games are played and all levels are 1:
        if (GameManager.Instance.isShipStopped == false)
        {
            if (_cockPitState.boost)
            {
                spaceShipSpeed += _cockpitMinigame.cockpitBoost();
            }
            if (_oxygenGardenState.plantsWatered)
            {
                plantEffects.getEffect();
                if (plantEffects.boostShipSpeed)
                {
                    spaceShipSpeed *= plantEffects.plantEffectsOnShipSpeed();
                }
            }
        }

        return spaceShipSpeed;

        //TODO:save and load all these values from file
    }

    public void updateSpeedUI()
    {
        float shipSpeed = MathF.Round(getShipSpeed(), 2);
        shipSpeedText.SetText(shipSpeed + " km/s");
        traveledText.SetText(distanceTraveled + " km traveled");
    }
    private IEnumerator distanceCalculator()
    {
        calcRunning = true;
        float dividedSpeed = spaceShipSpeed / 10;
        distanceTraveled += dividedSpeed;
        yield return new WaitForSeconds(0.1f);
        calcRunning = false;
    }
}
