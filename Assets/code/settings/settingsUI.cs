using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsUI : MonoBehaviour
{
    public void goBack()
    {
        GameManager.Instance.GoBack();
    }
}
