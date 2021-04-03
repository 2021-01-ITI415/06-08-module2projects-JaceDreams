using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;



// An enum to handle all the possible scoring events
public enum eScoreEvent
{
    draw,
    mine,
    mineGold,
    gameWin,
    gameLoss,
    board,
    boardGold,
}

// scoreManager handles all of the scoring
public class ScoreManager : MonoBehaviour
{
    static private ScoreManager S;

    static public int SCORE_FROM_PREV_ROUND = 0;
    static public int HIGH_SCORE = 0;

    [Header("Set Dynamically")]
    // Fields to track score info
    public int chain = 0; // of cards in this run
    public int scoreRun = 0;
    public int score = 0;

    void Awake()
    {
        if (S == null)
        {
            S = this; // Set the private singleton
        }
        else
        {
            Debug.LogError("ERROR: ScoreManager.Awake(): S is already set!");
        }

        // Check for a high score in PlayerPrefs
        if (PlayerPrefs.HasKey("ProspectorHighScore"))
        {
            HIGH_SCORE = PlayerPrefs.GetInt("ProspectorHighScore");
        }

        // Add the score from the last round, which will be >0 if it was a win
        score += SCORE_FROM_PREV_ROUND;

        // And reset the SCORE_FROM_PREV_ROUND
        SCORE_FROM_PREV_ROUND = 0;
    }

    static public void EVENT (eScoreEvent evt, bool isGold)
    {
        try
        {
            S.Event(evt, isGold);
        } catch (System.NullReferenceException nre)
        {
            Debug.LogError("ScoreManager:EVENT() called while S=null.\n" + nre);
        }
    }

    void Event(eScoreEvent evt, bool isGold)
    {
        switch (evt)
        {
            // Same things need to happen whether it's a draw, a win, or a loss
            case eScoreEvent.draw: // Drawing a card
            case eScoreEvent.gameWin: // Won the round
            case eScoreEvent.gameLoss: // Lost the round
                chain = 0; // resets the score chain
                score += scoreRun; // Add scoreRun to the total score
                scoreRun = 0; // reset scoreRun
                break;

            case eScoreEvent.mine: // Remove a mine card
                chain++; // Increase the score chain
                Debug.Log("isGold: " + isGold);
                if (isGold) { scoreRun += (2 * chain); }
                else { scoreRun += chain; }
                Debug.Log("Current score: " + scoreRun);
                scoreRun += chain; // add score for this card to run
                break;

            case eScoreEvent.board: // Remove a mine card
                chain++; // Increase the score chain
                Debug.Log("isGold: " + isGold);
                if (isGold) { scoreRun += (2 * chain); }
                else { scoreRun += chain; }
                Debug.Log("Current score: " + scoreRun);
                scoreRun += chain; // add score for this card to run
                break;
        }

        switch (evt)
        {
            case eScoreEvent.gameWin:
                // If it's a win, add the score to the next round. static fields are NOT reset by reloading the level
                SCORE_FROM_PREV_ROUND = score;
                print("You won this round! Round score: " + score);
                break;

            case eScoreEvent.gameLoss:
                // If it's a loss, check against the high score
                if (HIGH_SCORE <= score)
                {
                    print("You got the high score! High score: " + score);
                    HIGH_SCORE = score;
                    PlayerPrefs.SetInt("ProspectorHighScore", score);
                }
                else
                {
                    print("Your final score for the game was:" + score);
                }
                break;

            default:
                print("score: " + score + " scoreRun: " + scoreRun + " chain: " + chain);
                break;
        }
    }

    static public int CHAIN { get { return S.chain; } }
    static public int SCORE { get { return S.score; } }
    static public int SCORE_RUN { get { return S.scoreRun; } }
}