using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anchorChildObjects : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToAnchor;

    public void anchorObjects(bool onOrOff)
    {
        if (onOrOff)
        {
            foreach (var obj in objectsToAnchor)
            {
                obj.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            foreach (var obj in objectsToAnchor)
            {
                obj.GetComponent<AnchorGameObject>().UpdateAnchor();
                obj.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
