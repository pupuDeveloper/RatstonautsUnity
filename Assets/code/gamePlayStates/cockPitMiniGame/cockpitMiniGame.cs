using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class cockpitMiniGame : MonoBehaviour
{
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
    private int minSeconds = 7200;
    private int maxSeconds = 28800;

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
            Debug.Log("your turn!");
            if (playerAttempt != "")
            {
                int i = 0;
                foreach (char answer in playerAttempt)
                {
                    if (answer != correctOrder[i])
                    {
                        playerTurn = false;
                        clickBlocker.SetActive(true);
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
            int nextButton = UnityEngine.Random.Range(0, 6);
            yield return new WaitForSeconds(1f);
            switch (nextButton)
            {
                case 0:
                button1.onClick.Invoke();
                AudioManager.instance.Play("minigameB1");
                correctOrder += "1";
                Debug.Log("1 pressed");
                break;
                case 1:
                button2.onClick.Invoke();
                AudioManager.instance.Play("minigameB2");
                correctOrder += "2";
                Debug.Log("2 pressed");
                break;
                case 2:
                button3.onClick.Invoke();
                AudioManager.instance.Play("minigameB3");
                correctOrder += "3";
                Debug.Log("3 pressed");
                break;
                case 3:
                button4.onClick.Invoke();
                AudioManager.instance.Play("minigameB4");
                correctOrder += "4";
                Debug.Log("4 pressed");
                break;
                case 4:
                button5.onClick.Invoke();
                AudioManager.instance.Play("minigameB5");
                correctOrder += "5";
                Debug.Log("5 pressed");
                break;
                case 5:
                button6.onClick.Invoke();
                AudioManager.instance.Play("minigameB6");
                correctOrder += "6";
                Debug.Log("6 pressed");
                break;
            }
        }
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
        return boostAmount;
    }
    private void setWinData()
    {
        
        if (_xpManager.cockPitLvl != 0)
        {
            boostMultiplier = _xpManager.checkLvls(GameManager.Instance.cockPitXP); //REMEMBER!! XP STUFF IS 10X IN CODE
            boostAmount = level0boost * boostMultiplier;
        }
        else
        {
            boostAmount = level0boost;
        }
        _xpManager.cockpitMGXPReward();
        GameManager.Instance.timeSinceCockPitCDStarted = DateTime.Now;
        GameManager.Instance.triggerCockPitMG = DateTime.Now.AddSeconds(UnityEngine.Random.Range(minSeconds, maxSeconds));
        GameManager.Instance.cockpitBoostOn = true;
    }
}
