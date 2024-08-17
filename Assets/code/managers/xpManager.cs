using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class xpManager : MonoBehaviour
{
    //this class keeps track of, and calculates xp gain and levels.
    //use gamemanager's getTimeSinceLastSave to calculate xp gained while not in game,
    //based on TimeDate differences

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
        float xpAverage = 0;
        for (int i = 1; i < xpForLevels.Length; i++)
        {
            xpAverage += (i * 100) * (float)Math.Pow(2, i/12); 
            xpForLevels[i] = (int)Math.Floor(xpAverage);
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
            if (currentXp > xpForLevels[i])
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
}
