using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class wateringEvent : MonoBehaviour
{
    [SerializeField] private oxygengardenState _oxygengardenState;
    [SerializeField] private gardenManager _gardenManager;
    [SerializeField] private GameObject[] popups = new GameObject[3];
    [SerializeField] private xpManager _xpManager;
    private int level0boost;
    private int boostOnWatering;
    private int minTimeSec = 3600;
    private int maxTimeSec = 18000;


    private void Start()
    {
        level0boost = 50;
    }

    public void gardenEvent()
    {
        for (int i = 0; i < _oxygengardenState.getPlantsInSpots().Count; i++)
        {
            if (_oxygengardenState.getPlantsInSpots()[i] != _gardenManager.blank)
            {
                popups[i].SetActive(true);
            }
        }
    }
    public void plantsWatered()
    { 
        //TODO: play animation when watering plants, XP reward too.
        calculateboost();
        _xpManager.wateringXPReward();
        GameManager.Instance.timeSinceGardenCDStarted = DateTime.Now;
        GameManager.Instance.triggerFoodGenMG = DateTime.Now.AddSeconds(UnityEngine.Random.Range(minTimeSec, maxTimeSec));
        _gardenManager.arePlantsWatered = true;
        GameManager.Instance.gardenBoostOn = true;
    }

    private void calculateboost()
    {
        int spotMultiplier = 0;
        foreach (GameObject o in popups)
        {
            o.SetActive(false);
            spotMultiplier++;
        }
        boostOnWatering = level0boost * spotMultiplier;
    }
    public int getBoostAmount()
    {
        calculateboost();
        return boostOnWatering;
    }
}
