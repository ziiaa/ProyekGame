using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnHandler : MonoBehaviour
{
    public Button backButton;

    void Start()
    {
        if (backButton != null)
        {
            backButton.onClick.AddListener(OnbackButtonClicked);
        }
    }

    void OnbackButtonClicked()
    {
        SceneManager.LoadScene("Home");
    }
}
