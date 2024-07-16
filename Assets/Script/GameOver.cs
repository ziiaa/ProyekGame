using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Text scoreText;
    public Text winnerText;
    public Button btn_PlayAgain;
    public Button btn_GamePlay;
    public AudioClip gameOverMusic; // Referensi ke musik game over

    private int player1Score;
    private int player2Score;
    private string winner;
    private AudioSource audioSource;

    void Start()
    {
        // Mengambil skor dari PlayerPrefs
        player1Score = PlayerPrefs.GetInt("player1Score");
        player2Score = PlayerPrefs.GetInt("player2Score");

        // Menentukan pemenang
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

        // Menampilkan hasil di UI
        scoreText.text = "Player 1: " + player1Score.ToString() + " - Player 2: " + player2Score.ToString();
        winnerText.text = winner;

        // Menambahkan listener untuk tombol Play Again
        btn_PlayAgain.onClick.AddListener(OnPlayAgainButton);
        btn_GamePlay.onClick.AddListener(OnPlayMainButton); // Corrected line

        // Setup audio
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
        // Hentikan musik game over
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        // Memuat ulang scene permainan saat tombol Play Again ditekan
        SceneManager.LoadScene("MainPuzzle"); // Ganti "GameScene" dengan nama scene permainan Anda
    }

    void OnPlayMainButton()
    {
        // Hentikan musik game over
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        // Memuat ulang scene permainan saat tombol Play Again ditekan
        SceneManager.LoadScene("Home"); // Ganti "GameScene" dengan nama scene permainan Anda
    }
}