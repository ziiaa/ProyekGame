using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryPu : MonoBehaviour
{
    public Text dialogText;

    private string[] storyLines = {
        "Hai, teman-teman! Namaku Pu! Selamat datang di Dunia Fantasi Puzzle!",
        "Di sini, kita akan memasuki petualangan penuh warna dan keajaiban.",
        "Aku akan mengajak kalian untuk bermain tebak potongan puzzle huruf.",
        "Setiap huruf yang kalian susun dengan benar akan membuka rahasia dunia ini.",
        "Siapkah kalian untuk memulai petualangan ajaib ini? Ayo kita mulai!"
    };

    private int currentLineIndex = 0;

    void Start()
    {
        ShowNextLine();
    }

    void ShowNextLine()
    {
        if (currentLineIndex < storyLines.Length)
        {
            dialogText.text = storyLines[currentLineIndex];
            currentLineIndex++;
            Invoke("ShowNextLine", 3f); // Menampilkan baris berikutnya setelah 3 detik
        }
        else
        {
            dialogText.text = "Ayo kita mulai petualangan kita!";
            Invoke("StartGame", 3f); // Mulai permainan setelah cerita selesai
        }
    }

    void StartGame()
    {
        LoadHomeScene();
    }

    void LoadHomeScene()
    {
        Debug.Log("Loading Home scene synchronously...");
        SceneManager.LoadScene("Home");
    }
}
