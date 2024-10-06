using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class calculateXPGainedOffline : MonoBehaviour
{
    [SerializeField] private xpManager _xpManager;


    private void Start()
    {
        Debug.Log("offline time Cockpit: " + calcOfflineTime("cockpit"));
        Debug.Log("offline time Oxygen Garden: " + calcOfflineTime("oxygengarden"));
        Debug.Log("offline time turrets: " + calcOfflineTime("turrets"));
    }
    private int calcOfflineTime(string roomName)
    {
        roomName = roomName.ToLower();
        DateTime startTime;
        DateTime endTime = DateTime.Now;
        int difInSeconds = 0;
        switch (roomName)
        {
            case "cockpit":
                startTime = GameManager.Instance.timeSinceCockPitCDStarted;
                if (GameManager.Instance.triggerCockPitMG < endTime)
                {
                    endTime = GameManager.Instance.triggerCockPitMG;
                }
                difInSeconds = (int)(endTime - startTime).TotalSeconds;
            break;

            case "oxygengarden":
                startTime = GameManager.Instance.timeSinceFoodGenCDStarted;
                if (GameManager.Instance.triggerGardenWatering < endTime)
                {
                    endTime = GameManager.Instance.triggerGardenWatering;
                }
                difInSeconds = (int)(endTime - startTime).TotalSeconds;
            break;

            case "turrets":
                startTime = GameManager.Instance.timeSinceTurretsCDStarted;
                if (GameManager.Instance.triggerTurretsMG < endTime)
                {
                    endTime = GameManager.Instance.triggerTurretsMG;
                }
                difInSeconds = (int)(endTime - startTime).TotalSeconds;
            break;
        }
        return difInSeconds;
    }
}
