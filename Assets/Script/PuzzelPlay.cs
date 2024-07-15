using UnityEngine;
using UnityEngine.UI;

public class PuzzleGame : MonoBehaviour
{
    public Button A_Pieces1_Button;
    public Button A_Pieces2_Button;
    public Text ScorePlayer1;
    public Text ScorePlayer2;

    private bool isPlayer1Turn = true; // Assume Player 1 starts first
    private Button lastButtonClicked;

    void Start()
    {
        A_Pieces1_Button.onClick.AddListener(() => OnPuzzlePieceClicked(A_Pieces1_Button));
        A_Pieces2_Button.onClick.AddListener(() => OnPuzzlePieceClicked(A_Pieces2_Button));
    }

    void OnPuzzlePieceClicked(Button clickedButton)
    {
        if (lastButtonClicked == null)
        {
            lastButtonClicked = clickedButton;
            return;
        }

        if ((lastButtonClicked == A_Pieces1_Button && clickedButton == A_Pieces2_Button) ||
            (lastButtonClicked == A_Pieces2_Button && clickedButton == A_Pieces1_Button))
        {
            // Correct match
            lastButtonClicked.image.color = Color.green;
            clickedButton.image.color = Color.green;

            if (isPlayer1Turn)
            {
                int score = int.Parse(ScorePlayer1.text);
                ScorePlayer1.text = (score + 1).ToString();
            }
            else
            {
                int score = int.Parse(ScorePlayer2.text);
                ScorePlayer2.text = (score + 1).ToString();
            }
        }
        else
        {
            // Incorrect match
            lastButtonClicked.image.color = Color.red;
            clickedButton.image.color = Color.red;
        }

        // Reset for next turn
        lastButtonClicked = null;
        isPlayer1Turn = !isPlayer1Turn;
    }
}
