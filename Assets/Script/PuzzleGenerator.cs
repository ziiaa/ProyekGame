using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class PuzzleGenerator : MonoBehaviour
{
    public string randomWord; // The word to generate puzzle pieces for
    public GameObject puzzlePiecePrefab; // The prefab for individual puzzle pieces
    public Transform puzzleContainer; // Container for puzzle pieces
    public Transform puzzleContainer1; // Container for assembled puzzle pieces
    public Button btnCheck; // Button to check the assembled puzzle
    public TextMeshProUGUI scorePlayer1Text; // Text for Player 1's score
    public TextMeshProUGUI scorePlayer2Text; // Text for Player 2's score
    public TextMeshProUGUI timerText; // Text for the timer
    public float gameTime = 60f; // Total game time in seconds

    private int scorePlayer1 = 0;
    private int scorePlayer2 = 0;
    private bool isPlayer1Turn = true;
    private float remainingTime;

    void Start()
    {
        // Ensure all required components are assigned
        if (puzzlePiecePrefab == null || puzzleContainer == null || puzzleContainer1 == null ||
            btnCheck == null || scorePlayer1Text == null || scorePlayer2Text == null || timerText == null)
        {
            Debug.LogError("Please assign all required components in the inspector.");
            return;
        }

        randomWord = GetRandomWord(); // Get a random word
        GeneratePuzzlePieces();

        btnCheck.onClick.AddListener(OnCheckButtonClicked);

        remainingTime = gameTime;
    }

    void Update()
    {
        remainingTime -= Time.deltaTime;
        timerText.text = "Time Left: " + Mathf.Ceil(remainingTime).ToString();

        if (remainingTime <= 0)
        {
            EndGame();
        }
    }

    void GeneratePuzzlePieces()
    {
        foreach (char letter in randomWord)
        {
            GameObject puzzlePiece = Instantiate(puzzlePiecePrefab, puzzleContainer);
            TextMeshProUGUI textComponent = puzzlePiece.GetComponentInChildren<TextMeshProUGUI>();

            if (textComponent != null)
            {
                textComponent.text = letter.ToString();
            }
            else
            {
                Debug.LogError("Puzzle piece prefab is missing a TextMeshProUGUI component.");
            }
        }
    }

    void OnCheckButtonClicked()
    {
        // Logic to check if the selected pieces in puzzleContainer1 match a part of randomWord
        bool isMatch = CheckPuzzleMatch();

        if (isMatch)
        {
            if (isPlayer1Turn)
            {
                scorePlayer1++;
                scorePlayer1Text.text = scorePlayer1.ToString();
            }
            else
            {
                scorePlayer2++;
                scorePlayer2Text.text = scorePlayer2.ToString();
            }
        }

        isPlayer1Turn = !isPlayer1Turn; // Switch turns
    }

    bool CheckPuzzleMatch()
    {
        // Collect all letters from the assembled puzzle pieces
        List<string> assembledLetters = new List<string>();

        foreach (Transform piece in puzzleContainer1)
        {
            TextMeshProUGUI textComponent = piece.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                assembledLetters.Add(textComponent.text);
            }
        }

        // Check if the assembled letters form a part of randomWord
        string assembledWord = string.Join("", assembledLetters);
        return randomWord.Contains(assembledWord);
    }

    void EndGame()
    {
        // End the game and determine the winner
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

        // Optionally, load an end game scene or restart the game
    }

    string GetRandomWord()
    {
        // You can replace this with a more sophisticated random word generation method
        string[] words = { "apple", "banana", "cherry", "date", "fig", "grape", "kiwi" };
        return words[Random.Range(0, words.Length)];
    }
}
