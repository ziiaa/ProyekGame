using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public PuzzlePiece[] puzzlePieces;
    private List<PuzzlePiece> selectedPieces;
    private GameController gameController;

    void Start()
    {
        selectedPieces = new List<PuzzlePiece>();

        gameController = FindObjectOfType<GameController>();
        if (gameController == null)
        {
            Debug.LogError("GameController tidak ditemukan di scene. Pastikan GameController ada dan aktif.");
            return;
        }

        // Ini adalah langkah sementara untuk mengisi array puzzlePieces
        puzzlePieces = FindObjectsOfType<PuzzlePiece>();

        string[] pieceTypes = { "A", "K", "U" };
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            if (puzzlePieces[i] != null)
            {
                puzzlePieces[i].pieceType = pieceTypes[i % pieceTypes.Length];
            }
            else
            {
                Debug.LogError("PuzzlePiece pada index " + i + " belum diinisialisasi.");
            }
        }
    }

    public void SelectPiece(PuzzlePiece piece)
    {
        if (selectedPieces.Contains(piece))
            return;

        selectedPieces.Add(piece);
        piece.HighlightPiece();

        if (selectedPieces.Count == 2)
        {
            CheckPair();
        }
    }

    void CheckPair()
    {
        if (selectedPieces.Count < 2) return;

        if (selectedPieces[0].pieceType == selectedPieces[1].pieceType)
        {
            selectedPieces[0].SetCorrect();
            selectedPieces[1].SetCorrect();
            gameController.AddScore(gameController.GetCurrentPlayer());
        }
        else
        {
            selectedPieces[0].SetIncorrect();
            selectedPieces[1].SetIncorrect();
        }

        selectedPieces.Clear();
        gameController.SwitchPlayer();
    }
}