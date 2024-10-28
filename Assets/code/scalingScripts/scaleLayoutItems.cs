using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scaleLayoutItems : MonoBehaviour
{
    public void Scale(float preferredX, float preferredY)
    {
        gameObject.GetComponent<LayoutElement>().preferredWidth = preferredX;
        gameObject.GetComponent<LayoutElement>().preferredHeight = preferredY;
    }
}