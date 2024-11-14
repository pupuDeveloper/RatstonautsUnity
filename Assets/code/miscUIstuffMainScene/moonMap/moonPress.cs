using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moonPress : MonoBehaviour
{
    [SerializeField] private moonMapVisuals moonMap;

    public void viewLine()
    {
        moonMap.StartDrawingLine(transform.position);
    }
}
