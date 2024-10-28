using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class xpManager : MonoBehaviour
{
    //this class keeps track of, and calculates xp gain and levels.
    //use gamemanager's getTimeSinceLastSave to calculate xp gained while not in game,
    //based on TimeDate differences


    // IN CODE, ALL INTEGERS ARE 10 TIMES LARGER THAN USUAL, SO IN CODE 105 + 38 = 143 MEANS IN GAME, AND IN UI 10.5 + 3.8 = 14.3

    [SerializeField] private cockpitState _cockpitState;
    [SerializeField] private foodgeneratorState _foodgeneratorState;
    [SerializeField] private oxygengardenState _oxygengardenState;
    [SerializeField] private sleepingquartersState _sleepingquartersState;
    [SerializeField] private turretsState _turretsState;
    [SerializeField] private gameStats _gameStats;
    [SerializeField] private wateringEvent _wateringEvent;
    [SerializeField] private TMP_Text totalXpText;
    [SerializeField] private GameObject xpPopUpPrefab;
    private Transform canvasForPopup;
    public int[] xpForLevels { get; private set; }
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
    private bool turretCoroutine;
    public Action<bool, string> onBoostChanged;
    private Queue<Tuple<int, int>> popupArgs = new Queue<Tuple<int, int>>();
    private bool popupOnCDOn;

    public void changeBoost(bool toWhat, string whichRoom)
    {
        switch (whichRoom)
        {
            case "cockpit":
                GameManager.Instance.cockpitBoostOn = toWhat;
                break;
            case "oxygengarden":
                GameManager.Instance.gardenBoostOn = toWhat;
                break;
            case "turrets":
                GameManager.Instance.turretsBoostOn = toWhat;
                break;
            case "foodgen":
                GameManager.Instance.foodGenBoostOn = toWhat;
                break;
            default:
                Debug.LogWarning("THIS SHOULDNT HAPPEN! BREAKS GAME");
                break;
        }
    }
    private void Start()
    {
        canvasForPopup = GameObject.Find("worldSpaceCanvas").transform.GetChild(0).transform;
        xpForLevels = new int[100];
        xpForLevels[0] = 0;
        int xpAverage = 0;
        for (int i = 1; i < xpForLevels.Length; i++)
        {
            xpAverage += (i * 100) * (int)BigInteger.Pow(2, i / 12) * 10; // times 10, because last digit is a decimal.
            xpForLevels[i] = xpAverage;
        }
        cockPitLvl = checkLvls(GameManager.Instance.cockPitXP);
        foodGenLvl = checkLvls(GameManager.Instance.foodGenXP);
        oxygenGardenLvl = checkLvls(GameManager.Instance.gardenXP);
        sleepingQuartersLvl = checkLvls(GameManager.Instance.quartersXP);
        turretsLvl = checkLvls(GameManager.Instance.turretsXP);
        cockPitCoroutine = true;
        turretCoroutine = true;
        calculateXP();
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

        if (GameManager.Instance.turretsBoostOn && turretCoroutine)
        {
            StartCoroutine("trackTurretXP");
        }

        if (GameManager.Instance.turretsBoostOn == false)
        {
            StopCoroutine("trackTurretXP");
        }

        if (popupOnCDOn == false && popupArgs.Count != 0 && GameManager.Instance.showPopups)
        {
            StartCoroutine(showXpInUI(popupArgs.Peek().Item1, popupArgs.Peek().Item2));
        }
        if (GameManager.Instance.showPopups == false && popupArgs.Count != 0)
        {
            popupArgs.Clear();
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
        Debug.Log("xp value is: " + currentXp);
        Debug.LogError("xp value is not valid!");
        return -1;
    }
    public bool updateLevel(int currentXp, int roomLvl)
    {
        if (currentXp > xpForLevels[roomLvl + 1])
        {
            return true;
        }
        return false;
    }
    public int addXp(int currentXp, int addedXp)
    {
        int result = currentXp = currentXp + addedXp;
        return result;
    }

    private string convertToUIText(int xp, bool total, bool decPoint)
    {
        string stringToDisplay = "";

        if (total)
        {
            xp = xp / 10;
            stringToDisplay = xp.ToString();
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

        else
        {

            if (decPoint)
            {
                stringToDisplay = xp.ToString();
                int stringPos = stringToDisplay.Length - 1;
                stringToDisplay = stringToDisplay.Insert(stringPos, ".");

                int stringLenght = stringToDisplay.Length;
                if (stringLenght > 4)
                {
                    stringToDisplay = stringToDisplay.Insert(3, ",");
                }

                if (stringLenght > 7)
                {
                    stringToDisplay = stringToDisplay.Insert(3, ",");
                    stringToDisplay = stringToDisplay.Insert(6, ",");
                }
                return stringToDisplay;
            }

            else
            {
                xp = xp / 10;
                stringToDisplay = xp.ToString();
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
                return stringToDisplay;
            }
        }
    }
    public void updateTotalXp(int addedXp)
    {
        //first do it to file
        GameManager.Instance.totalXp = addXp(GameManager.Instance.getTotalXP(), addedXp);
        //then do it in UI
        string myXp = convertToUIText(GameManager.Instance.getTotalXP(), true, false);
        totalXpText.SetText("TOTAL XP<br>" + myXp);
    }

    private IEnumerator trackCockPitXP()
    {
        cockPitCoroutine = false;
        yield return new WaitForSeconds(3);
        int xpToAdd;
        xpToAdd = _gameStats.getCockPitSpeedBoost();
        GameManager.Instance.cockPitXP = addXp(GameManager.Instance.cockPitXP, xpToAdd);
        if (_oxygengardenState.doPlantsAffectRoom("cockpit") && GameManager.Instance.gardenBoostOn)
        {
            gardenPassiveXP(xpToAdd);
        }
        popupArgs.Enqueue(new Tuple<int, int>(0, xpToAdd));
        if (updateLevel(GameManager.Instance.cockPitXP, cockPitLvl))
        {
            cockPitLvl++;
        }
        updateTotalXp(xpToAdd);
        cockPitCoroutine = true;
    }
    private IEnumerator trackTurretXP()
    {
        turretCoroutine = false;
        yield return new WaitForSeconds(3);
        int xpToAdd;
        xpToAdd = _gameStats.getTurretSpeedBoost();
        GameManager.Instance.turretsXP = addXp(GameManager.Instance.turretsXP, xpToAdd);
        if (_oxygengardenState.doPlantsAffectRoom("turrets") && GameManager.Instance.gardenBoostOn)
        {
            gardenPassiveXP(xpToAdd);
        }
        popupArgs.Enqueue(new Tuple<int, int>(2, xpToAdd));
        if (updateLevel(GameManager.Instance.turretsXP, turretsLvl))
        {
            turretsLvl++;
        }
        updateTotalXp(xpToAdd);
        turretCoroutine = true;
    }
    private void gardenPassiveXP(int xpToAdd)
    {
        xpToAdd = xpToAdd / 3;
        GameManager.Instance.gardenXP = addXp(GameManager.Instance.gardenXP, xpToAdd);
        popupArgs.Enqueue(new Tuple<int, int>(1, xpToAdd));
        if (updateLevel(GameManager.Instance.gardenXP, oxygenGardenLvl))
        {
            oxygenGardenLvl++;
        }
        updateTotalXp(xpToAdd);
    }
    public void wateringXPReward()
    {
        int xpToAdd = _wateringEvent.getBoostAmount();
        if (oxygenGardenLvl != 0)
        {
            xpToAdd = xpToAdd * oxygenGardenLvl * 10;
        }
        else
        {
            xpToAdd = xpToAdd * 10;
        }
        GameManager.Instance.gardenXP = addXp(GameManager.Instance.gardenXP, xpToAdd);
        popupArgs.Enqueue(new Tuple<int, int>(1, xpToAdd));
        if (updateLevel(GameManager.Instance.gardenXP, oxygenGardenLvl))
        {
            oxygenGardenLvl++;
        }
        updateTotalXp(xpToAdd);
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
        popupArgs.Enqueue(new Tuple<int, int>(0, xpToAdd));
        if (updateLevel(GameManager.Instance.cockPitXP, cockPitLvl))
        {
            cockPitLvl++;
        }
        updateTotalXp(xpToAdd);
    }
    public void turretMGReward()
    {
        int xpToAdd = _gameStats.getTurretSpeedBoost();
        if (turretsLvl != 0)
        {
            xpToAdd = xpToAdd * turretsLvl * 10;
        }
        else
        {
            xpToAdd = xpToAdd * 10;
        }
        GameManager.Instance.turretsXP = addXp(GameManager.Instance.turretsXP, xpToAdd);
        popupArgs.Enqueue(new Tuple<int, int>(2, xpToAdd));
        if (updateLevel(GameManager.Instance.turretsXP, turretsLvl))
        {
            turretsLvl++;
        }
        updateTotalXp(xpToAdd);
    }

    private IEnumerator showXpInUI(int roomId, int xpAmount)
    {
        popupOnCDOn = true;
        yield return new WaitForSeconds(0.65f);
        Sprite displaySprite = null;
        switch (roomId)
        {
            case 0:
                displaySprite = Resources.LoadAll<Sprite>("roomButtonImages/icons1")[4];
                break;
            case 1:
                displaySprite = Resources.LoadAll<Sprite>("roomButtonImages/icons1")[3];
                break;
            case 2:
                displaySprite = Resources.LoadAll<Sprite>("roomButtonImages/icons1")[1];
                break;
            case 3:
                displaySprite = Resources.LoadAll<Sprite>("roomButtonImages/icons1")[2];
                break;
            case 4:
                displaySprite = Resources.LoadAll<Sprite>("roomButtonImages/icons1")[0];
                break;
        }
        xpPopUpPrefab.GetComponent<SpriteRenderer>().sprite = displaySprite;
        if (xpAmount % 10 != 0)
        {
            xpPopUpPrefab.GetComponentInChildren<TMP_Text>().SetText(convertToUIText(xpAmount, false, true));
        }
        else
        {
            xpPopUpPrefab.GetComponentInChildren<TMP_Text>().SetText(convertToUIText(xpAmount, false, false));
        }
        Instantiate(xpPopUpPrefab, new UnityEngine.Vector3(Camera.main.orthographicSize/ -2.5f, Camera.main.orthographicSize/2, 10), transform.rotation, canvasForPopup);
        if (popupArgs.Count != 0) popupArgs.Dequeue();
        popupOnCDOn = false;
    }
    private int calcOfflineTime(string roomName)
    {
        roomName = roomName.ToLower();
        DateTime endTime = DateTime.Now;
        DateTime validTimeLimiter = new DateTime(2000, 01, 01);
        DateTime startTime = GameManager.Instance.lastTimePlayed;
        int dropAmount = 0;
        switch (roomName)
        {
            case "cockpit":
                //check if times are valid
                if (GameManager.Instance.triggerCockPitMG < validTimeLimiter)
                {
                    //invalid time, triggercockpit was never set, so minigame was never done.
                    dropAmount = 0;
                    break;
                }
                //if trigger cockpit was before starting the game, that is end endtime
                if (GameManager.Instance.triggerCockPitMG < endTime)
                {
                    endTime = GameManager.Instance.triggerCockPitMG;
                }
                //check if starttime is valid
                if (startTime < validTimeLimiter)
                {
                    dropAmount = 0;
                    break;
                }
                dropAmount = (int)(endTime - startTime).TotalSeconds;
                break;
            case "oxygengarden":
                if (GameManager.Instance.triggerGardenWatering < validTimeLimiter)
                {
                    dropAmount = 0;
                    break;
                }
                if (GameManager.Instance.triggerGardenWatering < endTime)
                {
                    endTime = GameManager.Instance.triggerGardenWatering;
                }
                if (startTime < validTimeLimiter)
                {
                    dropAmount = 0;
                    break;
                }
                dropAmount = (int)(endTime - startTime).TotalSeconds;
                break;
            case "turrets":
                if (GameManager.Instance.triggerTurretsMG < validTimeLimiter)
                {
                    dropAmount = 0;
                    break;
                }
                if (GameManager.Instance.triggerTurretsMG < endTime)
                {
                    endTime = GameManager.Instance.triggerTurretsMG;
                }
                if (startTime < validTimeLimiter)
                {
                    dropAmount = 0;
                    break;
                }
                dropAmount = (int)(endTime - startTime).TotalSeconds;
                break;
        }
        if (dropAmount < 0) dropAmount = 0;
        return dropAmount / 3;
    }

    private void calculateXP()
    {
        int xpToAdd;
        int totalXpOffline1 = 0; //cockpit
        int totalXpOffline2 = 0; //oxygengarden
        int totalXpOffline3 = 0; //turrets
        int howManyXpDrops;

        if (calcOfflineTime("cockpit") > 0)
        {
            howManyXpDrops = calcOfflineTime("cockpit");
            for (int i = 0; i < howManyXpDrops; i++)
            {
                xpToAdd = _gameStats.getCockPitSpeedBoost();
                totalXpOffline1 += xpToAdd;
                GameManager.Instance.cockPitXP = addXp(GameManager.Instance.cockPitXP, xpToAdd);
                if (_oxygengardenState.doPlantsAffectRoom("cockpit") && i < calcOfflineTime("oxygengarden"))
                {
                    totalXpOffline2 += xpToAdd / 3;
                }
                if (updateLevel(GameManager.Instance.cockPitXP, cockPitLvl))
                {
                    cockPitLvl++;
                }
            }
            if (totalXpOffline1 != 0) popupArgs.Enqueue(new Tuple<int, int>(0, totalXpOffline1));
        }

        howManyXpDrops = 0;

        if (calcOfflineTime("turrets") > 0)
        {
            howManyXpDrops = calcOfflineTime("turrets");
            for (int i = 0; i < howManyXpDrops; i++)
            {
                xpToAdd = _gameStats.getTurretSpeedBoost();
                totalXpOffline3 += xpToAdd;
                GameManager.Instance.turretsXP = addXp(GameManager.Instance.turretsXP, xpToAdd);
                if (_oxygengardenState.doPlantsAffectRoom("turrets") && i < calcOfflineTime("oxygengarden"))
                {
                    totalXpOffline2 += xpToAdd / 3;
                }
                if (updateLevel(GameManager.Instance.turretsXP, turretsLvl))
                {
                    turretsLvl++;
                }
            }
            if (totalXpOffline3 != 0) popupArgs.Enqueue(new Tuple<int, int>(2, totalXpOffline3));
        }
        if (totalXpOffline2 != 0) popupArgs.Enqueue(new Tuple<int, int>(1, totalXpOffline2));
        updateTotalXp(totalXpOffline1 + totalXpOffline2 + totalXpOffline3);
    }
}

