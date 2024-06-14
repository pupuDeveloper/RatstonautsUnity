using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class cockpitMiniGame : MonoBehaviour
{
    private int minigameCD; //in seconds
    private int minigameLevel;
    private int baseSpeedBoost;
    private int boostMultiplier;
    private static bool isBoostOn;
    //buttons n other UI stuff
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;
    [SerializeField] private Button button4;
    [SerializeField] private Button button5;
    [SerializeField] private Button button6;
    [SerializeField] private GameObject clickBlocker;

    //
    private string correctOrder;
    private string playerAttempt;
    private bool playerTurn;
    private bool coroutineOn;

    private void Start()
    {
        //read is boost on etc from file
    }

    private void Update()
    {
        if (!isBoostOn)
        {
            runMiniGame();
        }
        else
        {
            //if boost is on and minigame not available
        }
    }

    private void runMiniGame()
    {
        if (!playerTurn && !coroutineOn)
        {
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
                        playerTurn = false;
                        clickBlocker.SetActive(true);
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                if (i == 5)
                {
                    Debug.Log("minigame successfull!");
                    isBoostOn = true;
                }
            }
        }
    }
    public void minigameButtonPress()
    {
        if (playerTurn)
        {
            switch (EventSystem.current.currentSelectedGameObject.name)
            {
                case "button1":
                playerAttempt += "1";
                break;
                case "button2":
                playerAttempt += "2";
                break;
                case "button3":
                playerAttempt += "3";
                break;
                case "button4":
                playerAttempt += "4";
                break;
                case "button5":
                playerAttempt += "5";
                break;
                case "button6":
                playerAttempt += "6";
                break;
                default:
                Debug.LogError("NAME OF SELECTED OBJECT WAS NOT ONE OF THE BUTTONS! BREAKS MINIGAME");
                break;
            }
        }
    }

    private IEnumerator minigame()
    {
        coroutineOn = true;
        for (int i = 0; i < 6; i++)
        {
            int nextButton = Random.Range(0, 5);
            yield return new WaitForSeconds(0.5f);
            switch (nextButton)
            {
                case 0:
                //play button 1
                correctOrder += "1";
                break;
                case 1:
                //play button 2
                correctOrder += "2";
                break;
                case 2:
                //play button 3
                correctOrder += "3";
                break;
                case 3:
                //play button 4
                correctOrder += "4";
                break;
                case 4:
                //play button 5
                correctOrder += "5";
                break;
                case 5:
                //play button 6
                correctOrder += "6";
                break;
            }
        }
        playerTurn = true;
        coroutineOn = false;
    }

    public bool checkBoost()
    {
        return isBoostOn;
    }
}
