using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private gameStats _gameStats;
    private GameStateManager gameStateManager; //gameplay state machine
    private static GameManager _instance;
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
    }
    private void Start()
    {
        //check scene first
        _gameStats = GameObject.Find("rooms(gameplay)").GetComponent<gameStats>();
    }
    private void Update()
    {
        _gameStats.updateSpeedUI();
    }
}
