using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    private int[] xpForLevels = new int[100];
    public int cockPitLvl { get; private set; }
    public int foodGenLvl { get; private set; }
    public int oxygenGardenLvl { get; private set; }
    public int sleepingQuartersLvl { get; private set; }
    public int turretsLvl { get; private set; }
    private float lastTimeCalled = 0.0f;
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
        Debug.LogError("shouldnt return this ever its an error");
        return null;
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
        if (_oxygengardenState.areAllPlantsBlank() == false)
        {
            gardenPassiveXP(xpToAdd);
        }
        StartCoroutine(showXpInUI(0, xpToAdd));
        if (updateLevel(GameManager.Instance.cockPitXP, cockPitLvl))
        {
            cockPitLvl++;
        }
        updateTotalXp(xpToAdd);
        cockPitCoroutine = true;
    }
    private void gardenPassiveXP(int xpToAdd)
    {
        xpToAdd = xpToAdd / 3;
        GameManager.Instance.gardenXP = addXp(GameManager.Instance.gardenXP, xpToAdd);
        StartCoroutine(showXpInUI(1, xpToAdd));
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
        StartCoroutine(showXpInUI(1, xpToAdd));
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
        StartCoroutine(showXpInUI(0, xpToAdd));
        if (updateLevel(GameManager.Instance.cockPitXP, cockPitLvl))
        {
            cockPitLvl++;
        }
        updateTotalXp(xpToAdd);
    }

    private IEnumerator showXpInUI(int roomId, int xpAmount)
    {
        float timeDifference = Time.time - lastTimeCalled;
        if (timeDifference < 0.50f)
        {
            float waitTime = 0.5f - timeDifference;
            yield return new WaitForSeconds(waitTime);
        }
        Sprite displaySprite = null;
        switch (roomId)
        {
            case 0:
                displaySprite = Resources.Load<Sprite>("roomButtonImages/Radar1");
                break;
            case 1:
                displaySprite = Resources.Load<Sprite>("roomButtonImages/Branch1");
                break;
            case 2:
                displaySprite = Resources.Load<Sprite>("roomButtonImages/Meteorite5");
                break;
            case 3:
                displaySprite = Resources.Load<Sprite>("roomButtonImages/CockHat2");
                break;
            case 4:
                displaySprite = Resources.Load<Sprite>("roomButtonImages/Sofa2");
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
        Instantiate(xpPopUpPrefab, new UnityEngine.Vector3(-5, 5.75f, 10), transform.rotation);
        lastTimeCalled = Time.time;
    }
}
