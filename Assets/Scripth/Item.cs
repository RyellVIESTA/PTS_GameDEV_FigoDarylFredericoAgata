using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemDetails ItemDetails;
    private GrabableItem itemCollector;

    private GameManager gameManager;

    void Awake()
    {
        foreach (MonoBehaviour component in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None))
        {
            if (component is GrabableItem collector)
            {
                itemCollector = collector;
                break;
            }
        }

        gameManager = FindFirstObjectByType<GameManager>();

        if (itemCollector == null)
        {
            Debug.LogError("No active component implementing GrabableItem was found in the scene.", this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && ItemDetails != null && itemCollector != null)
        {
            itemCollector.AddItem(ItemDetails);

            // Hanya Coin yang dihitung sebagai Coin terkumpul
            if (!ItemDetails.IsSuperHeal && gameManager != null)
            {
                gameManager.AmbilKoin();
            }

            Destroy(gameObject);
        }
    }
}