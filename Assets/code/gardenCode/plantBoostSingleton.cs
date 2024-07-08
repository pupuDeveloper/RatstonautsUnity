using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantBoostSingleton : MonoBehaviour
{
    public static plantBoostSingleton instance;

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

    public void DandelionEffect()
    {
        
    }
}
