using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PuzzleGame1 : MonoBehaviour
{
    public Transform PuzzleContainer; // Container untuk potongan puzzle yang dapat dipilih pemain
    public Transform PuzzleContainer1; // Container untuk kotak yang dapat diklik
    public Text randomWord; // Tulisan "MIAW"
    public Button Btn_Check; // Button untuk mengecek apakah potongan puzzle yang dipilih sudah benar
    public Text Score_Player1; // Score pada player 1
    public Text Score_Player2; // Score pada player 2

    private string targetWord = "MIAW";
    private List<GameObject> selectedPieces = new List<GameObject>();
    private int player1Score = 0;
    private int player2Score = 0;

    private void Start()
    {
        if (PuzzleContainer == null || PuzzleContainer1 == null || randomWord == null ||
            Btn_Check == null || Score_Player1 == null || Score_Player2 == null)
        {
            Debug.LogError("Harap tugaskan semua komponen yang dibutuhkan di inspector.");
            return;
        }

        randomWord.text = targetWord;
        AssignPuzzlePieces();
        Btn_Check.onClick.AddListener(CheckAnswer);
    }

    private void AssignPuzzlePieces()
    {
        // Mengambil sprite dari PuzzleContainer dan assign ke PuzzleContainer1
        foreach (Transform child in PuzzleContainer)
        {
            Transform targetSlot = PuzzleContainer1.GetChild(child.GetSiblingIndex());
            if (child.TryGetComponent<Image>(out Image childImage) && targetSlot.TryGetComponent<Image>(out Image targetImage))
            {
                targetImage.sprite = childImage.sprite;
                targetSlot.name = child.name;
                targetSlot.GetComponent<Button>().onClick.AddListener(() => OnPuzzlePieceSelected(targetSlot.gameObject));
            }
        }
    }

    public void OnPuzzlePieceSelected(GameObject piece)
    {
        if (selectedPieces.Count < 2 && !selectedPieces.Contains(piece))
        {
            selectedPieces.Add(piece);
            piece.GetComponent<Image>().color = Color.grey;
        }
    }

    private void CheckAnswer()
    {
        if (selectedPieces.Count != 2)
        {
            Debug.Log("Harap pilih tepat 2 potongan.");
            return;
        }

        string formedWord = string.Join("", selectedPieces.ConvertAll(p => p.name).ToArray());
        bool isPlayer1Turn = GameManager.instance.IsPlayer1Turn; 

        if (targetWord.Contains(formedWord))
        {
            Debug.Log("Benar! Kata yang terbentuk: " + formedWord);
            if (isPlayer1Turn)
            {
                player1Score++;
                Score_Player1.text = player1Score.ToString();
                GameManager.instance.AddScore(1, 1);
            }
            else
            {
                player2Score++;
                Score_Player2.text = player2Score.ToString();
                GameManager.instance.AddScore(2, 1);
            }
        }
        else
        {
            Debug.Log("Salah! Kata yang terbentuk: " + formedWord);
            GameManager.instance.SwitchTurn();
        }

        foreach (var piece in selectedPieces)
        {
            piece.GetComponent<Image>().color = Color.white;
        }
        selectedPieces.Clear();
    }
}