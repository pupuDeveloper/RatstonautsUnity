using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{

    [SerializeField]
    private TMP_Text timeNowText, savedTimeText;

    //get system time


    private void Awake()
    {
        DateTime timeNow = DateTime.Now;
        timeNowText.text = timeNow.ToString();
    }



    //save & load done later with a saving system
    //
}
