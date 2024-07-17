using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KataKami : MonoBehaviour
{
    public static KataKami Instance { get; private set; }

    public Text player1Text;
    public Text player2Text;
    public Text Timer;
    public Text Score_Player1;
    public Text Score_Player2;
    public Text TurnWarningText;

    public string playerName1;
    public string playerName2;
    public int player1Score = 0;
    public int player2Score = 0;

    private float totalTime = 60f;
    private float turnTime = 10f;
    private float turnTimer;
    private bool isPlayer1Turn = true;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        playerName1 = PlayerPrefs.GetString("playerName1");
        playerName2 = PlayerPrefs.GetString("playerName2");

        player1Text.text = playerName1;
        player2Text.text = playerName2;

        turnTimer = turnTime;
        TurnWarningText.gameObject.SetActive(false);

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
        Invoke("HideTurnWarning", 2f);
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
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        PlayerPrefs.SetInt("player1Score", player1Score);
        PlayerPrefs.SetInt("player2Score", player2Score);

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

    public void ResetKataKami()
    {
        totalTime = 60f;
        player1Score = 0;
        player2Score = 0;
        isPlayer1Turn = true;
        turnTimer = turnTime;
        UpdateUI();
    }
}
