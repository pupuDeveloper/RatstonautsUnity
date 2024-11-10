using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class cockpitMiniGame : MonoBehaviour
{
    #region sprites
    private Sprite btn1sprite1;
    private Sprite btn1sprite2;
    private Sprite btn2sprite1;
    private Sprite btn2sprite2;
    private Sprite btn3sprite1;
    private Sprite btn3sprite2;
    private Sprite btn4sprite1;
    private Sprite btn4sprite2;
    private Sprite btn5sprite1;
    private Sprite btn5sprite2;
    private Sprite btn6sprite1;
    private Sprite btn6sprite2;
    #endregion
    private int minigameCD; //in seconds
    private int level0boost;
    private int boostMultiplier;
    private static bool isGameDone;
    private int boostAmount;
    [SerializeField] private xpManager _xpManager;
    //buttons n other UI stuff
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;
    [SerializeField] private Button button4;
    [SerializeField] private Button button5;
    [SerializeField] private Button button6;
    [SerializeField] private GameObject clickBlocker;
    private int minSeconds;
    private int maxSeconds;

    //
    private string correctOrder;
    private string playerAttempt;
    private bool playerTurn;
    private bool coroutineOn;

    private void Start()
    {
        //read is boost on etc from file
        isGameDone = GameManager.Instance.cockpitBoostOn;
        playerTurn = false;
        coroutineOn = false;
        clickBlocker.SetActive(true);
        level0boost = 100; //xp is 10x in code
        minSeconds = 7200;
        maxSeconds = 28800;
        btn1sprite1 = Resources.Load<Sprite>("cockpitButtons/GameButton1");
        btn1sprite2 = Resources.Load<Sprite>("cockpitButtons/GameButton2");
        btn2sprite1 = Resources.Load<Sprite>("cockpitButtons/GameButton3");
        btn2sprite2 = Resources.Load<Sprite>("cockpitButtons/GameButton4");
        btn3sprite1 = Resources.Load<Sprite>("cockpitButtons/GameButton5");
        btn3sprite2 = Resources.Load<Sprite>("cockpitButtons/GameButton6");
        btn4sprite1 = Resources.Load<Sprite>("cockpitButtons/GameButton7");
        btn4sprite2 = Resources.Load<Sprite>("cockpitButtons/GameButton8");
        btn5sprite1 = Resources.Load<Sprite>("cockpitButtons/GameButton9");
        btn5sprite2 = Resources.Load<Sprite>("cockpitButtons/GameButton10");
        btn6sprite1 = Resources.Load<Sprite>("cockpitButtons/GameButton15");
        btn6sprite2 = Resources.Load<Sprite>("cockpitButtons/GameButton16");

    }

    public void runMiniGame()
    {
        if (!playerTurn && !coroutineOn)
        {
            clickBlocker.SetActive(true);
            playerAttempt = "";
            correctOrder = "";
            StartCoroutine("minigame");
        }

        if (playerTurn)
        {
            clickBlocker.SetActive(false);
            if (playerAttempt != "")
            {
                int i = 0;
                foreach (char answer in playerAttempt)
                {
                    if (answer != correctOrder[i])
                    {
                        AudioManager.instance.Stop("minigameB1");
                        AudioManager.instance.Stop("minigameB2");
                        AudioManager.instance.Stop("minigameB3");
                        AudioManager.instance.Stop("minigameB4");
                        AudioManager.instance.Stop("minigameB5");
                        AudioManager.instance.Stop("minigameB6");
                        playerTurn = false;
                        clickBlocker.SetActive(true);
                        AudioManager.instance.Play("minigamefail1");
                        Debug.Log("failed! Restarting");
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                if (i == 6)
                {
                    Debug.Log("minigame successfull!");
                    AudioManager.instance.Play("cockPitWon");
                    setWinData();
                    isGameDone = true;
                }
            }
        }
    }
    public void minigameButtonPress() // methdod in the buttons, if not players turns, just plays sound.
    {
        if (playerTurn)
        {
            switch (EventSystem.current.currentSelectedGameObject.name)
            {
                case "button1":
                    AudioManager.instance.Play("minigameB1");
                    playerAttempt += "1";
                    break;
                case "button2":
                    AudioManager.instance.Play("minigameB2");
                    playerAttempt += "2";
                    break;
                case "button3":
                    AudioManager.instance.Play("minigameB3");
                    playerAttempt += "3";
                    break;
                case "button4":
                    AudioManager.instance.Play("minigameB4");
                    playerAttempt += "4";
                    break;
                case "button5":
                    AudioManager.instance.Play("minigameB5");
                    playerAttempt += "5";
                    break;
                case "button6":
                    AudioManager.instance.Play("minigameB6");
                    playerAttempt += "6";
                    break;
                default:
                    Debug.LogError("NAME OF SELECTED OBJECT WAS NOT ONE OF THE BUTTONS! BREAKS MINIGAME");
                    break;
            }
        }
    }

    public IEnumerator minigame() //the AI playing random buttons to create the pattern
    {
        coroutineOn = true;
        for (int i = 0; i < 6; i++)
        {
            if (i == 0) yield return new WaitForSeconds(1f);
            int nextButton = UnityEngine.Random.Range(0, 6);
            yield return new WaitForSeconds(0.5f);
            switch (nextButton)
            {
                case 0:
                    button1.onClick.Invoke();
                    AudioManager.instance.Play("minigameB1");

                    button1.image.sprite = btn1sprite2;
                    correctOrder += "1";
                    Debug.Log("1 pressed");
                    break;
                case 1:
                    button2.onClick.Invoke();
                    AudioManager.instance.Play("minigameB2");
                    button2.image.sprite = btn2sprite2;
                    correctOrder += "2";
                    Debug.Log("2 pressed");
                    break;
                case 2:
                    button3.onClick.Invoke();
                    AudioManager.instance.Play("minigameB3");
                    button3.image.sprite = btn3sprite2;
                    correctOrder += "3";
                    Debug.Log("3 pressed");
                    break;
                case 3:
                    button4.onClick.Invoke();
                    AudioManager.instance.Play("minigameB4");
                    button4.image.sprite = btn4sprite2;
                    correctOrder += "4";
                    Debug.Log("4 pressed");
                    break;
                case 4:
                    button5.onClick.Invoke();
                    AudioManager.instance.Play("minigameB5");
                    button5.image.sprite = btn5sprite2;
                    correctOrder += "5";
                    Debug.Log("5 pressed");
                    break;
                case 5:
                    button6.onClick.Invoke();
                    AudioManager.instance.Play("minigameB6");
                    button6.image.sprite = btn6sprite2;
                    correctOrder += "6";
                    Debug.Log("6 pressed");
                    break;
            }
            yield return new WaitForSeconds(0.5f);
            button1.image.sprite = btn1sprite1;
            button2.image.sprite = btn2sprite1;
            button3.image.sprite = btn3sprite1;
            button4.image.sprite = btn4sprite1;
            button5.image.sprite = btn5sprite1;
            button6.image.sprite = btn6sprite1;
        }
        yield return new WaitForSeconds(1);
        Debug.Log(correctOrder);
        playerTurn = true;
        coroutineOn = false;
    }
    public void resetMinigamescript() //called at the end of state change
    {
        isGameDone = GameManager.Instance.cockpitBoostOn;
        StopCoroutine("minigame");
        clickBlocker.SetActive(false);
        coroutineOn = false;
        correctOrder = "";
        playerAttempt = "";
    }
    public int cockpitBoost()
    {
        calculateBoost();
        return boostAmount;
    }
    private void setWinData()
    {
        calculateBoost();
        _xpManager.cockpitMGXPReward();
        GameManager.Instance.timeSinceCockPitCDStarted = DateTime.Now;
        GameManager.Instance.triggerCockPitMG = DateTime.Now.AddSeconds(UnityEngine.Random.Range(minSeconds, maxSeconds));
        GameManager.Instance.cockpitBoostOn = true;
    }
    private void calculateBoost()
    {
        if (_xpManager.cockPitLvl <= 1)
        {
            boostAmount = level0boost;
        }
        else
        {
            boostMultiplier = _xpManager.cockPitLvl / 2; //REMEMBER!! XP STUFF IS 10X IN CODE
            boostAmount = level0boost * boostMultiplier;
        }
    }
}
