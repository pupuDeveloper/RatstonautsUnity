using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class backbutton : MonoBehaviour
{
    public void back()
    {
        Scene scene = SceneManager.GetActiveScene();
        switch(scene.name)
        {
            case "CockpitMinigame":
            SceneManager.LoadScene("CockPit");
            break;
            case "CannonsMinigame":
            SceneManager.LoadScene("Cannons");
            break;
        }
    }
}
