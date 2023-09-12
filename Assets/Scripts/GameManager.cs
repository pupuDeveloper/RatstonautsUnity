using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager is Null!!!");
            }
            return _instance;
        }
    }

    // How many kilometres spaceship has traveled
    public float kilometresTraveled {get; set;}

    // Speed of spaceship
    public float kilometresPerSecond {get; set;}

    // Tasks active influences kilometresPerSecond 
    public int tasksActive {get; set;}

    private void Awake()
    {
        //TODO: load kilometresTraveled,kilometresPerSecond and tasksActive from saved data

        kilometresTraveled = 0f;
        kilometresPerSecond = 0f;
        tasksActive = 0;

        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        DontDestroyOnLoad(this);
        }
    }
}
