using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonActions : MonoBehaviour
{

    public void toCentralRoom()
    {
        SceneManager.LoadScene("CentralRoom");
    }
    public void toCockPit()
    {
        SceneManager.LoadScene("CockPit");
    }
    public void toOxygenGenerator()
    {
        SceneManager.LoadScene("OxygenGenerator");
    }
    public void toCannons()
    {
        SceneManager.LoadScene("Cannons");
    }
    public void toKitchen()
    {
        SceneManager.LoadScene("Kitchen");
    }
    public void toSleepingQuarters()
    {
        SceneManager.LoadScene("SleepingQuarters");
    }

}
