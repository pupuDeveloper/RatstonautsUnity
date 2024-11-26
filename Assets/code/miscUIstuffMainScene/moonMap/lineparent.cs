using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineparent : MonoBehaviour
{
    
    void Start()
    {
        transform.SetParent(GameObject.Find("moonMap/mask/mapImage").transform);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
}
