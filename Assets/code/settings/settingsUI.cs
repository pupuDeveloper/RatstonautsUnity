using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsUI : MonoBehaviour
{

    void Start()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
    }
    public void buttonSound1()
    {
        AudioManager.instance.Play("UI1");
    }
    public void buttonSound2()
    {
        AudioManager.instance.Play("UI2");
    }
    public void resetGame()//only use while testing
    {
        GameManager.Instance.resetSaveData();
    }
    public void showPopups()
    {
        GameManager.Instance.showPopups = !GameManager.Instance.showPopups;
    }
    public void muteSFX()
    {
        foreach (Sound s in AudioManager.instance.sounds)
        {
            if (s.type == Sound.Type.sfx)
            {
                if (s.source.volume == 0f)
                {
                    s.source.volume = 1f;
                }
                else
                {
                    s.source.volume = 0f;
                }
            }
        }
    }

    public void muteMusic()
    {
        foreach (Sound s in AudioManager.instance.sounds)
        {
            if (s.type == Sound.Type.music)
            {
                if (s.source.volume == 0f)
                {
                    s.source.volume = 1f;
                }
                else
                {
                    s.source.volume = 0f;
                }
            }
        }
    }
}
