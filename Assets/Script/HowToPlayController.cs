using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HowToPlayController : MonoBehaviour
{
    // Reference to the Button component
    public Button backButton;
    public string mainMenuSceneName = "MainMenu"; // Name of the main menu scene
    public string gameSceneName = "Home"; // Name of the game scene

    // Start is called before the first frame update
    void Start()
    {
        // Add listeners to the buttons
        backButton.onClick.AddListener(OnBackButtonClicked);
    }
    public void OnBackButtonClicked()
    {
        // Load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
