using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsB : MonoBehaviour
{
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject roomBoardButton;
    [SerializeField] private GameObject moonMapButton;
    public void openCloseButtons()
    {
        if (!menuButton.activeSelf && !roomBoardButton.activeSelf)
        {
            menuButton.SetActive(true);
            roomBoardButton.SetActive(true);
            moonMapButton.SetActive(true);
        }
        else
        {
            menuButton.SetActive(false);
            roomBoardButton.SetActive(false);
            moonMapButton.SetActive(false);
        }
    }
    public void buttonSound1()
    {
        AudioManager.instance.Play("UI1");
    }
}
