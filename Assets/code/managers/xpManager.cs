using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int cockPitLvl;
    private int foodGenLvl;
    private int oxygenGardenLvl;
    private int sleepingQuartersLvl;
    private int turretsLvl;


    private void Start()
    {
        xpForLevels[0] = 0;
        for (int i = 1; i < xpForLevels.Length; i++)
        {
            float xpAverage += (i * 100f) * (float)Math.Pow(2, i/12); 
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
            
        }
    }
}
