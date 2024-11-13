using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuButtons : MonoBehaviour
{
    public void buttonSound1()
    {
        AudioManager.instance.Play("UI1");
    }
    public void buttonSound2()
    {
        AudioManager.instance.Play("UI2");
    }
    public void toGame()
    {
        GameManager.Instance.Go(StateType.InGame);
    }
    public void onQuit()
    {
        Debug.Log("quit! only works in build");
        Application.Quit();
    }
}
