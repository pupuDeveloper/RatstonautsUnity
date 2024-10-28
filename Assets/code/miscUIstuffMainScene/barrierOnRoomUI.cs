using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrierOnRoomUI : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = new Vector3((Screen.safeArea.width / Screen.width), (Screen.safeArea.height / Screen.height), 1);
    }
}
