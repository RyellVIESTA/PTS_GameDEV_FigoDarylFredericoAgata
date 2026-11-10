
using UnityEngine;

public class Enemy : MonoBehaviour, Idamageable
{
    [Header("Enemy Stats")]
    [SerializeField] private int hp = 100;
    [SerializeField] private int damage = 20;
    public float MS = 2f;

    [Header("Detection")]
    [SerializeField] private float jarakDeteksi = 6f;
    [SerializeField] private float jarakSerang = 1.2f;
    [SerializeField] private float jedaSerang = 1f;

    [Header("Explosion")]
    [SerializeField] private GameObject explosionPrefab;
    private bool sudahMeledak = false;

    [Header("Spawner")]
    private EnemySpawner enemySpawner;

    private Transform player;
    private Idamageable targetDamageable;
    private Animator animator;

    private StateZombie currentState = StateZombie.IDLE;
    private float waktuSerangTerakhir;

    // Variabel animasi
    private Vector2 arahTerakhir = Vector2.down;
    private float idleTimer = 0f;
    public float idleDelay = 0.15f;

    // Menghubungkan enemy dengan spawner
    public void SetSpawner(EnemySpawner spawner)
    {
        enemySpawner = spawner;
    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
            targetDamageable =
                playerObject.GetComponent<Idamageable>();
        }
        else
        {
            Debug.LogWarning(
                "Player dengan tag 'Player' tidak ditemukan."
            );
        }
    }

    private void Update()
    {
        if (sudahMeledak) return;

        PeriksaTransisi();

        switch (currentState)
        {
            case StateZombie.IDLE:
                PerilakuIdle();
                break;

            case StateZombie.PATROL:
                PerilakuPatrol();
                break;

            case StateZombie.CHASE:
                PerilakuChase();
                break;

            case StateZombie.ATTACK:
                PerilakuAttack();
                break;
        }
    }

    public void Kejar()
    {
        if (player == null) return;

        Vector2 posisiAwal = transform.position;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            MS * Time.deltaTime
        );

        Vector2 arahGerak =
            (Vector2)transform.position - posisiAwal;

        bool sedangBergerak =
            arahGerak.sqrMagnitude > 0.0001f;

        if (sedangBergerak)
        {
            arahTerakhir = arahGerak.normalized;
            idleTimer = 0f;
        }
        else
        {
            idleTimer += Time.deltaTime;
        }

        AturAnimasi(sedangBergerak);

        Debug.Log("Enemy bergerak: " + sedangBergerak);
    }

    private void AturAnimasi(bool sedangBergerak)
    {
        if (animator == null) return;

        animator.SetFloat("movex", arahTerakhir.x);
        animator.SetFloat("movey", arahTerakhir.y);

        float animationSpeed =
            sedangBergerak || idleTimer < idleDelay ? 1f : 0f;

        animator.SetFloat("speed", animationSpeed);
    }

    public virtual void serang()
    {
        if (targetDamageable == null || sudahMeledak) return;

        if (Time.time >= waktuSerangTerakhir + jedaSerang)
        {
            targetDamageable.TakeDamage(damage);
            waktuSerangTerakhir = Time.time;

            Meledak();
        }
    }

    private void Meledak()
    {
        if (sudahMeledak) return;

        sudahMeledak = true;

        if (explosionPrefab != null)
        {
            Instantiate(
                explosionPrefab,
                transform.position,
                Quaternion.identity
            );
        }

        // Spawn enemy baru setelah meledak
        if (enemySpawner != null)
        {
            enemySpawner.SpawnEnemy();
        }
        else
        {
            Debug.LogWarning("EnemySpawner belum terhubung!");
        }

        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if (sudahMeledak) return;

        hp -= damage;

        Debug.Log(
            $"{gameObject.name} terkena {damage} damage. Sisa HP: {hp}"
        );

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} mati.");
        Destroy(gameObject);
    }

    public float JarakKePlayer()
    {
        if (player == null)
            return Mathf.Infinity;

        return Vector2.Distance(
            transform.position,
            player.position
        );
    }

    private void PeriksaTransisi()
    {
        float jarak = JarakKePlayer();

        if (jarak <= jarakSerang)
        {
            currentState = StateZombie.ATTACK;
        }
        else if (jarak <= jarakDeteksi)
        {
            currentState = StateZombie.CHASE;
        }
        else
        {
            currentState = StateZombie.PATROL;
        }
    }

    private void PerilakuIdle()
    {
        idleTimer += Time.deltaTime;
        AturAnimasi(false);
    }

    private void PerilakuPatrol()
    {
        idleTimer += Time.deltaTime;
        AturAnimasi(false);
    }

    private void PerilakuChase()
    {
        Kejar();
    }

    private void PerilakuAttack()
    {
        if (player != null)
        {
            Vector2 arahPlayer =
                (Vector2)(player.position - transform.position);

            if (arahPlayer.sqrMagnitude > 0.01f)
                arahTerakhir = arahPlayer.normalized;
        }

        idleTimer += Time.deltaTime;
        AturAnimasi(false);

        serang();
    }
}
