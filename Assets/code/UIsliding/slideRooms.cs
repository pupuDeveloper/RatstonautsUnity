using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slideRooms : MonoBehaviour
{
    [SerializeField] private Vector2[] roomPositions = new Vector2[5];
    public Transform allRooms;
    public Vector2 currentPos;
    public Vector2 targetPos;

    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.15f;
    public bool adjusting;
    public bool moving;

    private void Start()
    {
        currentPos = roomPositions[0];
        targetPos = roomPositions[0];
        moving = false;
    }

    private void Update()
    {
        if (currentPos == targetPos || adjusting == false)
        {
            detectMove(allRooms.InverseTransformPoint(transform.position).x);
        }
        smoothTransition(targetPos);
    }

    public void detectMove(float pos)
    {
        if (pos - 50 > Mathf.Abs(currentPos.x))
        {
            moving = true;
            switch (currentPos)
            {
                case var value when value == roomPositions[0]:
                    targetPos = roomPositions[1];
                    break;
                case var value when value == roomPositions[1]:
                    targetPos = roomPositions[2];
                    break;
                case var value when value == roomPositions[2]:
                    targetPos = roomPositions[3];
                    break;
                case var value when value == roomPositions[3]:
                    targetPos = roomPositions[4];
                    break;
                case var value when value == roomPositions[4]:
                    targetPos = roomPositions[4]; //cant move more right
                    break;
            }
        }
        if (pos + 50 < Mathf.Abs(currentPos.x))
        {
            moving = true;
            switch (currentPos)
            {
                case var value when value == roomPositions[4]:
                    targetPos = roomPositions[3];
                    break;
                case var value when value == roomPositions[3]:
                    targetPos = roomPositions[2];
                    break;
                case var value when value == roomPositions[2]:
                    targetPos = roomPositions[1];
                    break;
                case var value when value == roomPositions[1]:
                    targetPos = roomPositions[0];
                    break;
                case var value when value == roomPositions[0]:
                    targetPos = roomPositions[0]; //cant move more left
                    break;
            }
        }
    }

    private void smoothTransition(Vector2 targetPos)
    {
        allRooms.localPosition = Vector3.SmoothDamp(allRooms.localPosition, targetPos, ref velocity, smoothTime);
        float difference = getDifference();

        if (difference < 150 && difference > -150)
        {
            currentPos = targetPos;
            moving = false;
            adjusting = true;
            adjust(difference);
        }
    }
    private void adjust(float difference)
    {
        if (difference < 5)
        {
            adjusting = false;
            allRooms.localPosition = currentPos;
            currentPos = targetPos;
        }
    }
    private float getDifference()
    {
        return Mathf.Abs(targetPos.x) - Mathf.Abs(allRooms.localPosition.x);
    }
}

