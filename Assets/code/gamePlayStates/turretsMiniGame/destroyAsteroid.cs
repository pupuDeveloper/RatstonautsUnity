using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAsteroid : MonoBehaviour
{
    // add sound effects, asteroid destroying effects etcetc
    private turretsMiniGame _turretsMinigame;
    public float speed;
    private Vector3 direction;
    private bool coroutineRunning;


    private void Start()
    {
        _turretsMinigame = GameObject.Find("rooms(gameplay)").gameObject.transform.GetChild(2).gameObject.GetComponent<turretsMiniGame>();
        newDirectionAndSpeed();
        coroutineRunning = false;
    }

    private void Update()
    {
        if (isTooClose(1f))
        {
            if (coroutineRunning == false)
            {
                Debug.Log("heyy");
                StartCoroutine("slowDown");
            }
        }

        if (isTooClose(0f) == false)
        {
            move();
        }
        else
        {
            Debug.Log("Called");
            newDirectionAndSpeed();
        }

        if (speed < 0f)
        {
            newDirectionAndSpeed();
        }
    }

    private void OnMouseDown()
    {
        _turretsMinigame.asteroidAmount--;
        Destroy(gameObject);
    }
    private void move()
    {
        transform.position += direction * speed;
    }
    private bool isTooClose(float limit)
    {
        float distanceX;
        float distanceY;
        //check if direction x and y are positive or negative and compare to correct sides
        if (direction.x > 0)
        {
            distanceX = _turretsMinigame.maxX - transform.position.x;
        }
        else
        {
            distanceX = _turretsMinigame.minX - transform.position.x;
        }

        if (direction.y > 0)
        {
            distanceY = _turretsMinigame.maxY - transform.position.y;
        }
        else
        {
            distanceY = _turretsMinigame.minY - transform.position.y;
        }

        if (Mathf.Abs(distanceX) < limit || Mathf.Abs(distanceY) < limit)
        {
            return true; // Over Limit
        }
        return false; // Under Limit
    }
    private IEnumerator slowDown()
    {
        coroutineRunning = true;
        yield return new WaitForSeconds(0.05f);
        speed -= 0.0001f;
        coroutineRunning = false;
    }
    private void newDirectionAndSpeed()
    {
        direction.x = Random.Range(-1f, 1f);
        direction.y = Random.Range(-1f, 1f);
        speed = Random.Range(0.001f, 0.005f);
    }
}
