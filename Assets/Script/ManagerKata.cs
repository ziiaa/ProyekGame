using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ManagerKata : MonoBehaviour
{
    public static ManagerKata Instance { get; private set; }
    [SerializeField] DragScript hurufPrefab;
    [SerializeField] Transform slotAwal, slotAkhir;
    [SerializeField] string[] listKatakata;

    private int poinKata, poin;
    private int currentWordIndex = 0;
    private int totalWordsToSolve = 5;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (listKatakata == null || listKatakata.Length == 0)
        {
            Debug.LogError("listKatakata is not assigned or is empty.");
            return;
        }

        IntKata(listKatakata[currentWordIndex]);
    }

    public void IntKata(string kata)
    {
        foreach (Transform child in slotAwal)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in slotAkhir)
        {
            Destroy(child.gameObject);
        }

        char[] hurufkata = kata.ToCharArray();
        char[] hurufAcak = new char[hurufkata.Length];

        List<char> hurufKataCopy = hurufkata.ToList();

        for (int i = 0; i < hurufkata.Length; i++)
        {
            int randomIndex = Random.Range(0, hurufKataCopy.Count);
            hurufAcak[i] = hurufKataCopy[randomIndex];
            hurufKataCopy.RemoveAt(randomIndex);

            DragScript temp = Instantiate(hurufPrefab, slotAwal);
            temp.Inisialisasi(slotAwal, hurufAcak[i].ToString(), false);
        }

        for (int i = 0; i < hurufkata.Length; i++)
        {
            DragScript temp = Instantiate(hurufPrefab, slotAkhir);
            temp.Inisialisasi(slotAkhir, hurufkata[i].ToString(), true);
        }

        poinKata = hurufkata.Length;
        poin = 0;
    }

    public void TambahPoin()
    {
        poin++;

        if (poin == poinKata)
        {
            Debug.Log("Susunan Berhasil");
            KataKami.Instance.AddScore();

            currentWordIndex++;
            if (currentWordIndex < totalWordsToSolve && currentWordIndex < listKatakata.Length)
            {
                IntKata(listKatakata[currentWordIndex]);
            }
            else
            {
                KataKami.Instance.EndGame();
            }
        }
    }

    public void ResetKataKami()
    {
        currentWordIndex = 0;
        if (listKatakata != null && listKatakata.Length > 0)
        {
            IntKata(listKatakata[currentWordIndex]);
        }
    }
}
