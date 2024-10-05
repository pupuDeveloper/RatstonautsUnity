using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

    #region SavedData
    [SerializeField] public int spaceShipSpeed {get; set;} // km/sec
    [SerializeField] public int totalDistanceTraveled {get; set;}
    [SerializeField] public int totalXp;
    public event Action roomBoostOn;

    // cockpit data
    private bool _cockpitBoostOn;
    public bool cockpitBoostOn
    {
        get
        {
            return _cockpitBoostOn;
        }
        set
        {
            if (_cockpitBoostOn != value)
            {
                _cockpitBoostOn = value;
                roomBoostOn?.Invoke();
            }
        }
    }
    [SerializeField] public int cockPitXP { get; set; } // correlates boost amount
    [SerializeField] public DateTime timeSinceCockPitCDStarted { get; set; } //since last minigame was played
    [SerializeField] public DateTime triggerCockPitMG { get; set; } // when to trigger next minigame

    // oxygen garden data
    private bool _gardenBoostOn;
    public bool gardenBoostOn
    {
        get
        {
            return _gardenBoostOn;
        }
        set
        {
            if (_gardenBoostOn != value)
            {
                _gardenBoostOn = value;
                roomBoostOn?.Invoke();
            }
        }
    }
    [SerializeField] public int gardenXP { get; set; } //this correlates to unlocked plants
    [SerializeField] public int[] plantsInSpots; //plants that are in the spots, invidiual plant object has effect info etc, no need to save it.
    [SerializeField] public DateTime timeSinceGardenCDStarted { get; set; }
    [SerializeField] public DateTime triggerGardenWatering { get; set; }

    //turrets data
    private bool _turretsBoostOn;
    public bool turretsBoostOn
    {
        get
        {
            return _turretsBoostOn;
        }
        set
        {
            if (_turretsBoostOn != value)
            {
                _turretsBoostOn = value;
                roomBoostOn?.Invoke();
            }
        }
    }
    [SerializeField] public int turretsXP { get; set; }
    [SerializeField] public DateTime timeSinceTurretsCDStarted { get; set; }
    [SerializeField] public DateTime triggerTurretsMG { get; set; }

    // FoodGenerator Data
    [SerializeField] public bool foodGenBoostOn { get; set; }
    [SerializeField] public int foodGenXP { get; private set; } //unlocked foods
    [SerializeField] private int selectedFood; // food thats been COOKED
    [SerializeField] private DateTime timeSinceFoodGenCDStarted;
    [SerializeField] public DateTime triggerFoodGenMG;

    //sleepingQuarters data

    [SerializeField] public int quartersXP { get; private set; } //unlock cosmetics/customization for sleeping quarters

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
    public GameStateBase CurrentState { get; private set; }
    private GameStateBase PreviousState { get; set; }
    public SaveSystem saveSystem { get; private set; }
    public bool unlockEverything { get; private set; } // for developing purposes, unlocks progression form start for testing/debugging etc.



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

    private void Start()
    {
        StartCoroutine("autosaveWithTimer");
        string mainSaveSlot = saveSystem.MainSaveSlot;
        saveSystem.Load(mainSaveSlot);
        if (CurrentState.Type == StateType.Initialization && totalDistanceTraveled == 0)
        {
            Go(StateType.MainMenu);
        }
        else
        {
            Go(StateType.InGame);
        }
    }

    private void InitializeSaveSystem()
    {
        saveSystem = new SaveSystem();
    }

    private void InitializeStates()
    {
        GameStateBase initialState = new InitState();
        //create all states
        _states.Add(initialState);
        _states.Add(new MainMenuState());
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
    public int getTotalXP()
    {
        totalXp = cockPitXP + gardenXP + turretsXP + quartersXP + foodGenXP;
        return totalXp;
    }

    public void Save(BinarySaver writer)
    {
        //ship data
        writer.WriteInt(spaceShipSpeed);
        writer.WriteInt(totalDistanceTraveled);
        writer.WriteInt(totalXp);
        //cockpit data
        writer.WriteBool(cockpitBoostOn);
        writer.WriteInt(cockPitXP);
        writer.WriteTime(timeSinceCockPitCDStarted);
        writer.WriteTime(triggerCockPitMG);
        //oxygen garden data
        writer.WriteBool(gardenBoostOn);
        writer.WriteInt(gardenXP);
        foreach (int id in plantsInSpots)
        {
            writer.WriteInt(id);
        }
        writer.WriteTime(timeSinceGardenCDStarted);
        writer.WriteTime(triggerGardenWatering);
        //turrets data
        writer.WriteBool(turretsBoostOn);
        writer.WriteInt(turretsXP);
        Debug.Log("saved turret xp: " + turretsXP);
        writer.WriteTime(timeSinceTurretsCDStarted);
        writer.WriteTime(triggerTurretsMG);
        //foodGen Data
        //writer.WriteBool(foodGenBoostOn);
        //writer.WriteInt(foodGenXP);
        //writer.WriteInt(selectedFood);
        //writer.WriteTime(timeSinceFoodGenCDStarted);
        //writer.WriteTime(triggerFoodGenMG);
        //sleepingquarters data
        //writer.WriteInt(quartersXP);
    }

    public void Load(BinarySaver reader)
    {
        //ship data
        spaceShipSpeed = reader.ReadInt();
        totalDistanceTraveled = reader.ReadInt();
        totalXp = reader.ReadInt();
        //cockpit data
        cockpitBoostOn = reader.ReadBool();
        cockPitXP = reader.ReadInt();
        timeSinceCockPitCDStarted = reader.ReadTime();
        triggerCockPitMG = reader.ReadTime();
        //oxygen garden data
        gardenBoostOn = reader.ReadBool();
        gardenXP = reader.ReadInt();
        if (plantsInSpots == null || plantsInSpots.Length == 0)
        {
            plantsInSpots = new int[3]
        }
        for (int i = 0; i < plantsInSpots.Length; i++)
        {
            plantsInSpots[i] = reader.ReadInt();
        }
        timeSinceGardenCDStarted = reader.ReadTime();
        triggerGardenWatering = reader.ReadTime();
        //turrets data
        turretsBoostOn = reader.ReadBool();
        turretsXP = reader.ReadInt();
        timeSinceTurretsCDStarted = reader.ReadTime();
        triggerTurretsMG = reader.ReadTime();
        //foodGen Data
        //foodGenBoostOn = reader.ReadBool();
        //foodGenXP = reader.ReadInt();
        //selectedFood = reader.ReadInt();
        //timeSinceFoodGenCDStarted = reader.ReadTime();
        //triggerFoodGenMG = reader.ReadTime();
        //sleepingquarters data
        //quartersXP = reader.ReadInt();
    }
    private IEnumerator autosaveWithTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            string mainSaveSlot = saveSystem.MainSaveSlot;
            saveSystem.Save(mainSaveSlot);
        }
    }

    void OnApplicationFocus() //commented out bcs testing in editor
    {
        //string mainSaveSlot = saveSystem.MainSaveSlot;
        //saveSystem.Save(mainSaveSlot);
    }

    void OnApplicationQuit()
    {
        string mainSaveSlot = saveSystem.MainSaveSlot;
        saveSystem.Save(mainSaveSlot);
    }

    public int getTimeSinceLastSave(DateTime referencedTime) //get time between requested timedate and current timedate in seconds.
    {
        var timeDif = DateTime.Now - referencedTime;
        int differenceInSeconds = timeDif.Seconds;
        return differenceInSeconds;
    }
    private void checkForNullTimes()
    {
        if (timeSinceCockPitCDStarted < new DateTime(2000,01,01)) timeSinceCockPitCDStarted = DateTime.Now;
        if (timeSinceGardenCDStarted < new DateTime(2000,01,01)) timeSinceGardenCDStarted = DateTime.Now;
        if (timeSinceTurretsCDStarted < new DateTime(2000,01,01)) timeSinceTurretsCDStarted = DateTime.Now;
        if (triggerCockPitMG < new DateTime(2000,01,01)) triggerCockPitMG = DateTime.Now;
        if (triggerGardenWatering < new DateTime(2000,01,01)) triggerGardenWatering = DateTime.Now;
        if (triggerTurretsMG < new DateTime(2000,01,01)) triggerTurretsMG = DateTime.Now;
        //timeSinceFoodGenCDStarted = (null) ? timeSinceFoodGenCDStarted = DateTime.Now:;
        //triggerFoodGenMG = (null) ? triggerFoodGenMG = DateTime.Now:;
    }
}
