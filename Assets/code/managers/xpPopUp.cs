using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xpPopUp : MonoBehaviour
{
    private float speed = 1.75f;
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (transform.position.y > Camera.main.orthographicSize/2 + 4f)
        {
            Destroy(this.gameObject);
        }
    }
}
