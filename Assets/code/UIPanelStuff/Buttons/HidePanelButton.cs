using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanelButton : MonoBehaviour
{
    private PanelsManager _panelsManager; // cached panels manager

    private void Start()
    {
        _panelsManager = PanelsManager.instance;
    }

    public void DoHidePanel()
    {
        _panelsManager.HideLastPanel();
    }
}

