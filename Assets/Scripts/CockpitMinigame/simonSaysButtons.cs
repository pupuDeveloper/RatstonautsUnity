using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class simonSaysButtons : MonoBehaviour
{
    public AudioSource soundPlayer;
    public AudioSource soundPlayer2;
    public AudioSource soundPlayer3;
    public AudioSource soundPlayer4;
    public AudioSource soundPlayer5;
    public AudioSource soundPlayer6;
    private Button simonButton1;
    private Button simonButton2;
    private Button simonButton3;
    private Button simonButton4;
    private Button simonButton5;
    private Button simonButton6;
    private Image blocker;
    private bool turn;
    private bool crRunning;
    private string compMoves = "";
    private string playerMoves = "";

    private void Start()
    {
        simonButton1 = GameObject.Find("Button1").GetComponent<Button>();
        simonButton2 = GameObject.Find("Button2").GetComponent<Button>();
        simonButton3 = GameObject.Find("Button3").GetComponent<Button>();
        simonButton4 = GameObject.Find("Button4").GetComponent<Button>();
        simonButton5 = GameObject.Find("Button5").GetComponent<Button>();
        simonButton6 = GameObject.Find("Button6").GetComponent<Button>();
        blocker = GameObject.Find("clickBlocker").GetComponent<Image>();
        simonButton1.enabled = false;
        simonButton2.enabled = false;
        simonButton3.enabled = false;
        simonButton4.enabled = false;
        simonButton5.enabled = false;
        simonButton6.enabled = false;
        blocker.enabled = false;
        turn = false;
        crRunning = false;
    }

    //TODO 
    //Check if its games or players turn to try the simon game
    //when scene is loaded, play 6 buttons in random order
    //after that, player tries to recreate the order by tapping the buttons
    //if the player fails (even after one wrong answer, for example the 4th button is wrong), have an indicator
    //that the player failed (maybe a sound that plays 3 times like beepbeepbeep WRONG)
    //If player gets correctly, display a message and a sound that player succeeded

    private void Update()
    {
        if (turn == false && crRunning == false)
        {
            StartCoroutine(simonCoroutine());
            turn = true;
        }
        else
        {

        }
    }

    //this method is the randomised moves the computer does in the simon game
    IEnumerator simonCoroutine()
    {
        blocker.enabled = true;
        crRunning = true;
        Debug.Log("got in");
        simonButton1.enabled = true;
        simonButton2.enabled = true;
        simonButton3.enabled = true;
        simonButton4.enabled = true;
        simonButton5.enabled = true;
        simonButton6.enabled = true;
        for (int i = 0; i <= 5; i++)
        {
            int randButton = Random.Range(1, 6);
            compMoves = string.Concat(compMoves, randButton);
            yield return new WaitForSeconds(1);
            switch (randButton)
            {
                case 1:
                simonButton1.onClick.Invoke();
                Debug.Log("1 pressed");
                break;
                case 2:
                simonButton2.onClick.Invoke();
                Debug.Log("2 pressed");
                break;
                case 3:
                simonButton3.onClick.Invoke();
                Debug.Log("3 pressed");
                break;
                case 4:
                simonButton4.onClick.Invoke();
                Debug.Log("4 pressed");
                break;
                case 5:
                simonButton5.onClick.Invoke();
                Debug.Log("5 pressed");
                break;
                case 6:
                simonButton6.onClick.Invoke();
                Debug.Log("6 pressed");
                break;
            }
        }
        simonButton1.enabled = false;
        simonButton2.enabled = false;
        simonButton3.enabled = false;
        simonButton4.enabled = false;
        simonButton5.enabled = false;
        simonButton6.enabled = false;
        Debug.Log(playerMoves);
        Debug.Log(compMoves);
        crRunning = false;
        blocker.enabled = false;
    }

    private bool compareMoves(string playerMoves, string compMoves)
    {
        if (playerMoves != "")
        {
            char[] p = playerMoves.ToCharArray();
            char[] c = compMoves.ToCharArray();
            int length = playerMoves.Length;
            if (p[length - 1] == c[length] - 1)
            {
                return true;
            }
        return false;
        }
    return false;
    }


    public void playSoundEffect()
    {
        soundPlayer.Play();
        if (crRunning == false)
        {
            playerMoves = string.Concat(playerMoves, 1);
            compareMoves(playerMoves, compMoves);
        }
    }
    public void playSoundEffect2()
    {
        soundPlayer2.Play();
        if (crRunning == false)
        {
            playerMoves = string.Concat(playerMoves, 2);
            compareMoves(playerMoves, compMoves);
        }
    }
    public void playSoundEffect3()
    {
        soundPlayer3.Play();
        if (crRunning == false)
        {
            playerMoves = string.Concat(playerMoves, 3);
            compareMoves(playerMoves, compMoves);
        }
    }
    public void playSoundEffect4()
    {
        soundPlayer4.Play();
        if (crRunning == false)
        {
            playerMoves = string.Concat(playerMoves, 4);
            compareMoves(playerMoves, compMoves);
        }
    }
    public void playSoundEffect5()
    {
        soundPlayer5.Play();
        if (crRunning == false)
        {
            playerMoves = string.Concat(playerMoves, 5);
            compareMoves(playerMoves, compMoves);
        }
    }
    public void playSoundEffect6()
    {
        soundPlayer6.Play();
        if (crRunning == false)
        {
            playerMoves = string.Concat(playerMoves, 6);
            compareMoves(playerMoves, compMoves);
        }
    }
}
