using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantBoostSingleton : MonoBehaviour
{
    public static plantBoostSingleton instance;
    [SerializeField] gardenManager _gardenManager;
    

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

    public List getActivePlants()
    {
        return _gardenManager.plantsInSpots;
    }

    public void DandelionEffect() //rn can't rly think any other way to make all effects except each in its own function.
    {
        
    }
}
