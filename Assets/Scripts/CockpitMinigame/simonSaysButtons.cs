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
    private bool turn;
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
        simonButton1.enabled = false;
        simonButton2.enabled = false;
        simonButton3.enabled = false;
        simonButton4.enabled = false;
        simonButton5.enabled = false;
        simonButton6.enabled = false;
        turn = false;
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
        if (turn == false)
        {
            StartCoroutine(simonCoroutine());
            turn = true;
        }
        else
        {
        simonButton1.enabled = true;
        simonButton2.enabled = true;
        simonButton3.enabled = true;
        simonButton4.enabled = true;
        simonButton5.enabled = true;
        simonButton6.enabled = true;
        }
    }

    IEnumerator simonCoroutine()
    {
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
    }


    public void playSoundEffect()
    {
        soundPlayer.Play();
        if (turn)
        {
            playerMoves = string.Concat(playerMoves, 1);
        }
    }
    public void playSoundEffect2()
    {
        soundPlayer2.Play();
        if (turn)
        {
            playerMoves = string.Concat(playerMoves, 2);
        }
    }
    public void playSoundEffect3()
    {
        soundPlayer3.Play();
        if (turn)
        {
            playerMoves = string.Concat(playerMoves, 3);
        }
    }
    public void playSoundEffect4()
    {
        soundPlayer4.Play();
        if (turn)
        {
            playerMoves = string.Concat(playerMoves, 4);
        }
    }
    public void playSoundEffect5()
    {
        soundPlayer5.Play();
        if (turn)
        {
            playerMoves = string.Concat(playerMoves, 5);
        }
    }
    public void playSoundEffect6()
    {
        soundPlayer6.Play();
        if (turn)
        {
            playerMoves = string.Concat(playerMoves, 6);
        }
    }
}
