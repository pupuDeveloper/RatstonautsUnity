using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class xpProgressBar : MonoBehaviour
{
    [SerializeField] private int min;
    [SerializeField] private int max;
    [SerializeField] private int current;
    [SerializeField] private Image mask;
    private RectTransform maskRect;
    [SerializeField] private RectTransform tracker;
    private xpManager _xpManager;

    void Start()
    {
        _xpManager = GameObject.Find("xpmanager").GetComponent<xpManager>();
        maskRect = mask.GetComponent<RectTransform>();
    }
    private void FixedUpdate()
    {
        getValues();
        getCurrentFill();
    }


    private void getCurrentFill()
    {
        float currentOffset = current - min;
        float maxOffset = max - min;
        float fillAmount = Mathf.Abs(currentOffset / maxOffset);
        mask.fillAmount = fillAmount;
        
        tracker.anchoredPosition = new Vector2(tracker.rect.width * fillAmount,0);
    }
    private void getValues()
    {
        string whichSkill = transform.parent.name;
        switch (whichSkill)
        {
            case "skill1":
            min = _xpManager.xpForLevels[_xpManager.cockPitLvl];
            max = _xpManager.xpForLevels[_xpManager.cockPitLvl + 1];
            current = GameManager.Instance.cockPitXP;
            break;
            case "skill2":
            min = _xpManager.xpForLevels[_xpManager.oxygenGardenLvl];
            max = _xpManager.xpForLevels[_xpManager.oxygenGardenLvl + 1];
            current = GameManager.Instance.gardenXP;
            break;
            case "skill3":
            min = _xpManager.xpForLevels[_xpManager.turretsLvl];
            max = _xpManager.xpForLevels[_xpManager.turretsLvl + 1];
            current = GameManager.Instance.turretsXP;
            break;
            case "skill4":
            min = _xpManager.xpForLevels[_xpManager.foodGenLvl];
            max = _xpManager.xpForLevels[_xpManager.foodGenLvl + 1];
            current = GameManager.Instance.foodGenXP;
            break;
            case "skill5":
            min = _xpManager.xpForLevels[_xpManager.sleepingQuartersLvl];
            max = _xpManager.xpForLevels[_xpManager.sleepingQuartersLvl + 1];
            current = GameManager.Instance.quartersXP;
            break;
            default:
            Debug.LogWarning("this is an error!");
            break;
        }
    }
}
