using UnityEngine;

public class Enemy : MonoBehaviour, Idamageable
{

    [SerializeField] private int hp = 100; // Health Points
    public float MS = 2f; // Movement Speed
    protected Transform player;

    [Header("Pengaturan State Machine")]
    [SerializeField] private float jarakDeteksi = 6f; // Jarak untuk mendeteksi player
    [SerializeField] private float jarakSerang = 1.2f; // Jarak untuk menyerang player
    [SerializeField] private float jedaSerang = 1f; // Jeda waktu antara serangan

    private StateZombie currentState = StateZombie.IDLE; // State awal
    private float waktuSerangTerakhir; // Waktu terakhir menyerang
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        // PeriksaTransisi();

        // switch (currentState)
        // {
        //     case StateZombie.IDLE:  PerilakuIdle(); break;
        //     case StateZombie.PATROL:  PerilakuPatrol(); break;
        //     case StateZombie.CHASE:  PerilakuChase(); break;
        //     case StateZombie.ATTACK:  PerilakuAttack(); break;
        // }
    }


    public void Kejar()

    {

        if (player == null) return;

        transform.position = Vector2.MoveTowards(

            transform.position,

            player.position,

            MS * Time.deltaTime

        );

    }

    public virtual void serang()
    {
        Debug.Log("Serang player");
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining HP: {hp}");

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);
    }

    public float JarakKePlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);
    }

    void PeriksaTransisi()
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

    void PerilakuIdle()
    {
        
    }

    void PerilakuPatrol()
    {
        // Implementasi perilaku saat dalam state PATROL
        Debug.Log("Zombie is patrolling.");
    }

    void PerilakuChase()
    {
        Kejar();
        // Implementasi perilaku saat dalam state CHASE
        Debug.Log("Zombie is chasing the player.");
    }

    void PerilakuAttack()
    {
        // Implementasi perilaku saat dalam state ATTACK
        Debug.Log("Zombie is attacking the player.");
        serang();
    }
}
