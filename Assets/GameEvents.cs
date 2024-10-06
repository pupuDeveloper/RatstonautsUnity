using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    private static GameEvents _current;
    public static GameEvents current
    {
        get
        {
            if (_current == null)
            {
                Debug.LogError("eventsystem is NULL");
            }
            return _current;
        }
    }

    private void Awake()
    {
        if (_current == null)
        {
            _current = this;
        }
        else if (_current != this)
        {
            Destroy(gameObject);
            return; // required bcs destroy call is not instant
        }
        DontDestroyOnLoad(gameObject);
    }
    public event Action onAsteroidDestroyed;
    public void OnAsteroidDestroyed()
    {
        if (onAsteroidDestroyed != null)
        {
            onAsteroidDestroyed();
        }
    }
}
