using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

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
}
