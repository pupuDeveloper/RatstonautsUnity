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
    [SerializeField] slideRooms _slideRooms;
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
            StartCoroutine(_slideRooms.smoothTransition(_slideRooms.roomPositions[0], 0));
            break;
            case "oxygenGarden":
            targetState = _oxygengardenState;
            StartCoroutine(_slideRooms.smoothTransition(_slideRooms.roomPositions[1], 1));
            break;
            case "turrets":
            targetState = _turretsState;
            StartCoroutine(_slideRooms.smoothTransition(_slideRooms.roomPositions[2], 2));
            break;
            case "foodGenerator":
            targetState =  _foodgeneratorState;
            StartCoroutine(_slideRooms.smoothTransition(_slideRooms.roomPositions[3], 3));
            break;
            case "sleepingQuarters":
            targetState = _sleepingquartersState;
            StartCoroutine(_slideRooms.smoothTransition(_slideRooms.roomPositions[4], 4));
            break;
            default:
            targetState = null;
            break;
        }
    }
    public void swiped(string stateName)
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
