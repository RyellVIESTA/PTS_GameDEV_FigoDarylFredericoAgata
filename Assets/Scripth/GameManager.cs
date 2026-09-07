using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int totalKoin;
    private int koinTerkumpul = 0;
    public TMP_Text TextMenang;

    void Start()
    {
        // Hitung jumlah koin di scene saat mulai
        totalKoin = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    public void AmbilKoin()
    {
        koinTerkumpul++;

        // Jika semua koin sudah diambil
        if (koinTerkumpul == totalKoin)
            Menang();
    }

    void Menang()
    {
        Debug.Log("KAMU MENANG!");
        TextMenang.gameObject.SetActive(true);
    }
}