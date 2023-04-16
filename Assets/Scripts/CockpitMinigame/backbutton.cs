using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class backbutton : MonoBehaviour
{
    public void back()
    {
        SceneManager.LoadScene("CockPit");
    }
}
