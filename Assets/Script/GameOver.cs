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
    private string playerName1;
    private string playerName2;
    private string winner;
    private AudioSource audioSource;

    void Start()
    {
        player1Score = PlayerPrefs.GetInt("player1Score");
        player2Score = PlayerPrefs.GetInt("player2Score");
        playerName1 = PlayerPrefs.GetString("playerName1");
        playerName2 = PlayerPrefs.GetString("playerName2");

        if (player1Score > player2Score)
        {
            winner = playerName1 + " Wins!";
        }
        else if (player2Score > player1Score)
        {
            winner = playerName2 + " Wins!";
        }
        else
        {
            winner = "It's a Draw!";
        }

        scoreText.text = playerName1 + ": " + player1Score.ToString() + " - " + playerName2 + ": " + player2Score.ToString();
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
