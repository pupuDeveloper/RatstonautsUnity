using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class GameStateManager : MonoBehaviour
{
    public State currentState; //state that is playing
    public State nextState;
    public cockpitState _cockpitState;
    public foodgeneratorState _foodgeneratorState;
    public oxygengardenState _oxygengardenState;
    public sleepingquartersState _sleepingquartersState;
    public turretsState _turretsState;
    void Update()
    {
        RunStateMachine();
    }
    private void RunStateMachine()
    {

        if (nextState != null)
        {
            SwitchToNextState(nextState);
        }
        nextState = currentState?.RunCurrentState(); //if variable is not null run current state

        // above line is this 
        /*
        State nextState = null;
        if (currentState != null)
        {
            nextState = currentState.RunCurrentState();
        }
        */
    }
    private void SwitchToNextState(State nextState)
    {
        currentState = nextState;
    }
    public void roomButtonPressed(string stateName)
    {
        switch (stateName)
        {
            case "cockPit":
            nextState = _cockpitState;
            break;
            case "oxygenGarden":
            nextState = _oxygengardenState;
            break;
            case "turrets":
            nextState = _turretsState;
            break;
            case "foodGenerator":
            nextState =  _foodgeneratorState;
            break;
            case "sleepingQuarters":
            nextState = _sleepingquartersState;
            break;
            default:
            Debug.LogError("state not found!");
            break;
        }
        Debug.Log("got here");
    }
}
