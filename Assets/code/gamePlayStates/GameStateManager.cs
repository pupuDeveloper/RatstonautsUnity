using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class GameStateManager : MonoBehaviour
{
    public State currentState; //state that is playing
    public State targetState;
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
        nextState = currentState?.RunCurrentState();
        // above line is this 
        /*
        State nextState = null;
        if (currentState != null)
        {
            nextState = currentState.RunCurrentState();
        }
        */
        if (nextState != null)
        {
            SwitchToNextState(nextState);
        }
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
            targetState = _cockpitState;
            break;
            case "oxygenGarden":
            targetState = _oxygengardenState;
            break;
            case "turrets":
            targetState = _turretsState;
            break;
            case "foodGenerator":
            targetState =  _foodgeneratorState;
            break;
            case "sleepingQuarters":
            targetState = _sleepingquartersState;
            break;
            default:
            targetState = null;
            break;
        }
    }
}
