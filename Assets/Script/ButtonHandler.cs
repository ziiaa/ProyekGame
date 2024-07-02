using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public Button howToPlayButton;

    void Start()
    {
        if (howToPlayButton != null)
        {
            howToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
        }
    }

    void OnHowToPlayButtonClicked()
    {
        SceneManager.LoadScene("HowToPlay");
    }
}
