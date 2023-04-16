using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class minigameButton : MonoBehaviour
{

    public void toMinigame()
    {
        SceneManager.LoadScene("CockpitMinigame");
    }
}
