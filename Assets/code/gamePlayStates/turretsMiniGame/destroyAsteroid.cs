using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAsteroid : MonoBehaviour
{
    // add sound effects, asteroid destroying effects etcetc
    [SerializeField] private turretsMiniGame _turretsMinigame;



    private void OnMouseDown()
    {
        _turretsMinigame.asteroidAmount--;
        Destroy(gameObject);
    }
}
