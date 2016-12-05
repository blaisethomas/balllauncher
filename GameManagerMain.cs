using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GameManagerMain: MonoBehaviour {

    private bool gameStarted = false;
    private float timer;

    [SerializeField]
    private float gameLengthInSeconds = 5f;

    public static int score;
    public static int totalShotsTaken;

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

    ScoreManagerMain sm;

    // Use this for initialization
    void Start () {
        this.gameObject.AddComponent<ScoreManagerMain>();
        sm = this.GetComponent<ScoreManagerMain>();
        sm.LastAdded();
        
        gameStateText.text = "Practice";
        timer = gameLengthInSeconds;

        UpdateScoreBoard ();
	
	}
	
	// Update is called once per frame
	void Update () {

        // if game has not started and player pressed AppButton
        if (!gameStarted && GvrController.TouchDown)
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
        
        //gameStateSounds.PlayOneShot(gameStartSound);
        //string playerName = sm.LastAdded();
        //Debug.Log(playerName);
        gameStateText.text = "TEst";

    }

    private void EndGame()
    {
        gameStarted = false;
        timer = gameLengthInSeconds;
        gameStateText.text = "Hit App Button\nTo Play Again";
        gameStateSounds.PlayOneShot(gameEndSound);
        writeScore();
    }

    private void UpdateScoreBoard()
    {
        scoreText.text = "Score\n" + score;
        timerText.text = "Timer\n" + Mathf.RoundToInt (timer);
    }

    private void writeScore()
    {
        string scoreString = score.ToString();
        string totalShotsTakenString = totalShotsTaken.ToString();
        sm.CreateHighScore(scoreString, totalShotsTakenString);
    }


}
