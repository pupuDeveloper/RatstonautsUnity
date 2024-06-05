using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class gameStats : MonoBehaviour
{
    public int spaceShipSpeed { get; private set; } //spaceships speed km/per second
    public int distanceTraveled { get; private set;} // total distance traveled
    public TMP_Text shipSpeedText;
    public TMP_Text traveledText;
    public bool calcRunning;


    private void Update()
    {
        if (!calcRunning)
        {
            StartCoroutine("distanceCalculator");
        }
    }
    private void Start()
    {
        calcRunning = false;
    }

    private int getShipSpeed()
    {
        //TODO:get minigame levels to determine speed if none of them are on
        //TODO:get each minigames speedboost

        //this is a placeholder
        //if no games are played and all levels are 1:

        spaceShipSpeed = 10;
        return spaceShipSpeed;

        //TODO:save and load all these values from file
    }

    public void updateSpeedUI()
    {
        int shipSpeed = getShipSpeed();
        shipSpeedText.SetText(shipSpeed + " km/s");
        traveledText.SetText(distanceTraveled + " km traveled");
    }
    private IEnumerator distanceCalculator()
    {
        calcRunning = true;
        int dividedSpeed = spaceShipSpeed / 10;
        distanceTraveled += dividedSpeed;
        yield return new WaitForSeconds(0.1f);
        calcRunning = false;
    }
}
