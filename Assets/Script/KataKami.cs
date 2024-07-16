using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KataKami : MonoBehaviour
{
    public static KataKami Instance { get; private set; }

    public Text player1Text; // Reference for Player 1 Name Text
    public Text player2Text; // Reference for Player 2 Name Text
    public Text Timer; // Reference for Timer Text
    public Text Score_Player1; // Reference for Player 1 Score Text
    public Text Score_Player2; // Reference for Player 2 Score Text
    public Text TurnWarningText; // Reference for Turn Warning Text

    public string playerName1;
    public string playerName2;
    public int player1Score = 0;
    public int player2Score = 0;

    private float totalTime = 60f;
    private float turnTime = 10f;
    private float turnTimer;
    private bool isPlayer1Turn = true;
    private AudioSource audioSource; // Deklarasikan audioSource

    void Awake()
    {
        // Ensure only one instance of KataKami exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keeps the instance alive between scenes
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>(); // Inisialisasi audioSource
    }

    void Start()
    {
        playerName1 = PlayerPrefs.GetString("playerName1");
        playerName2 = PlayerPrefs.GetString("playerName2");

        player1Text.text = playerName1;
        player2Text.text = playerName2;

        turnTimer = turnTime;
        TurnWarningText.gameObject.SetActive(false); // Hide the warning text initially

        // Mulai memutar musik
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        totalTime -= Time.deltaTime;
        turnTimer -= Time.deltaTime;

        if (totalTime <= 0)
        {
            EndGame();
        }

        if (turnTimer <= 0)
        {
            ShowTurnWarning();
            SwitchTurn();
        }

        UpdateUI();
    }

    void ShowTurnWarning()
    {
        TurnWarningText.text = isPlayer1Turn ? $"{playerName1}'s turn is over!" : $"{playerName2}'s turn is over!";
        TurnWarningText.gameObject.SetActive(true);
        Invoke("HideTurnWarning", 2f); // Hide the warning after 2 seconds
    }

    void HideTurnWarning()
    {
        TurnWarningText.gameObject.SetActive(false);
    }

    void SwitchTurn()
    {
        isPlayer1Turn = !isPlayer1Turn;
        turnTimer = turnTime;
    }

    void UpdateUI()
    {
        Timer.text = $"Time: {Mathf.CeilToInt(totalTime)}s";
        Score_Player1.text = player1Score.ToString();
        Score_Player2.text = player2Score.ToString();
    }

    public void EndGame()
    {
        // Hentikan musik saat pindah scene
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        // Save the scores in PlayerPrefs
        PlayerPrefs.SetInt("player1Score", player1Score);
        PlayerPrefs.SetInt("player2Score", player2Score);

        // Load the End scene
        SceneManager.LoadScene("End");
    }

    public void AddScore()
    {
        if (isPlayer1Turn)
        {
            player1Score++;
        }
        else
        {
            player2Score++;
        }
    }
}