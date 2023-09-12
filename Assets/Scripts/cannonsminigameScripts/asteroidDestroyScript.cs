using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidDestroyScript : MonoBehaviour
{

    void OnMouseDown()
    {
        Destroy(this.gameObject);
    }
}
