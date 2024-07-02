using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Tampilan Score
    public int player1Score;
    public int player2Score;

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

    //Tampilan Nama Karakter
    private string playerName1;
    private string playerName2;

    public TMP_Text player1Text; // Referensi untuk TextMeshPro UI Player 1
    public TMP_Text player2Text; // Referensi untuk TextMeshPro UI Player 2

    void Start()
    {
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
    }
}