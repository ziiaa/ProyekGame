using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text timerText;
    private float timeLeft = 60f;
    private bool isPlayer1Turn = true;

    public int player1Score;
    public int player2Score;

    private string playerName1;
    private string playerName2;

    public TMP_Text player1Text;
    public TMP_Text player2Text;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerName1 = PlayerPrefs.GetString("playerName1");
        playerName2 = PlayerPrefs.GetString("playerName2");

        if (player1Text != null)
        {
            player1Text.text = playerName1;
        }
        else
        {
            Debug.LogError("Text UI untuk Player 1 tidak ditugaskan!");
        }

        if (player2Text != null)
        {
            player2Text.text = playerName2;
        }
        else
        {
            Debug.LogError("Text UI untuk Player 2 tidak ditugaskan!");
        }

        StartCoroutine(Timer());
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "Time Left: " + Mathf.Round(timeLeft).ToString();
        }
        else
        {
            SwitchTurn();
        }
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (timeLeft > 0)
            {
                timeLeft -= 1f;
                timerText.text = "Time Left: " + Mathf.Round(timeLeft).ToString();
            }
            else
            {
                SwitchTurn();
            }
        }
    }

    public void SwitchTurn()
    {
        isPlayer1Turn = !isPlayer1Turn;
        timeLeft = 60f;

        if (isPlayer1Turn)
        {
            Debug.Log("Giliran Player 1: " + playerName1);
        }
        else
        {
            Debug.Log("Giliran Player 2: " + playerName2);
        }
    }

    public bool IsPlayer1Turn
    {
        get { return isPlayer1Turn; }
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
    }
}