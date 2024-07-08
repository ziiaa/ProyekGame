using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic();
    }

    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene != "Home" && currentScene != "HowToPlay" && currentScene != "Input_Name")
        {
            StopMusic();
        }
        else
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                PlayMusic();
            }
        }
    }

    public void PlayMusic()
    {
        GetComponent<AudioSource>().Play();
    }

    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }
}
