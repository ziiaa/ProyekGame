using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputNameController : MonoBehaviour
{
    public InputField inputName1; // Referensi untuk InputField Player 1
    public InputField inputName2; // Referensi untuk InputField Player 2
    public Button btnPlay; // Referensi untuk tombol Play

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
            Debug.LogError("InputFields are not assigned correctly.");
            return;
        }

        // Cek apakah kedua InputField tidak kosong
        if (string.IsNullOrEmpty(inputName1.text) || string.IsNullOrEmpty(inputName2.text))
        {
            Debug.LogWarning("Both player names must be entered!");
            return; // Tidak melanjutkan jika ada yang kosong
        }

        // Simpan nama pemain dalam PlayerPrefs (opsional, jika perlu)
        PlayerPrefs.SetString("Player1Name", inputName1.text);
        PlayerPrefs.SetString("Player2Name", inputName2.text);

        // Memuat scene Game
        SceneManager.LoadScene("Game");
    }
}
