using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PageHome : MonoBehaviour
{
    public Button btn_howtoplay; // Tombol untuk How To Play
    public Button btn_play; // Tombol untuk Play

    void Start()
    {
        if (btn_howtoplay != null)
        {
            btn_howtoplay.onClick.AddListener(LoadHowToPlayScene);
        }

        if (btn_play != null)
        {
            btn_play.onClick.AddListener(OnPlayButtonClicked);
        }
    }

    void LoadHowToPlayScene()
    {
        Debug.Log("Loading HowToPlay Scene");
        SceneManager.LoadScene("HowToPlay");
    }

    void OnPlayButtonClicked()
    {
        Debug.Log("Loading Input_Name Scene");
        SceneManager.LoadScene("Input_Name");
    }

}
