using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class roomBoardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text skillLvlText1;
    [SerializeField] private TMP_Text skillLvlText2;
    [SerializeField] private TMP_Text skillLvlText3;
    [SerializeField] private TMP_Text skillLvlText4;
    [SerializeField] private TMP_Text skillLvlText5;
    [SerializeField] private TMP_Text skillXPText1;
    [SerializeField] private TMP_Text skillXPText2;
    [SerializeField] private TMP_Text skillXPText3;
    [SerializeField] private TMP_Text skillXPText4;
    [SerializeField] private TMP_Text skillXPText5;
    [SerializeField] private xpManager _xpManager;

    void Update()
    {
        skillLvlText1.SetText("Level: " + _xpManager.cockPitLvl.ToString());
        skillLvlText2.SetText("Level: " + _xpManager.oxygenGardenLvl.ToString());
        skillLvlText3.SetText("Level: " + _xpManager.turretsLvl.ToString());
        skillLvlText4.SetText("Level: " + _xpManager.foodGenLvl.ToString());
        skillLvlText5.SetText("Level: " + _xpManager.sleepingQuartersLvl.ToString());

        int xp1 = GameManager.Instance.cockPitXP / 10;
        int xp2 = GameManager.Instance.gardenXP / 10;
        int xp3 = GameManager.Instance.turretsXP / 10;
        int xp4 = GameManager.Instance.foodGenXP / 10;
        int xp5 = GameManager.Instance.quartersXP / 10;

        skillXPText1.SetText("XP: " + xp1.ToString());
        skillXPText2.SetText("XP: " + xp2.ToString());
        skillXPText3.SetText("XP: " + xp3.ToString());
        skillXPText4.SetText("XP: " + xp4.ToString());
        skillXPText5.SetText("XP: " + xp5.ToString());
    }
}
