using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantBoostSingleton : MonoBehaviour
{
    public static plantBoostSingleton instance;
    [SerializeField] gardenManager _gardenManager;
    public bool boostShipSpeed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void getEffect()
    {
        foreach (Plant p in _gardenManager.plantsInSpots)
        {
            switch (p.name)
            {
                case "Dandelion":
                boostShipSpeed = true;
                break;
            }
        }
    }

    public float plantEffectsOnShipSpeed() //rn can't rly think any other way to make all effects except each in its own function.
    {
        float multiplier = 1f;
        foreach (Plant p in _gardenManager.plantsInSpots)
        {
            switch (p.name)
            {
                case "Dandelion":
                multiplier += 0.05f;
                break;
            }
        }
        return multiplier;
    }
}
