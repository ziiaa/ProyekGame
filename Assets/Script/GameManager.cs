using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
=======
using System.Collections;
using UnityEngine.SceneManagement;
>>>>>>> d2723c5ad0f73079e5ff7ca451e27b9b13a85191
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private AudioSource audioSource; // Referensi ke AudioSource

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
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
<<<<<<< HEAD
=======
        // Audio
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.Play(); // Mulai memutar musik saat loading
        }

        // Mengambil nama pemain dari PlayerPrefs
>>>>>>> d2723c5ad0f73079e5ff7ca451e27b9b13a85191
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
<<<<<<< HEAD

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
=======
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
>>>>>>> d2723c5ad0f73079e5ff7ca451e27b9b13a85191
}