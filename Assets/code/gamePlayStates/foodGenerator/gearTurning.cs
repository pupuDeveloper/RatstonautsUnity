using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gearTurning : MonoBehaviour
{
    [SerializeField] private int rotationSpeed;
    [SerializeField] private Vector3 defaultLocation;
    [SerializeField] private bool clockWiseRotation;
    private Quaternion rotationMinusEnd;
    private Quaternion rotationPlusEnd;
    private Quaternion defaultQuaternion;
    private float shakeX;
    private float shakeY;
    private bool lerpBack;
    private float lerpBackSpeed = 1.5f;
    private float lerpForwardSpeed = 2.5f;
    public float timeCount = 0.0f;
    private bool shakeTimerOn;
    private bool constantShakeTimerOn;

    private void Start()
    {
        transform.localPosition = defaultLocation;
        defaultQuaternion = transform.rotation;
        rotationMinusEnd = Quaternion.Euler(0, 0, -10);
        rotationPlusEnd = Quaternion.Euler(0, 0, 10);
        lerpBack = true;
        shakeTimerOn = false;
        constantShakeTimerOn = false;
    }
    public void stuckMotion()
    {
        if (timeCount < 1)
        {
            moveBackForth(clockWiseRotation, lerpBack);
        }
        else
        {
            if (lerpBack == false && shakeTimerOn == false)
            {
                StartCoroutine("stuckShake");
            }
            if (lerpBack)
            {
                timeCount = 0f;
                lerpBack = false;
            }
        }
    }
    private void moveBackForth(bool clockwise, bool backwards)
    {
        if (backwards)
        {
            if (!clockwise)
            {
                transform.rotation = Quaternion.Lerp(rotationMinusEnd, rotationPlusEnd, timeCount * lerpBackSpeed);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(rotationPlusEnd, rotationMinusEnd, timeCount * lerpBackSpeed);
            }
            timeCount = timeCount + Time.deltaTime * lerpBackSpeed;
        }
        else
        {
            if (!clockwise)
            {
                transform.rotation = Quaternion.Lerp(rotationPlusEnd, rotationMinusEnd, timeCount * lerpForwardSpeed);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(rotationMinusEnd, rotationPlusEnd, timeCount * lerpForwardSpeed);
            }
            timeCount = timeCount + Time.deltaTime * lerpForwardSpeed;
        }
    }
    public void turnMotion()
    {
        if (clockWiseRotation)
        {
            this.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else
        {
            this.transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        }

        if (!constantShakeTimerOn)
        {
            StartCoroutine("constantShake");
        }
    }
    private IEnumerator constantShake()
    {
        constantShakeTimerOn = true;
        shakeX = Random.Range((defaultLocation.x - 1f) - 2f, (defaultLocation.x + 1f) + 2f);
        shakeY = Random.Range((defaultLocation.y - 1f) - 2f, (defaultLocation.y + 1f) + 2f);
        transform.localPosition = new Vector3(shakeX, shakeY, 0);
        yield return new WaitForSeconds(0.1f);
        transform.localPosition = defaultLocation;
        constantShakeTimerOn = false;
    }
    private IEnumerator stuckShake()
    {
        shakeTimerOn = true;
        for (int i = 0; i < 10; i++)
        {
            shakeX = Random.Range((defaultLocation.x - 1f) - 5f, (defaultLocation.x + 1f) + 5f);
            shakeY = Random.Range((defaultLocation.y - 1f) - 5f, (defaultLocation.y + 1f) + 5f);
            transform.localPosition = new Vector3(shakeX, shakeY, 0);
            yield return new WaitForSeconds(0.05f);
            transform.localPosition = defaultLocation;
        }
        yield return new WaitForSeconds(1f);
        lerpBack = true;
        shakeTimerOn = false;
        timeCount = 0f;
    }
}
