using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleLayoutItems : MonoBehaviour
{
    [SerializeField] private ScaleToFitScreen scaleScript;

    private void Start()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x * scaleScript.getScaleMultiplierX(), gameObject.GetComponent<RectTransform>().sizeDelta.y * scaleScript.getScaleMultiplierY());
    }
}
