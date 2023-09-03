using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    public GameObject asteroidPrefab1;
    public GameObject asteroidPrefab2;
    public bool asteroidsCleared = false;

    void Update()
    {
        if (asteroidsCleared == false)
        {
            Debug.Log("Clear all the asteroids by shooting them!");
            int index = Random.Range(9, 16);
            for (int i = 0; i < index; i++)
            {
                Vector2 randomSpawnPosition = new Vector2(Random.Range(-2.3f,2.3f), Random.Range(-3.1f,2.5f));
                if (Random.Range(0, 2) == 0)
                {
                    Instantiate(asteroidPrefab1, randomSpawnPosition, Quaternion.identity);
                }
                else
                {
                    Instantiate(asteroidPrefab2, randomSpawnPosition, Quaternion.identity);
                }
            }
            asteroidsCleared = true;
        }
    }
}
