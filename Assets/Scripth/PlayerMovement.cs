
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float kecepatan = 5f;

    private Vector2 arahGerak;
    private Animator animator;

    public int skor = 0;
    public GameManager gameManager;
    public TMP_Text skorText;

    // Arah terakhir agar animasi idle tetap menghadap arah terakhir.
    private Vector2 arahTerakhir = Vector2.down;
    private float idleTimer = 0f;
    public float idleDelay = 0.15f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnMove(InputValue value)
    {
        arahGerak = value.Get<Vector2>();
    }

    void Update()
    {
        // Gerakan Player
        Vector2 gerak = arahGerak.normalized;
        Vector3 arah = new Vector3(gerak.x, gerak.y, 0);
        transform.position += arah * kecepatan * Time.deltaTime;

        // Update arah animasi
        bool sedangBergerak = arahGerak.sqrMagnitude > 0.01f;

        if (sedangBergerak)
        {
            arahTerakhir = arahGerak;
            idleTimer = 0f;
        }
        else
        {
            idleTimer += Time.deltaTime;
        }

        animator.SetFloat("MoveX", arahTerakhir.x);
        animator.SetFloat("MoveY", arahTerakhir.y);

        // Masuk Idle hanya jika input berhenti cukup lama
        float animationSpeed = sedangBergerak || idleTimer < idleDelay ? 1f : 0f;
        animator.SetFloat("Speed", animationSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);

            skor++;
            Debug.Log("Skor: " + skor);
            skorText.text = "" + skor;

            gameManager.AmbilKoin();
        }
    }
}
