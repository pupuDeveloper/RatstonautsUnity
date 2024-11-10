using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyParticles : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
}
