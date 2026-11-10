using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, Idamageable
{
    [Header("Health")]
    [SerializeField] private int maxHP = 100;
    private int currentHP;

    [Header("UI")]
    [SerializeField] private Slider healthBar;

    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;

    private bool isDead = false;

    private void Start()
    {
        currentHP = maxHP;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHP;
            healthBar.value = currentHP;
        }

        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        if (healthBar != null)
            healthBar.value = currentHP;

        Debug.Log("Player terkena damage. HP: " + currentHP);

        if (currentHP <= 0)
            Die();
    }
    public void HealFull()
    {
        if (isDead) return;

        currentHP = maxHP;

        if (healthBar != null)
            healthBar.value = currentHP;

        Debug.Log("Super Heal digunakan. HP kembali menjadi " + currentHP);
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Player mati.");

        if (gameManager != null)
        {
            gameManager.Kalah();
        }
        else
        {
            Debug.LogError("GameManager belum ditemukan!");
        }
    }

}