using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleLayoutItems : MonoBehaviour
{
    [SerializeField] private ScaleToFitScreen scaleScript;

    private void Awake()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
