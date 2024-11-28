using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantListDescButtons : MonoBehaviour
{

    private bool myBool = true;
    [SerializeField] GameObject o1;
    [SerializeField] GameObject o2;
    [SerializeField] GameObject o3;
    [SerializeField] GameObject o4;

    public void toggleItems()
    {
        if (myBool)
        {
            o1.SetActive(false);
            o2.SetActive(false);
            o3.SetActive(false);
            o4.SetActive(true);
        }
        else
        {
            o1.SetActive(true);
            o2.SetActive(true);
            o3.SetActive(true);
            o4.SetActive(false);
        }
        myBool = !myBool;
    }
}
