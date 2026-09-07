using UnityEngine;
using UnityEngine.InputSystem; // WAJIB untuk Input System\
using TMPro; 
public class PlayerMovement : MonoBehaviour

{
    public float kecepatan = 5f;
    private Vector2 arahGerak; // nilai dari action "Move"
        // Dipanggil OTOMATIS oleh komponen Player Input
        // saat action "Move" pada asset InputSystem_Actions aktif.
        // Nama method WAJIB: On + nama action -> OnMove
    public int skor = 0;
    public GameManager gameManager;
    public TMP_Text skorText; 
    

    void OnMove(InputValue value)
    {
        // Mengambil nilai input Vector2
        arahGerak = value.Get<Vector2>();
    }
    void Update()
    {
        // Menggerakkan objek
        Vector3 arah = new Vector3(arahGerak.x, arahGerak.y, 0);
        transform.position += arah * kecepatan * Time.deltaTime;
    }
    // Dipanggil otomatis saat Player menyentuh objek ber-Trigger
    void OnTriggerEnter2D(Collider2D other)
    {
    if (other.CompareTag("Coin"))
    {
        Destroy(other.gameObject);

        skor++;
        Debug.Log("Skor: " + skor);
        skorText.text = "" + skor;

        // Beri tahu GameManager bahwa satu koin telah diambil
        gameManager.AmbilKoin();
    }
    }
}