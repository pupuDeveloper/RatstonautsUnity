using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class roomBoardUI : MonoBehaviour
{
    private TMP_Text skillLvlText1;
    private TMP_Text skillLvlText2;
    private TMP_Text skillLvlText3;
    private TMP_Text skillLvlText4;
    private TMP_Text skillLvlText5;
    private TMP_Text skillXPText1;
    private TMP_Text skillXPText2;
    private TMP_Text skillXPText3;
    private TMP_Text skillXPText4;
    private TMP_Text skillXPText5;
    private xpManager _xpManager;

    private void Start()
    {
        skillLvlText1 = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        skillXPText1 = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();

        skillLvlText2 = transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        skillXPText2 = transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>();

        skillLvlText3 = transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>();
        skillXPText3 = transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>();

        skillLvlText4 = transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>();
        skillXPText4 = transform.GetChild(3).GetChild(1).GetComponent<TMP_Text>();

        skillLvlText5 = transform.GetChild(4).GetChild(0).GetComponent<TMP_Text>();
        skillXPText5 = transform.GetChild(4).GetChild(1).GetComponent<TMP_Text>();

        _xpManager = GameObject.Find("xpmanager").GetComponent<xpManager>();
    }

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
    public void switchText()
    {
        switch(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.parent.name)
        {
            case "skill1":
                skillLvlText1.transform.gameObject.SetActive(!skillLvlText1.transform.gameObject.activeInHierarchy);
                skillXPText1.transform.gameObject.SetActive(!skillXPText1.transform.gameObject.activeInHierarchy);
            break;
            case "skill2":
                skillLvlText2.transform.gameObject.SetActive(!skillLvlText2.transform.gameObject.activeInHierarchy);
                skillXPText2.transform.gameObject.SetActive(!skillXPText2.transform.gameObject.activeInHierarchy);
            break;
            case "skill3":
                skillLvlText3.transform.gameObject.SetActive(!skillLvlText3.transform.gameObject.activeInHierarchy);
                skillXPText3.transform.gameObject.SetActive(!skillXPText3.transform.gameObject.activeInHierarchy);
            break;
            case "skill4":
                skillLvlText4.transform.gameObject.SetActive(!skillLvlText4.transform.gameObject.activeInHierarchy);
                skillXPText4.transform.gameObject.SetActive(!skillXPText4.transform.gameObject.activeInHierarchy);
            break;
            case "skill5":
                skillLvlText5.transform.gameObject.SetActive(!skillLvlText5.transform.gameObject.activeInHierarchy);
                skillXPText5.transform.gameObject.SetActive(!skillXPText5.transform.gameObject.activeInHierarchy);
            break;
        }
        AudioManager.instance.Play("UI2");
    }
}
