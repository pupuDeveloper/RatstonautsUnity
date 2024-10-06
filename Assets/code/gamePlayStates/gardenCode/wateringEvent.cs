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
    public int minTimeSec { get; private set; }
    public int maxTimeSec { get; private set; }


    private void Start()
    {
        level0boost = 50;
        minTimeSec = 3600;
        maxTimeSec = 18000;
    }

    public void gardenEvent(bool dryPlant)
    {
        if (dryPlant)
        {
            foreach (var p in popups)
            {
                p.SetActive(true);
            }
        }
        else
        {
            foreach (var p in popups)
            {
                p.SetActive(false);
            }
        }
    }

    public void plantsWatered()
    {
        calculateboost();
        _xpManager.wateringXPReward();
        GameManager.Instance.timeSinceGardenCDStarted = DateTime.Now;
        GameManager.Instance.triggerGardenWatering = DateTime.Now.AddSeconds(UnityEngine.Random.Range(minTimeSec, maxTimeSec));
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
