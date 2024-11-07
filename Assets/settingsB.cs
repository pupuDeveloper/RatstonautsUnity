using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsB : MonoBehaviour
{
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject roomBoardButton;
    public void openCloseButtons()
    {
        if (!menuButton.activeSelf && !roomBoardButton.activeSelf)
        {
            menuButton.SetActive(true);
            roomBoardButton.SetActive(true);
        }
        else
        {
            menuButton.SetActive(false);
            roomBoardButton.SetActive(false);
        }
    }
    public void buttonSound1()
    {
        AudioManager.instance.Play("UI1");
    }
}
