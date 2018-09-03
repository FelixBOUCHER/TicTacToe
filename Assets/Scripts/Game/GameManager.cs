/**************************************
* Author: Thomas Knox
* ~ GameManager ~ 
* Manages the state of the game;
**************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using CotcSdk;
using CotcSdkTemplate;

public class GameManager : MonoBehaviour
{
    [HideInInspector] // characters for player one and two
    public string playerOne = "x", playerTwo = "o";

    [Tooltip("The UI Text components attached to the space objects;")]
    public List<Text> spaces = new List<Text>();

    [Tooltip("End of the game UI object;")]
    public GameObject endUI;

    // game ai manager
    private AI ai;

    /******************************************
    * Runs once at the start;
    ******************************************/
    void Start ()
    {
        // get the game ai
        ai = GetComponent<AI>();
	}
	
	void SetAchievementData(string WhatAreWeUpping)
    {
		LoginFeatures.gamer.Transactions.Post(Bundle.CreateObject(WhatAreWeUpping, +1))
		.Done(txResult => {
		Debug.Log(txResult.TriggeredAchievements.Count); // 1
		});
    }
	
	void PostScore()
			{
				// currentGamer is an object retrieved after one of the different Login functions.

				if (Settings.difficulty == 0)
					{
						LoginFeatures.gamer.Scores.Domain("private").Post(Settings.wins, "Easy", ScoreOrder.HighToLow,
						"", false)
						.Done(postScoreRes => {
						Debug.Log("Post score: " + postScoreRes.ToString());
						}, ex => {
						// The exception should always be CotcException
						CotcException error = (CotcException)ex;
						Debug.LogError("Could not post score: " + error.ErrorCode + " (" + error.ErrorInformation + ")");
						});
					}
					
				if (Settings.difficulty == 1)
					{
						LoginFeatures.gamer.Scores.Domain("private").Post(Settings.wins, "Normal", ScoreOrder.HighToLow,
						"", false)
						.Done(postScoreRes => {
						Debug.Log("Post score: " + postScoreRes.ToString());
						}, ex => {
						// The exception should always be CotcException
						CotcException error = (CotcException)ex;
						Debug.LogError("Could not post score: " + error.ErrorCode + " (" + error.ErrorInformation + ")");
						});
					}
					
				if (Settings.difficulty == 2)
					{
						LoginFeatures.gamer.Scores.Domain("private").Post(Settings.wins, "Hard", ScoreOrder.HighToLow,
						"", false)
						.Done(postScoreRes => {
						Debug.Log("Post score: " + postScoreRes.ToString());
						}, ex => {
						// The exception should always be CotcException
						CotcException error = (CotcException)ex;
						Debug.LogError("Could not post score: " + error.ErrorCode + " (" + error.ErrorInformation + ")");
						});
						
						if(Settings.wins == 3)
						{
							SetAchievementData("Win-Hard");
						}
					}
			}

    /// <summary>
    /// Try to set a space on the board;
    /// </summary>
    /// <param name="id">Id/Index of the space to set;</param>
    public void SetSpace(int id)
    {
        // if the space is open
        if (spaces[id].text == "")
        {
            // place a marker depending on the player
            spaces[id].text = Settings.playerOneTurn ? playerOne : playerTwo;
            
            // swap players)
            Settings.playerOneTurn = !Settings.playerOneTurn;

            // check for a win or tie
            int win = CheckWin();
            if (win > -1)
            {
                switch (win)
                {
                    case 0: Settings.wins += 1; break;
					
                    case 1: Settings.losses += 1;
					if(Settings.difficulty == 0)
					{
						SetAchievementData("Lose-Easy");
					}
					if(Settings.difficulty == 2)
					{
						Settings.wins = 0;	
					}
					break;
					
                    case 2: Settings.ties += 1; break;
                }
				
				PostScore();

                // spawn the end of the round ui
                Instantiate(endUI);

                return;
            }

            // if it is player two, let ai go
            if (!Settings.playerOneTurn)
                ai.Move();
        }
    }

    /// <summary>
    /// Check to see if someone won;
    /// </summary>
    /// <returns>-1: No Winner, 0: Player One, 1: Player Two, 2: Tie</returns>
    int CheckWin()
    {
        // check for a horizontal win
        for (int i = 0; i < 7; i += 3)
        {
            // check if row is all the same
            if (spaces[i].text == spaces[i + 1].text && spaces[i + 1].text == spaces[i + 2].text)
            {
                if (spaces[i].text == "")
                    break;
                else if (spaces[i].text == playerOne && spaces[i].text != "")
                    return 0;
                else
                    return 1;
            }
        }

        // check for a vertical win
        for (int i = 0; i < 3; i++)
        {
            if (spaces[i].text == spaces[i + 3].text && spaces[i + 3].text == spaces[i + 6].text)
            {
                if (spaces[i].text == "")
                    break;
                else if (spaces[i].text == playerOne)
                    return 0;
                else
                    return 1;
            }
        }

        // check for a (0 - 4 - 8) diagonal win
        if (spaces[0].text == spaces[4].text && spaces[4].text == spaces[8].text)
        {
            if (spaces[0].text != "")
            {
                if (spaces[0].text == playerOne)
                    return 0;
                else
                    return 1;
            }
        }

        // check for a (2 - 4 - 6) diagonal win
        if (spaces[2].text == spaces[4].text && spaces[4].text == spaces[6].text)
        {
            if (spaces[2].text != "")
            {
                if (spaces[2].text == playerOne)
                    return 0;
                else
                    return 1;
            }
        }

        // last - check for a tie
        int filled = 0;
        for(int i = 0; i < 9; i++)
        {
            // check all spaces for an empty
            if (spaces[i].text != "")
                filled += 1;
        }
        // if every space is filled
        if (filled == 9)
            return 2;

        return -1;
    }
}
