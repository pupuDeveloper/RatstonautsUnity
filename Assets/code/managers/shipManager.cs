using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shipManager : MonoBehaviour
{

    //TODO: MAKE SURE DATA HERE IS SAVED WHEN EXITING/ENTERING FROM MAIN GAMEPLAY SCENE TO OTHER SCENES, OR APP IS CLOSED!!!!! TODO:
    private gameStats _gameStats;
    private GameStateManager gameStateManager; //gameplay state machine
    public bool isShipStopped;
    [SerializeField] private cockpitState _cockPitState;
    [SerializeField] private foodgeneratorState _foodGenState;
    [SerializeField] private oxygengardenState _oxygenGardenState;
    [SerializeField] private sleepingquartersState _sleepingQuartersState;
    [SerializeField] private turretsState _turretsState;

    private void Awake()
    {
        gameStateManager = GameObject.Find("rooms(gameplay)").GetComponent<GameStateManager>(); //fix this so it doesnt break in main menu
        isShipStopped = true;
    }

    private void Update()
    {
        if (GameManager.Instance.cockpitBoostOn == false && _oxygenGardenState.arePlantsWatered() == false && _oxygenGardenState.areAllPlantsBlank() == true) //this checks if spaceship is stopped, i.e none of the minigames are played and their cooldowns are ran out
        {
            isShipStopped = true;
        }
        else
        {
            isShipStopped = false;
        }
    }
}
