using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Text scoreText;
    public Text winnerText;
    public Button btn_PlayAgain;
    public Button btn_GamePlay;
    public AudioClip gameOverMusic;

    private int player1Score;
    private int player2Score;
    private string winner;
    private AudioSource audioSource;

    void Start()
    {
        player1Score = PlayerPrefs.GetInt("player1Score");
        player2Score = PlayerPrefs.GetInt("player2Score");

        if (player1Score > player2Score)
        {
            winner = "Player 1 Wins!";
        }
        else if (player2Score > player1Score)
        {
            winner = "Player 2 Wins!";
        }
        else
        {
            winner = "It's a Draw!";
        }

        scoreText.text = "Player 1: " + player1Score.ToString() + " - Player 2: " + player2Score.ToString();
        winnerText.text = winner;

        btn_PlayAgain.onClick.AddListener(OnPlayAgainButton);
        btn_GamePlay.onClick.AddListener(OnPlayMainButton);

        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && gameOverMusic != null)
        {
            audioSource.clip = gameOverMusic;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource or gameOverMusic is not assigned!");
        }
    }

    void OnPlayAgainButton()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        if (ManagerKata.Instance != null)
        {
            ManagerKata.Instance.ResetKataKami();
        }

        if (KataKami.Instance != null)
        {
            KataKami.Instance.ResetKataKami();
        }

        SceneManager.LoadScene("MainPuzzle");
    }

    void OnPlayMainButton()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        SceneManager.LoadScene("Home");
    }
}
