using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Instance statis

    public Text Timer;
    public Text player1Text;
    public Text player2Text;
    public Text Score_Player1;
    public Text Score_Player2;

    private int player1Score = 0;
    private int player2Score = 0;
    private bool isPlayer1Turn = true; // Properti untuk mengecek giliran pemain
    private float gameTime = 60f; // Waktu permainan dalam detik
    private AudioSource audioSource; // Deklarasikan audioSource

    public bool IsPlayer1Turn
    {
        get { return isPlayer1Turn; }
    }

    public int Player1Score
    {
        get { return player1Score; }
    }

    public int Player2Score
    {
        get { return player2Score; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>(); // Inisialisasi audioSource
    }

    void Start()
    {
        // Inisialisasi
        SceneManager.sceneLoaded += OnSceneLoaded;
        ResetGame();
    }

    void Update()
    {
        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            Timer.text = "Time: " + Mathf.RoundToInt(gameTime).ToString();

            if (gameTime <= 0)
            {
                EndGame();
            }
        }
    }

    void ResetGame()
    {
        player1Score = 0;
        player2Score = 0;
        gameTime = 60f; // Reset waktu permainan
        UpdateScore();
    }

    void EndGame()
    {
        Timer.text = "Game Over!";
        // Logika untuk mengakhiri permainan
    }

    void UpdateScore()
    {
        Score_Player1.text = "Score: " + player1Score.ToString();
        Score_Player2.text = "Score: " + player2Score.ToString();
    }

    public void AddScore(int playerNumber, int scoreToAdd)
    {
        if (playerNumber == 1)
        {
            player1Score += scoreToAdd;
        }
        else if (playerNumber == 2)
        {
            player2Score += scoreToAdd;
        }

        UpdateScore();
    }

    public void SwitchTurn()
    {
        isPlayer1Turn = !isPlayer1Turn;
        // Logika lain untuk mengubah giliran
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "End")
        {
            if (audioSource != null)
            {
                audioSource.Stop(); // Hentikan musik saat pindah ke scene lain
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}