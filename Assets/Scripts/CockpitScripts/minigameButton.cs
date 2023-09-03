using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class minigameButton : MonoBehaviour
{

    public void toMinigame()
    {
        Scene scene = SceneManager.GetActiveScene();
        switch(scene.name)
        {
            case "CockPit":
            SceneManager.LoadScene("CockpitMinigame");
            break;
            case "Cannons":
            SceneManager.LoadScene("CannonsMinigame");
            break;
        }
    }
}
