using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsB : MonoBehaviour
{
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject roomBoardButton;
    [SerializeField] private GameObject roomBoard;
    public void toMenu()
    {
        GameManager.Instance.Go(StateType.Options);
    }
    public void openCloseButtons()
    {
        if (!menuButton.activeSelf && !roomBoardButton.activeSelf && !roomBoard.activeSelf)
        {
            menuButton.SetActive(true);
            roomBoardButton.SetActive(true);
        }
        else
        {
            menuButton.SetActive(false);
            roomBoardButton.SetActive(false);
        }
        if (roomBoard.activeSelf)
        {
            roomBoard.SetActive(false);
        }
    }
    public void buttonSound1()
    {
        AudioManager.instance.Play("UI1");
    }
}
