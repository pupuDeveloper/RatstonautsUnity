using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System;


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

    private int[] xpForLevels = new int[100];
    public int cockPitLvl {get; private set; }
    public int foodGenLvl {get; private set; }
    public int oxygenGardenLvl {get; private set; } 
    public int sleepingQuartersLvl {get; private set; }
    public int turretsLvl {get; private set; }
            /*xpNeededForResult += (L * 100f) * (float)Math.Pow(2, L/12); 
            int resultXp = (int)Math.Floor(xpNeededForResult);
            Debug.Log("xp needed for lvl " + L + " is " + resultXp);*/

    private void Start()
    {
        xpForLevels[0] = 0;
        int xpAverage = 0;
        for (int i = 1; i < xpForLevels.Length; i++)
        {
            xpAverage += (i * 100) * (int)BigInteger.Pow(2, i/12) * 10; // times 10, because last digit is a decimal.
            xpForLevels[i] = xpAverage;
            Debug.Log("xp needed for lvl " + i + " is " + xpForLevels[i]);
        }
        cockPitLvl = checkLvls(GameManager.Instance.cockPitXP);
        foodGenLvl = checkLvls(GameManager.Instance.foodGenXP);
        oxygenGardenLvl = checkLvls(GameManager.Instance.gardenXP);
        sleepingQuartersLvl = checkLvls(GameManager.Instance.quartersXP);
        turretsLvl = checkLvls(GameManager.Instance.turretsXP);
    }
    public int checkLvls(int currentXp)
    {
        for (int i = 0; i < xpForLevels.Length; i++)
        {
            if (currentXp >= xpForLevels[i])
            {
                return i;
            }
        }
        Debug.LogError("xp value is not valid!");
        return -1;
    }
    public bool updateLevel(int currentXp, int addedXp, int roomLvl)
    {
        if (currentXp + addedXp > xpForLevels[roomLvl])
        {
            return true;
        }
        return false;
    }
    public int addXp (int currentXp, int addedXp)
    {
        return currentXp += addedXp;
    }

    private string convertToUIText(int xp)
    {
        xp = xp/10;
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
}
