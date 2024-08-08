using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuButtons : MonoBehaviour
{
    public void toGame()
    {
        GameManager.Instance.Go(StateType.InGame);
    }
    public void toOptions()
    {
        GameManager.Instance.Go(StateType.Options);
    }
    public void onQuit()
    {
        Debug.Log("quit! only works in build");
        Application.Quit();
    }
}
