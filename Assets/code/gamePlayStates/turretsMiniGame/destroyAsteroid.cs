using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAsteroid : MonoBehaviour
{
    // add sound effects, asteroid destroying effects etcetc
    private turretsMiniGame _turretsMinigame;
    private float rotationSpeed;
    public float speed;
    private float minSpeed = 0.01f;
    public float targetSpeed;
    private Vector3 direction;
    private bool SlowcoroutineRunning;
    private bool SpeedcoroutineRunning;
    private Animator cannonAnimator;
    private Animator asteroidAnimator;
    private BoxCollider2D boxCollider;
    [SerializeField] GameObject destroyEffect;

    private void Start()
    {
        _turretsMinigame = GameObject.Find("rooms(gameplay)").gameObject.transform.GetChild(2).gameObject.GetComponent<turretsMiniGame>();
        newDirectionAndSpeed();
        speed = targetSpeed;
        SlowcoroutineRunning = false;
        SpeedcoroutineRunning = false;
        cannonAnimator = transform.parent.GetChild(0).GetComponent<Animator>();
        asteroidAnimator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        rotationSpeed = Random.Range(-20f, 20f);
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        if (isTooClose(1f))
        {
            if (SlowcoroutineRunning == false && speed > minSpeed)
            {
                StartCoroutine("slowDown");
            }
        }
        else
        {
            if (speed < targetSpeed && SpeedcoroutineRunning == false)
            {
                StartCoroutine("speedUp");
            }
        }

        if (isTooClose(0.1f) == false)
        {
            move();
        }
        else
        {
            Debug.Log("Called");
            newDirectionAndSpeed();
        }
    }

    private void OnMouseDown()
    {
        boxCollider.enabled = false;
        cannonAnimator.SetTrigger("TappedScreen");
        Instantiate(destroyEffect, transform.position, Quaternion.identity, GameObject.Find("TurretminigameItems").transform);
        _turretsMinigame.asteroidAmount--;
        AudioManager.instance.Play("turretShot");
        GameEvents.current.OnAsteroidDestroyed();
        Destroy(gameObject);
    }

    private void move()
    {
        transform.position += direction * speed * Time.deltaTime;
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
        SlowcoroutineRunning = true;
        yield return new WaitForSeconds(0.05f);
        speed -= 0.2f;
        SlowcoroutineRunning = false;
    }
    private IEnumerator speedUp()
    {
        SpeedcoroutineRunning = true;
        yield return new WaitForSeconds(0.05f);
        speed += 0.2f;
        SpeedcoroutineRunning = false;
    }
    private void newDirectionAndSpeed()
    {
        Debug.Log("direction changed");
        direction.x = Random.Range(-1f, 1f);
        direction.y = Random.Range(-1f, 1f);
        targetSpeed = Random.Range(5f, 20f);
    }
}
