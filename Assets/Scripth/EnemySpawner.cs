
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn Area")]
    [SerializeField] private Vector2 areaMin;
    [SerializeField] private Vector2 areaMax;

    [Header("Spawn Distance")]
    [SerializeField] private float jarakMinimal = 5f;

    private Transform player;

    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player tidak ditemukan!");
            return;
        }

        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if (enemyPrefab == null || player == null)
            return;

        Vector2 posisiAcak = Vector2.zero;
        bool posisiValid = false;

        for (int i = 0; i < 100; i++)
        {
            posisiAcak = new Vector2(
                Random.Range(areaMin.x, areaMax.x),
                Random.Range(areaMin.y, areaMax.y)
            );

            float jarak = Vector2.Distance(
                posisiAcak,
                player.position
            );

            if (jarak >= jarakMinimal)
            {
                posisiValid = true;
                break;
            }
        }

        if (!posisiValid)
        {
            Debug.LogWarning(
                "Tidak menemukan posisi spawn yang cukup jauh dari player!"
            );
            return;
        }

        GameObject enemy = Instantiate(
            enemyPrefab,
            posisiAcak,
            Quaternion.identity
        );

        Enemy enemyScript = enemy.GetComponent<Enemy>();

        if (enemyScript != null)
        {
            enemyScript.SetSpawner(this);
        }
    }
}
