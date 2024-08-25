using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System;
using TMPro;

public class xpManager : MonoBehaviour
{
    //this class keeps track of, and calculates xp gain and levels.
    //use gamemanager's getTimeSinceLastSave to calculate xp gained while not in game,
    //based on TimeDate differences

    //TODO: HUGE TODO!!! CHANGE ALL FLOATING POINT VALUES TO INTEGERS
    // IN CODE, ALL INTEGERS ARE 10 TIMES LARGER THAN USUAL, SO IN CODE 105 + 38 = 143 MEANS IN GAME, AND IN UI 10.5 + 3.8 = 14.3

    [SerializeField] private cockpitState _cockpitState;
    [SerializeField] private foodgeneratorState _foodgeneratorState;
    [SerializeField] private oxygengardenState _oxygengardenState;
    [SerializeField] private sleepingquartersState _sleepingquartersState;
    [SerializeField] private turretsState _turretsState;
    [SerializeField] private gameStats _gameStats;


    [SerializeField] private TMP_Text totalXpText;
    private int[] xpForLevels = new int[100];
    public int cockPitLvl { get; private set; }
    public int foodGenLvl { get; private set; }
    public int oxygenGardenLvl { get; private set; }
    public int sleepingQuartersLvl { get; private set; }
    public int turretsLvl { get; private set; }
    /*xpNeededForResult += (L * 100f) * (float)Math.Pow(2, L/12); 
    int resultXp = (int)Math.Floor(xpNeededForResult);
    Debug.Log("xp needed for lvl " + L + " is " + resultXp);*/

    //bools

    private bool cockPitCoroutine;

    private void Start()
    {
        xpForLevels[0] = 0;
        int xpAverage = 0;
        for (int i = 1; i < xpForLevels.Length; i++)
        {
            xpAverage += (i * 100) * (int)BigInteger.Pow(2, i / 12) * 10; // times 10, because last digit is a decimal.
            xpForLevels[i] = xpAverage;
            Debug.Log("xp needed for lvl " + i + " is " + xpForLevels[i]);
        }
        cockPitLvl = checkLvls(GameManager.Instance.cockPitXP);
        foodGenLvl = checkLvls(GameManager.Instance.foodGenXP);
        oxygenGardenLvl = checkLvls(GameManager.Instance.gardenXP);
        sleepingQuartersLvl = checkLvls(GameManager.Instance.quartersXP);
        turretsLvl = checkLvls(GameManager.Instance.turretsXP);
        cockPitCoroutine = true;
    }

    private void Update()
    {
        if (GameManager.Instance.cockpitBoostOn && cockPitCoroutine)
        {
            StartCoroutine("trackCockPitXP");
        }
        
        if (GameManager.Instance.cockpitBoostOn == false)
        {
            StopCoroutine("trackCockPitXP");
        }
    }
    public int checkLvls(int currentXp)
    {
        for (int i = 0; i < xpForLevels.Length; i++)
        {
            if (currentXp < xpForLevels[i])
            {
                return i - 1;
            }
        }
        Debug.LogError("xp value is not valid!");
        return -1;
    }
    public bool updateLevel(int currentXp, int roomLvl)
    {
        if (currentXp > xpForLevels[roomLvl + 1])
        {
            Debug.Log("LEVELD UP!");
            return true;
        }
        return false;
    }
    public int addXp(int currentXp, int addedXp)
    {
        int result = currentXp = currentXp + addedXp;
        return result;
    }

    private string convertToUIText(int xp)
    {
        xp = xp / 10;
        string stringToDisplay = xp.ToString();
        int stringLenght = stringToDisplay.Length;

        if (stringLenght > 3)
        {
            stringToDisplay = stringToDisplay.Insert(3, ",");
        }

        if (stringLenght > 6)
        {
            stringToDisplay = stringToDisplay.Insert(3, ",");
            stringToDisplay = stringToDisplay.Insert(6, ",");
        }

        if (xp >= 200000000)
        {
            stringToDisplay = "200,000,000";
        }

        return stringToDisplay;
    }
    public void updateTotalXp(int addedXp)
    {
        //first do it to file
        GameManager.Instance.totalXp = addXp(GameManager.Instance.getTotalXP(), addedXp);
        //then do it in UI
        string myXp = convertToUIText(GameManager.Instance.getTotalXP());
        totalXpText.SetText("TOTAL XP<br>" + myXp);
    }

    private IEnumerator trackCockPitXP()
    {
        cockPitCoroutine = false;
        yield return new WaitForSeconds(3);
        int xpToAdd;
        xpToAdd = _gameStats.getCockPitSpeedBoost();
        GameManager.Instance.cockPitXP = addXp(GameManager.Instance.cockPitXP, xpToAdd);
        if (updateLevel(GameManager.Instance.cockPitXP, cockPitLvl))
        {
            cockPitLvl++;
        }
        updateTotalXp(xpToAdd);
        cockPitCoroutine = true;
    }
    public void cockpitMGXPReward()
    {
        int xpToAdd = _gameStats.getCockPitSpeedBoost();
        if (cockPitLvl != 0)
        {
            xpToAdd = xpToAdd * cockPitLvl * 10;
        }
        else
        {
            xpToAdd = xpToAdd * 10;
        }

        GameManager.Instance.cockPitXP = addXp(GameManager.Instance.cockPitXP, xpToAdd);
        if (updateLevel(GameManager.Instance.cockPitXP, cockPitLvl))
        {
            cockPitLvl++;
        }
        updateTotalXp(xpToAdd);
    }
}
