using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGame : MonoBehaviour
{
    public GameObject puzzlePiecePrefab; // Prefab for puzzle piece
    public Transform puzzleContainer; // Container for the puzzle pieces UI
    public Text targetWordText; // Text to display the target word
    public Button checkButton; // Button to check the answer
    public Text leftScoreText;
    public Text rightScoreText;

    private string targetWord = "MIAW"; // Target word to form
    private List<string> selectedLetters = new List<string>(); // List to store selected letters
    private int leftScore = 0;
    private int rightScore = 0;

    private void Start()
    {
        // Initialize target word
        targetWordText.text = targetWord;
        // Generate puzzle pieces
        GeneratePuzzlePieces();
        // Add listener to check button
        checkButton.onClick.AddListener(CheckAnswer);
    }

    private void GeneratePuzzlePieces()
    {
        foreach (char letter in targetWord)
        {
            GameObject pieceInstance = Instantiate(puzzlePiecePrefab, puzzleContainer);
            pieceInstance.name = letter.ToString();
            pieceInstance.GetComponentInChildren<Text>().text = letter.ToString();
            pieceInstance.GetComponent<Button>().onClick.AddListener(() => OnPuzzlePieceSelected(pieceInstance));
        }
    }

    public void OnPuzzlePieceSelected(GameObject piece)
    {
        // Add selected letter to the list
        selectedLetters.Add(piece.name);
        piece.GetComponent<Image>().color = Color.grey; // Change color to indicate it's selected
    }

    private void CheckAnswer()
    {
        string formedWord = string.Join("", selectedLetters.ToArray());
        if (formedWord == targetWord)
        {
            // Correct answer
            Debug.Log("Correct! Formed word: " + formedWord);
            leftScore++;
            leftScoreText.text = leftScore.ToString();
        }
        else
        {
            // Wrong answer
            Debug.Log("Wrong answer! Formed word: " + formedWord);
            rightScore++;
            rightScoreText.text = rightScore.ToString();
        }

        // Clear selected letters and reset puzzle pieces
        selectedLetters.Clear();
        foreach (Transform child in puzzleContainer)
        {
            child.GetComponent<Image>().color = Color.white; // Reset color
        }
    }
}
