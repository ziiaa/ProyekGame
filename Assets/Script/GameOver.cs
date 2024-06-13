using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Text scoreText;
    public Text winnerText;
    public Button btn_PlayAgain;

    private int player1Score;
    private int player2Score;
    private string winner;

    void Start()
    {
        // Misalkan kita mengambil skor dari GameManager (yang harus kamu implementasi)
        player1Score = GameManager.instance.player1Score;
        player2Score = GameManager.instance.player2Score;

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
    }

    void OnPlayAgainButton()
    {
        // Memuat ulang scene saat tombol Play Again ditekan
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
