using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xpPopUp : MonoBehaviour
{
    void Update()
    {
        float speed =  Camera.main.orthographicSize / 5f;
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (transform.position.y > Camera.main.orthographicSize * 0.75)
        {
            Destroy(this.gameObject);
        }
    }
}
