using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class turretsMiniGame : MonoBehaviour
{
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject asteroid4;
    public GameObject asteroid5;
    public GameObject asteroid6;
    public GameObject asteroid7;
    public GameObject asteroid8;
    public GameObject asteroid9;
    public GameObject asteroid10;
    public GameObject asteroid11;
    public GameObject asteroid12;


    public int minAsteroidAmount;
    public int maxAsteroidAmount;
    private GameObject[] asteroids;
    public float maxY;
    public float minY;
    public float maxX;
    public float minX;
    private float xPos;
    private float yPos;
    public float zPos;
    private Vector3 asteroidPos;
    public int asteroidAmount;
    public turretsState _turretsState;
    private bool asteroidsSpawned;



    private void Start()
    {
        asteroids[0] = asteroid1;
        asteroids[1] = asteroid2;
        asteroidsSpawned = false;
    }
    public void minigame()
    {
        asteroidAmount = Random.Range(minAsteroidAmount, maxAsteroidAmount);
        for (int i = 0; i < asteroidAmount; i++)
        {
            int whichAsteroid = Random.Range(0, asteroids.Length);
            xPos = Random.Range(minX, maxX);
            yPos = Random.Range(minY, maxY);
            asteroidPos = new Vector3(xPos, yPos, zPos);
            Instantiate(asteroids[whichAsteroid], asteroidPos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
        asteroidsSpawned = true;
    }

    void Update()
    {
        if (_turretsState.cooldownOn == false && asteroidsSpawned == false)
        {
            minigame();
        }

        if (asteroidAmount == 0)
        {
            _turretsState.cooldownOn = true;
        }
    }



    private void gameWon()
    {
        Debug.Log("All asteroids destroyed!");
    }
}
