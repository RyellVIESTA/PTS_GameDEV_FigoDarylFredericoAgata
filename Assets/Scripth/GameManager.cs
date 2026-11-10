using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Koin")]
    public int totalKoin;
    private int koinTerkumpul = 0;

    [Header("UI")]
    public TMP_Text TextMenang;
    public TMP_Text TextKalah;

    private bool gameEnded = false;

    private void Start()
    {
        totalKoin = GameObject.FindGameObjectsWithTag("Coin").Length;

        if (TextMenang != null)
            TextMenang.gameObject.SetActive(false);

        if (TextKalah != null)
            TextKalah.gameObject.SetActive(false);

        Time.timeScale = 1f;
    }

    public void AmbilKoin()
    {
        if (gameEnded) return;

        koinTerkumpul++;

        if (koinTerkumpul >= totalKoin)
            Menang();
    }

    public void Menang()
    {
        if (gameEnded) return;

        gameEnded = true;
        Debug.Log("KAMU MENANG!");

        if (TextMenang != null)
            TextMenang.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Kalah()
    {
        if (gameEnded) return;

        gameEnded = true;
        Debug.Log("KAMU KALAH!");

        if (TextKalah != null)
            TextKalah.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }
}