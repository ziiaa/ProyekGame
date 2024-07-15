using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text TimerText;
    public Text ScorePlayer1;
    public Text ScorePlayer2;
    public Text Player1;
    public Text Player2;

    private int currentPlayer;
    private int scorePlayer1;
    private int scorePlayer2;
    private float gameTime = 60f;
    private float turnTime = 5f;
    private float currentTurnTime;
    private bool isGameActive;

    void Start()
    {
        isGameActive = true;
        currentPlayer = Random.Range(1, 3); // 1 for Player1, 2 for Player2
        UpdatePlayerTurn();
        StartCoroutine(GameTimer());
    }

    void UpdatePlayerTurn()
    {
        if (currentPlayer == 1)
        {
            Player1.color = Color.yellow;
            Player2.color = Color.white;
        }
        else
        {
            Player1.color = Color.white;
            Player2.color = Color.yellow;
        }

        currentTurnTime = turnTime;
        StartCoroutine(TurnTimer());
    }

    IEnumerator GameTimer()
    {
        while (gameTime > 0 && isGameActive)
        {
            gameTime -= Time.deltaTime;
            TimerText.text = "Timer Left: " + Mathf.Ceil(gameTime).ToString();
            yield return null;
        }

        isGameActive = false;
        // End game logic here
    }

    IEnumerator TurnTimer()
    {
        while (currentTurnTime > 0 && isGameActive)
        {
            currentTurnTime -= Time.deltaTime;
            yield return null;
        }

        // Switch player turn
        currentPlayer = currentPlayer == 1 ? 2 : 1;
        UpdatePlayerTurn();
    }

    public void AddScore(int playerNumber)
    {
        if (playerNumber == 1)
        {
            scorePlayer1++;
            ScorePlayer1.text = "Score: " + scorePlayer1.ToString();
        }
        else
        {
            scorePlayer2++;
            ScorePlayer2.text = "Score: " + scorePlayer2.ToString();
        }
    }

    public void SwitchPlayer()
    {
        currentPlayer = currentPlayer == 1 ? 2 : 1;
        UpdatePlayerTurn();
    }

    public int GetCurrentPlayer()
    {
        return currentPlayer;
    }
}