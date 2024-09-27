using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameNotif : MonoBehaviour
{
    [SerializeField] private GameObject notif1, notif2, notif3, notif4;

    void Start()
    {
        GameManager.Instance.
    }

    public void checkIfGameplayAvailable()
    {
        if (GameManager.Instance.cockpitBoostOn == false)
        {
            notif1.SetActive(true);
        }
        else
        {
            notif1.SetActive(false);
        }

        if (GameManager.Instance.gardenBoostOn == false)
        {
            notif2.SetActive(true);
        }
        else
        {
            notif2.SetActive(false);
        }

        if (GameManager.Instance.turretsBoostOn == false)
        {
            notif3.SetActive(true);
        }
        else
        {
            notif3.SetActive(false);
        }

        if (GameManager.Instance.foodGenBoostOn == false)
        {
            notif4.SetActive(true);
        }
        else
        {
            notif4.SetActive(false);
        }
    }
}
