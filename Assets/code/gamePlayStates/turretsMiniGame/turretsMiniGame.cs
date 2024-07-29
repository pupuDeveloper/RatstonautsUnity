using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class turretsMiniGame : MonoBehaviour
{

    public int minAsteroidAmount;
    public int maxAsteroidAmount;
    public GameObject[] asteroids;
    private List<GameObject> asteroidClones = new List<GameObject>();
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
        asteroidsSpawned = false;
    }
    public void minigame()
    {
        asteroidAmount = Random.Range(minAsteroidAmount, maxAsteroidAmount);
        for (int i = 0; i < asteroidAmount; i++)
        {
            int whichAsteroid = Random.Range(0, asteroids.Length);
            xPos = Random.Range(minX + 0.5f, maxX - 0.5f);
            yPos = Random.Range(minY + 0.5f, maxY - 0.5f);
            asteroidPos = new Vector3(xPos, yPos, zPos);
            asteroidClones.Add(Instantiate(asteroids[whichAsteroid], asteroidPos, Quaternion.Euler(0, 0, Random.Range(0, 360))));
        }
        asteroidsSpawned = true;
    }

    public void checkForAsteroids()
    {
        if (_turretsState.cooldownOn == false && asteroidsSpawned == false)
        {
            minigame();
        }
    }

    void Update()
    {
        if (asteroidAmount == 0 && asteroidsSpawned)
        {
            _turretsState.cooldownOn = true;
            gameWon();
            asteroidsSpawned = false;
        }
    }

    public void resetMiniGame()
    {
        asteroidsSpawned = false;
        foreach (GameObject g in asteroidClones)
        {
            Destroy(g);
        }
    }

    private void gameWon()
    {
        Debug.Log("All asteroids destroyed!");
    }
}
