using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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
    [SerializeField] private xpManager _xpManager;
    private int level0boost;
    private int boostAmount;
    private int minSeconds = 7200;
    private int maxSeconds = 28800;

    private void Start()
    {
        asteroidsSpawned = false;
        level0boost = 100;
        GameManager.Instance.onBoostChanged +=
    }
    public void minigame()
    {
        asteroidAmount = UnityEngine.Random.Range(minAsteroidAmount, maxAsteroidAmount);
        for (int i = 0; i < asteroidAmount; i++)
        {
            int whichAsteroid = UnityEngine.Random.Range(0, asteroids.Length);
            xPos = UnityEngine.Random.Range(minX + 0.5f, maxX - 0.5f);
            yPos = UnityEngine.Random.Range(minY + 0.5f, maxY - 0.5f);
            asteroidPos = new Vector3(xPos, yPos, zPos);
            asteroidClones.Add(Instantiate(asteroids[whichAsteroid], asteroidPos, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360))));
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
            gameWonSendData();
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

    private void gameWonSendData()
    {
        calculateBoost();
        _xpManager.turretMGReward();
        GameManager.Instance.timeSinceTurretsCDStarted = DateTime.Now;
        GameManager.Instance.triggerTurretsMG = DateTime.Now.AddSeconds(UnityEngine.Random.Range(minSeconds, maxSeconds));
        GameManager.Instance.turretsBoostOn = true;
        Debug.Log("All asteroids destroyed!");
    }

    private void calculateBoost()
    {
        if (_xpManager.turretsLvl == 0)
        {
            boostAmount = level0boost;
        }
        else
        {
            int boostMultiplier = _xpManager.checkLvls(GameManager.Instance.turretsXP);
            boostAmount = boostMultiplier * level0boost;
        }
    }
    public int getBoost()
    {
        calculateBoost();
        return boostAmount;
    }
}
