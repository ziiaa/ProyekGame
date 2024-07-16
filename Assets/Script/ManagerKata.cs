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
    private int totalWordsToSolve = 5; // Number of words to solve

    void Start()
    {
        Instance = this;

        // Ensure listKatakata is not null or empty
        if (listKatakata == null || listKatakata.Length == 0)
        {
            Debug.LogError("listKatakata is not assigned or is empty.");
            return;
        }

        IntKata(listKatakata[currentWordIndex]);
    }

    void IntKata(string kata)
    {
        // Clear previous word slots
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
            temp.Inisialisasi(slotAwal, hurufAcak[i].ToString(), false); // Corrected Inisialisasi call
        }

        for (int i = 0; i < hurufkata.Length; i++)
        {
            DragScript temp = Instantiate(hurufPrefab, slotAkhir);
            temp.Inisialisasi(slotAkhir, hurufkata[i].ToString(), true); // Corrected Inisialisasi call
        }

        poinKata = hurufkata.Length;
        poin = 0; // Reset points for the new word
    }

    public void TambahPoin()
    {
        poin++;

        if (poin == poinKata)
        {
            Debug.Log("Susunan Berhasil");
            KataKami.Instance.AddScore(); // Add this line to add score

            currentWordIndex++;
            if (currentWordIndex < totalWordsToSolve && currentWordIndex < listKatakata.Length)
            {
                IntKata(listKatakata[currentWordIndex]);
            }
            else
            {
                KataKami.Instance.EndGame(); // End the game if all words are solved
            }
        }
    }
}