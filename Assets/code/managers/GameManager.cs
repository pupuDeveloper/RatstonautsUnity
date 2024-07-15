using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private gameStats _gameStats;
    private GameStateManager gameStateManager; //gameplay state machine
    private static GameManager _instance;
    public bool isShipStopped;
    [SerializeField] private cockpitState _cockPitState;
    [SerializeField] private foodgeneratorState _foodGenState;
    [SerializeField] private oxygengardenState _oxygenGardenState;
    [SerializeField] private sleepingquartersState _sleepingQuartersState;
    [SerializeField] private turretsState _turretsState;
    
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is NULL");
            }
            return _instance;
        }
    }
    
    
    private void Awake()
    {
        // Destroy dublicate instances
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return; // required bcs destroy call is not instant
        }
        DontDestroyOnLoad(gameObject);

        //

        gameStateManager = GameObject.Find("rooms(gameplay)").GetComponent<GameStateManager>(); //fix this so it doesnt break in main menu
        isShipStopped = true;
    }
    private void Start()
    {
        //check scene first
        _gameStats = GameObject.Find("rooms(gameplay)").GetComponent<gameStats>();
    }
    private void Update()
    {
        _gameStats.updateSpeedUI();
        if (_cockPitState.boost == false && _oxygenGardenState.plantsWatered == false) //this checks if spaceship is stopped, i.e none of the minigames are played and their cooldowns are ran out
        {
            isShipStopped = true;
        }
        else
        {
            isShipStopped = false;
        }
    }
}
