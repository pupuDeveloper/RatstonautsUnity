using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

    #region SavedData
    [SerializeField] private float spaceShipSpeed; // km/sec
    [SerializeField] private float totalDistanceTraveled;

    // cockpit data
    [SerializeField] private bool cockpitBoostOn;
    [SerializeField] private int cockPitLevel; // correlates boost amount
    [SerializeField] private DateTime timeSinceCockPitCDStarted; //since last minigame was played
    [SerializeField] private DateTime triggerCockPitMG; // when to trigger next minigame

    // oxygen garden data

    [SerializeField] private int gardenLevel; //this correlates to unlocked plants
    [SerializeField] private int[] plantsInSpots; //plants that are in the spots, invidiual plant object has effect info etc, no need to save it.

    //turrets data

    [SerializeField] private bool turretsBoostOn;
    [SerializeField] private int turretsLevel;
    [SerializeField] private DateTime timeSinceTurretsCDStarted;
    [SerializeField] private DateTime triggerTurretsMG;

    // FoodGenerator Data

    [SerializeField] private int foodGenLevel; //unlocked foods
    [SerializeField] private int selectedFood; // food thats been COOKED
    [SerializeField] private DateTime timeSinceFoodGenCDStarted;
    [SerializeField] private DateTime triggerFoodGenMG;

    //sleepingQuarters data

    [SerializeField] private int quartersLevel; //unlock cosmetics/customization for sleeping quarters

    #endregion

    #region statics
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

    #endregion statics
    

    private List<GameStateBase> _states = new List<GameStateBase>();
    public GameStateBase CurrentState { get; private set;}
    private GameStateBase PreviousState {get; set;}

    public SaveSystem saveSystem { get; private set;}


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

        InitializeStates();
        InitializeSaveSystem();
    }

    private void InitializeSaveSystem()
    {
        saveSystem = new SaveSystem();
    }

    private void InitializeStates()
    {
        GameStateBase initialState = new MainMenuState();
        //create all states
        _states.Add(initialState);
        _states.Add(new InGameState());
        _states.Add(new OptionsState());

#if UNITY_EDITOR
    string activeSceneName = SceneManager.GetActiveScene().name.ToLower();
    foreach (GameStateBase state in _states)
    {
        if (state.SceneName.ToLower() == activeSceneName)
        {
            initialState = state;
            break;
        }
    }
#endif


        CurrentState = initialState;
        CurrentState.Activate();
    }

    public bool Go(StateType targetStateType)
    {
        if (!CurrentState.IsValidTarget(targetStateType))
        {
            Debug.Log($"Transition from {CurrentState.Type} to {targetStateType} is not allowed.");
            return false;
        }

        GameStateBase targetState = GetState(targetStateType);
        if (targetState == null)
        {
            Debug.Log($"Target state {targetStateType} is not found.");
            return false;
        }

        CurrentState.Deactivate();
        PreviousState = CurrentState;
        CurrentState = targetState;
        CurrentState.Activate();
        return true;
    }

    public bool GoBack()
    {
        return Go(PreviousState.Type);
    }

    private GameStateBase GetState(StateType type)
    {
        foreach (GameStateBase state in _states)
        {
            if (state.Type == type)
            {
                return state;
            }
        }
        return null; //null is used here as error value
    }

    public void Save(BinarySaver writer)
    {
        //ship data
        writer.WriteFloat(spaceShipSpeed);
        writer.WriteFloat(totalDistanceTraveled);
        //cockpit data
        writer.WriteBool(cockpitBoostOn);
        writer.WriteInt(cockPitLevel);
        writer.WriteTime(timeSinceCockPitCDStarted);
        writer.WriteTime(triggerCockPitMG);
        //oxygen garden data
        writer.WriteInt(gardenLevel);
        foreach (int id in plantsInSpots)
        {
            writer.WriteInt(id);
        }
        //turrets data
        writer.WriteBool(turretsBoostOn);
        writer.WriteInt(turretsLevel);
        writer.WriteTime(timeSinceTurretsCDStarted);
        writer.WriteTime(triggerTurretsMG);
        //foodGen Data
        writer.WriteInt(foodGenLevel);
        writer.WriteInt(selectedFood);
        writer.WriteTime(timeSinceFoodGenCDStarted);
        writer.WriteTime(triggerFoodGenMG);
        //sleepingquarters data
        writer.WriteInt(quartersLevel);
    }

    public void Load(BinarySaver reader)
    {
        //ship data
        spaceShipSpeed = reader.ReadFloat();
        totalDistanceTraveled = reader.ReadFloat();
        //cockpit data
        cockpitBoostOn = reader.ReadBool();
        cockPitLevel = reader.ReadInt();
        timeSinceCockPitCDStarted = reader.ReadTime();
        triggerCockPitMG = reader.ReadTime();
        //oxygen garden data
        gardenLevel = reader.ReadInt();
        for (int i = 0; i < plantsInSpots.Length; i++) 
        {
            plantsInSpots[i] = reader.ReadInt();
        }
        //turrets data
        turretsBoostOn = reader.ReadBool();
        turretsLevel = reader.ReadInt();
        timeSinceTurretsCDStarted = reader.ReadTime();
        triggerFoodGenMG = reader.ReadTime();
        //sleepingquarters data
        quartersLevel = reader.ReadInt();
    }
}
