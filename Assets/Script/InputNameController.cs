using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputNameController : MonoBehaviour
{
    public InputField inputName1; // Referensi untuk InputField Player 1
    public InputField inputName2; // Referensi untuk InputField Player 2
    public Button btnPlay; // Referensi untuk tombol Play

    private string playerName1;
    private string playerName2;

    void Start()
    {
        if (btnPlay != null)
        {
            btnPlay.onClick.AddListener(OnPlayButtonClicked);
        }
        else
        {
            Debug.LogError("Button Play is not assigned!");
        }

        if (inputName1 == null)
        {
            Debug.LogError("InputField for Player 1 is not assigned!");
        }

        if (inputName2 == null)
        {
            Debug.LogError("InputField for Player 2 is not assigned!");
        }
    }

    void OnPlayButtonClicked()
    {
        if (inputName1 == null || inputName2 == null)
        {
            Debug.LogError("InputFields are not assigned!");
            return;
        }

        playerName1 = inputName1.text;
        playerName2 = inputName2.text;

        if (!string.IsNullOrEmpty(playerName1) && !string.IsNullOrEmpty(playerName2))
        {
            // Store player names and load the next scene
            PlayerPrefs.SetString("playerName1", playerName1);
            PlayerPrefs.SetString("playerName2", playerName2);
            SceneManager.LoadScene("Game"); // game scene
        }
        else
        {
            Debug.Log("Please enter names for both players.");
        }
    }
}
