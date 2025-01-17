﻿using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    public static UIManager sharedInstance;
    public Text titleLabel;
    public Text scoreLabel;
    private int totalScore;

	void Awake () 
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
        totalScore = 0;
	}
	
	void Update () 
    {
        if(GameManager.sharedInstance.gamePaused||!GameManager.sharedInstance.gameStarted)
        {
            titleLabel.enabled = true;
        } else 
        {
            titleLabel.enabled = false;
        }
	}


    public void ScorePoints(int points)
    {
        totalScore += points;
        scoreLabel.text = "Score: " + totalScore;
    }
}

