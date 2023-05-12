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
    private string[] compMoves = new string[5];

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
        if (!turn)
        {

        }
    }

    IEnumerator simonCoroutine()
    {
        for (int i = 0; i <= 5; i++)
        {
            int randButton = Random.Range(1, 6);
            compMoves[i] = randButton;
            yield return new WaitForSeconds(1);
            switch (randButton)
            {
                case 1:
                simonButton1.onClick.Invoke();
                break;
            }
        }
    }


    public void playSoundEffect()
    {
        soundPlayer.Play();
    }
    public void playSoundEffect2()
    {
        soundPlayer2.Play();
    }
    public void playSoundEffect3()
    {
        soundPlayer3.Play();
    }
    public void playSoundEffect4()
    {
        soundPlayer4.Play();
    }
    public void playSoundEffect5()
    {
        soundPlayer5.Play();
    }
    public void playSoundEffect6()
    {
        soundPlayer6.Play();
    }
}
