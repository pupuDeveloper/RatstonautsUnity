using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class slideRooms : MonoBehaviour
{
    public Vector2[] roomPositions = new Vector2[5];
    [SerializeField] private dragDetection _dragDetection;
    [SerializeField] GameStateManager _gameStateManager;
    public Transform allRooms;
    public Vector2 currentPos;
    public Vector2 targetPos;
    private bool checkerOn = false;
    private bool transitionOn;
    private float TimeLerped;
    public float speed;
    private bool overLapping;
    private int roomInt;

    private void Start()
    {
        roomInt = 0;
        currentPos = roomPositions[roomInt];
        targetPos = roomPositions[roomInt];
        checkerOn = false;
        overLapping = false;
    }
    public void slide(dragDetection.DraggedDirection direction, float distance)
    {
        Debug.Log("drag distance is: " + distance);
        Debug.Log("Drag direction is: " + direction);
        if (distance > 50 && direction == dragDetection.DraggedDirection.Left)
        {
            switch (currentPos)
            {
                case var value when value == roomPositions[0]:
                    roomInt = 0;
                    targetPos = roomPositions[1];
                    _gameStateManager.swiped("oxygenGarden");
                    break;
                case var value when value == roomPositions[1]:
                    roomInt = 1;
                    targetPos = roomPositions[2];
                    _gameStateManager.swiped("turrets");
                    break;
                case var value when value == roomPositions[2]:
                    roomInt = 2;
                    targetPos = roomPositions[3];
                    _gameStateManager.swiped("foodGenerator");
                    break;
                case var value when value == roomPositions[3]:
                    roomInt = 3;
                    targetPos = roomPositions[4];
                    _gameStateManager.swiped("sleepingQuarters");
                    break;
                case var value when value == roomPositions[4]:
                    roomInt = 4;
                    targetPos = roomPositions[4]; //cant move more right
                    break;
            }
            if (overLapping && roomInt <= 2)
            {
                currentPos = roomPositions[roomInt + 1];
                targetPos = roomPositions[roomInt + 2];
            }
            StartCoroutine(smoothTransition(targetPos));
        }
        if (distance > 50 && direction == dragDetection.DraggedDirection.Right)
        {
            switch (currentPos)
            {
                case var value when value == roomPositions[4]:
                    roomInt = 4;
                    targetPos = roomPositions[3];
                    _gameStateManager.swiped("foodGenerator");
                    break;
                case var value when value == roomPositions[3]:
                    roomInt = 3;
                    targetPos = roomPositions[2];
                    _gameStateManager.swiped("turrets");
                    break;
                case var value when value == roomPositions[2]:
                    roomInt = 2;
                    targetPos = roomPositions[1];
                    _gameStateManager.swiped("oxygenGarden");
                    break;
                case var value when value == roomPositions[1]:
                    roomInt = 1;
                    targetPos = roomPositions[0];
                    _gameStateManager.swiped("cockPit");
                    break;
                case var value when value == roomPositions[0]:
                    roomInt = 0;
                    targetPos = roomPositions[0]; //cant move more left
                    break;
            }
            if (overLapping && roomInt >= 2)
            {
                currentPos = roomPositions[roomInt - 1];
                targetPos = roomPositions[roomInt - 2];
            }
            StartCoroutine(smoothTransition(targetPos));
        }
    }
    private void checker()
    {
        allRooms.localPosition = targetPos;
    }

    public IEnumerator smoothTransition(Vector2 endPoint)
    {
        targetPos = endPoint;
        overLapping = true;
        speed = 8f;
        TimeLerped = 0;
        while(TimeLerped < 1)
        {
            allRooms.localPosition = Vector3.Lerp(currentPos, endPoint, TimeLerped);
            if (TimeLerped < 0.6f)
            {
                TimeLerped += Time.deltaTime * speed;
            }
            else
            {
                if (speed > 0.85f)
                {
                    speed *= 0.75f;
                }
                TimeLerped += Time.deltaTime * speed;
            }
            Debug.Log("lerping");
            yield return null;
        }
        overLapping = false;
        allRooms.localPosition = endPoint;
        currentPos = targetPos;
    }
}

