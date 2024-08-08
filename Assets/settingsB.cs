using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsB : MonoBehaviour
{
    public void toMenu()
    {
        GameManager.Instance.Go(StateType.Options);
    }
}
