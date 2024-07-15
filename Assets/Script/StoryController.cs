using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    private string[] storyLines = {
        "Hai, teman-teman! Namaku Pu!", 
        "Selamat datang di Dunia Puzzle Pixel!",
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
            Invoke("ShowNextLine", 3f); // Menampilkan baris berikutnya setelah 4 detik
        }
        else
        {
            dialogText.text = "Ayo kita mulai petualangan kita!";
            Invoke("StartGame", 3f); // Mulai permainan setelah cerita selesai
        }
    }

    void StartGame()
    {
        SceneManager.LoadSceneAsync("Home");
    }
}
