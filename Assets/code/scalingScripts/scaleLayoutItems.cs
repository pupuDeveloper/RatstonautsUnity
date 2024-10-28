using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scaleLayoutItems : MonoBehaviour
{
    [SerializeField] private ScaleToFitScreen scaleScript;

    private void Start()
    {
        gameObject.GetComponent<LayoutElement>().preferredWidth = scaleScript.getX();
        gameObject.GetComponent<LayoutElement>().preferredHeight = scaleScript.getY();
    }
}