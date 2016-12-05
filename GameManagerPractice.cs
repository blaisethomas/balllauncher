﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManagerPractice : MonoBehaviour {

    public bool gameStarted = false;
    private float timer;

    [SerializeField]
    private float gameLengthInSeconds = 60f;

    public static int score;
    public static int ballThrown;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private Text gameStateText;

    [SerializeField]
    private AudioSource gameStateSounds;

    [SerializeField]
    private AudioClip gameStartSound;

    [SerializeField]
    private AudioClip gameEndSound;

    public GameObject OnboardingCube;
    public GameObject Hand;
    private bool gameActive;



    // Use this for initialization
    void Start () {

        gameStateText.text = "Practice Round";
        timer = gameLengthInSeconds;
        UpdateScoreBoard ();
       
	
	}
	
	// Update is called once per frame
	void Update () {

        gameActive = Hand.GetComponent<BallLauncher>().gameActive;

        // if game has not started and player pressed AppButton
        if (!gameStarted && GvrController.ClickButtonDown && gameActive)
        {
            StartGame();
        }

        //if game has started 
        if (gameStarted)
        {
            timer -= Time.deltaTime;
            UpdateScoreBoard();
        }

        //if game has started and timer is less or equal to zero
        if (gameStarted && timer <= 0)
        {
            EndGame();
        }
	
	}

    private void StartGame()
    {
        score = 0;
        gameStarted = true;
        gameStateText.text = "Practice";
        //gameStateSounds.PlayOneShot(gameStartSound);
    }

    private void EndGame()
    {
        gameStarted = false;
        timer = gameLengthInSeconds;
        gameStateText.text = "Main Event...";
        //gameStateSounds.PlayOneShot(gameEndSound);
        //SceneManager.LoadScene("Test");
        //int indexSC = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Main");
        
    }

    private void UpdateScoreBoard()
    {
        scoreText.text = "Score\n" + score;
        timerText.text = "Timer\n" + Mathf.RoundToInt (timer);
    }



}
