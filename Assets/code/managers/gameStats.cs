using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class gameStats : MonoBehaviour
{
    public int spaceShipSpeed { get; private set; } //spaceships speed km/per second
    public int distanceTraveled { get; private set;} // total distance traveled
    [SerializeField] private cockpitMiniGame _cockpitMinigame;
    private bool booooooooost;
    private bool speedBoost; //when all available minigames are done add boostMultiplier to shipspeed
    private int boostMultiplier;
    public TMP_Text shipSpeedText;
    public TMP_Text traveledText;
    public bool calcRunning;

    public bool testingBool; //testing;


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
        testingBool = true;
    }

    private int getShipSpeed()
    {
        booooooooost = _cockpitMinigame.checkBoost();
        //TODO:get minigame levels to determine speed if none of them are on
        //TODO:get each minigames speedboost

        //this is a placeholder
        //if no games are played and all levels are 1:
    
        if (testingBool)
        {
            spaceShipSpeed = 10;
        }
        if (booooooooost)
        {
            spaceShipSpeed += _cockpitMinigame.cockpitBoost();
        }
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
