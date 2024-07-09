using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private AudioSource audioSource; // Referensi ke AudioSource

    //Tampilan Waktu
    public Text timerText;
    private float timeLeft = 60f;
    private bool isPlayer1Turn = true;

    //Tampilan Score
    public int player1Score;
    public int player2Score;

    //Tampilan Nama Karakter
    private string playerName1;
    private string playerName2;

    public TMP_Text player1Text; // Referensi untuk TextMeshPro UI Player 1
    public TMP_Text player2Text; // Referensi untuk TextMeshPro UI Player 2

    //Score
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
        // Audio
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.Play(); // Mulai memutar musik saat loading
        }

        // Mengambil nama pemain dari PlayerPrefs
        playerName1 = PlayerPrefs.GetString("playerName1");
        playerName2 = PlayerPrefs.GetString("playerName2");

        // Menampilkan nama pemain di UI Text
        if (player1Text != null)
        {
            player1Text.text = "" + playerName1;
        }
        else
        {
            Debug.LogError("Text UI for Player 1 is not assigned!");
        }

        if (player2Text != null)
        {
            player2Text.text = "" + playerName2;
        }
        else
        {
            Debug.LogError("Text UI for Player 2 is not assigned!");
        }

        //Mulai Timer
        StartCoroutine(Timer());
    }

    //Timer
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
        while(true)
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
    private void SwitchTurn()
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