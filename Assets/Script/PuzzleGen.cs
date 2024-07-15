using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class PuzzleGen : MonoBehaviour
{
    public List<string> words; // Daftar kata

    public GameObject puzzlePiecePrefab; // Prefab untuk potongan puzzle
    public Transform puzzleContainer; // Kontainer untuk potongan puzzle
    public Transform puzzleContainer1; // Kontainer untuk potongan puzzle yang sudah disusun
    public Button btnCheck; // Tombol untuk cek jawaban
    public TextMeshProUGUI scorePlayer1Text; // Teks skor pemain 1
    public TextMeshProUGUI scorePlayer2Text; // Teks skor pemain 2
    public TextMeshProUGUI timerText; // Teks timer
    public float gameTime = 60f; // Waktu total permainan

    public Button A_Pieces1_Button; // Referensi untuk A_Pieces1_Button
    public Button A_Pieces2_Button; // Referensi untuk A_Pieces2_Button

    private int scorePlayer1 = 0;
    private int scorePlayer2 = 0;
    private bool isPlayer1Turn;
    private float remainingTime;
    private string randomWord;
    private List<char> selectedLetters = new List<char>(); // Daftar huruf yang dipilih

    void Start()
    {
        // Pastikan semua komponen telah di-assign
        if (puzzlePiecePrefab == null || puzzleContainer == null || puzzleContainer1 == null ||
            btnCheck == null || scorePlayer1Text == null || scorePlayer2Text == null || timerText == null ||
            A_Pieces1_Button == null || A_Pieces2_Button == null)
        {
            Debug.LogError("Pastikan semua komponen telah di-assign di inspector.");
            return;
        }

        randomWord = GetRandomWord(); // Ambil kata acak
        GeneratePuzzlePieces();

        btnCheck.onClick.AddListener(OnCheckButtonClicked);
        A_Pieces1_Button.onClick.AddListener(() => OnPuzzlePieceClicked(A_Pieces1_Button, 'A'));
        A_Pieces2_Button.onClick.AddListener(() => OnPuzzlePieceClicked(A_Pieces2_Button, 'A'));

        remainingTime = gameTime;
        isPlayer1Turn = Random.value > 0.5f; // Random pemain yang mulai
        UpdateTurnDisplay();
    }

    void Update()
    {
        remainingTime -= Time.deltaTime;
        timerText.text = "Time Left: " + Mathf.Ceil(remainingTime).ToString();

        if (remainingTime <= 0)
        {
            EndTurn();
        }
    }

    void GeneratePuzzlePieces()
    {
        foreach (char letter in randomWord)
        {
            GameObject puzzlePiece = Instantiate(puzzlePiecePrefab, puzzleContainer);
            Image imageComponent = puzzlePiece.GetComponent<Image>();
            TextMeshProUGUI textComponent = puzzlePiece.GetComponentInChildren<TextMeshProUGUI>();

            if (imageComponent != null && textComponent != null)
            {
                Sprite letterSprite = Resources.Load<Sprite>("Asset Game/Resources/Sprites" + letter); // Asumsi gambar ada di folder Resources/Sprites
                imageComponent.sprite = letterSprite;
                textComponent.text = letter.ToString();

                // Tambahkan event klik
                puzzlePiece.GetComponent<Button>().onClick.AddListener(() => OnPuzzlePieceClicked(puzzlePiece.GetComponent<Button>(), letter));
            }
            else
            {
                Debug.LogError("Prefab potongan puzzle kehilangan komponen Image atau TextMeshProUGUI.");
            }
        }
    }

    void OnPuzzlePieceClicked(Button clickedButton, char letter)
    {
        Debug.Log("Button clicked: " + letter);

        // Logika untuk menangani klik potongan puzzle
        // Jika huruf yang diklik adalah bagian dari kata acak, perbarui skor
        if (randomWord.Contains(letter))
        {
            clickedButton.image.color = Color.green; // Ubah warna menjadi hijau
            Debug.Log("Button turned green: " + letter);
            selectedLetters.Add(letter);
            if (selectedLetters.Count == 2 && selectedLetters[0] == 'A' && selectedLetters[1] == 'A')
            {
                if (isPlayer1Turn)
                {
                    scorePlayer1++;
                    scorePlayer1Text.text = "Player 1 Score: " + scorePlayer1.ToString();
                }
                else
                {
                    scorePlayer2++;
                    scorePlayer2Text.text = "Player 2 Score: " + scorePlayer2.ToString();
                }
                selectedLetters.Clear(); // Reset the selected letters list
            }
        }
        else
        {
            clickedButton.image.color = Color.red; // Ubah warna menjadi merah
            Debug.Log("Button turned red: " + letter);
            selectedLetters.Clear(); // Reset the selected letters list
        }

        isPlayer1Turn = !isPlayer1Turn; // Ganti giliran
        UpdateTurnDisplay();
        remainingTime = gameTime; // Reset timer untuk pemain berikutnya
    }

    void OnCheckButtonClicked()
    {
        // Verifikasi apakah potongan puzzle di puzzleContainer1 membentuk kata yang benar
        string assembledWord = GetAssembledWord();

        if (assembledWord == randomWord)
        {
            // Jika benar, tambahkan skor pemain
            if (isPlayer1Turn)
            {
                scorePlayer1++;
                scorePlayer1Text.text = "Player 1 Score: " + scorePlayer1.ToString();
            }
            else
            {
                scorePlayer2++;
                scorePlayer2Text.text = "Player 2 Score: " + scorePlayer2.ToString();
            }
        }

        isPlayer1Turn = !isPlayer1Turn; // Ganti giliran
        UpdateTurnDisplay();
        remainingTime = gameTime; // Reset timer untuk pemain berikutnya
    }

    string GetAssembledWord()
    {
        // Kumpulkan semua huruf dari puzzleContainer1
        List<char> assembledLetters = new List<char>();

        foreach (Transform piece in puzzleContainer1)
        {
            TextMeshProUGUI textComponent = piece.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                assembledLetters.Add(textComponent.text[0]);
            }
        }

        return new string(assembledLetters.ToArray());
    }

    void EndTurn()
    {
        isPlayer1Turn = !isPlayer1Turn; // Ganti giliran
        UpdateTurnDisplay();
        remainingTime = gameTime; // Reset timer untuk pemain berikutnya
    }

    void EndGame()
    {
        if (scorePlayer1 > scorePlayer2)
        {
            Debug.Log("Player 1 wins!");
        }
        else if (scorePlayer2 > scorePlayer1)
        {
            Debug.Log("Player 2 wins!");
        }
        else
        {
            Debug.Log("It's a tie!");
        }

        // Opsi: muat ulang scene atau mulai ulang permainan
    }

    string GetRandomWord()
    {
        return words[Random.Range(0, words.Count)];
    }

    void UpdateTurnDisplay()
    {
        // Opsi: perbarui UI untuk menunjukkan giliran siapa
        Debug.Log(isPlayer1Turn ? "Player 1's turn" : "Player 2's turn");
    }
}
