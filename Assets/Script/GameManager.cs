using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int player1Score;
    public int player2Score;

    void Awake()
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
}
