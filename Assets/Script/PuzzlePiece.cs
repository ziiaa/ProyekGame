using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    public Button pieceButton;
    public GameObject pieceObject;
    public string pieceType; // "A", "K", or "U"
    private PuzzleManager puzzleManager;
    private bool isSelected;

    void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
        pieceButton.onClick.AddListener(OnPieceClicked);
        isSelected = false;
    }

    void OnPieceClicked()
    {
        if (isSelected)
            return;

        puzzleManager.SelectPiece(this);
    }

    public void HighlightPiece()
    {
        isSelected = true;
        pieceObject.GetComponent<Image>().color = Color.green;
    }

    public void SetCorrect()
    {
        pieceObject.GetComponent<Image>().color = Color.green;
    }

    public void SetIncorrect()
    {
        pieceObject.GetComponent<Image>().color = Color.red;
    }
}