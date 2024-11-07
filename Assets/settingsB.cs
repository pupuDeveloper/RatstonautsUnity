using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsB : MonoBehaviour
{
    public void buttonSound1()
    {
        AudioManager.instance.Play("UI1");
    }
}
