
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Data item
    public string itemName;
    public int itemQuantity;
    public Sprite itemIcon;
    public bool isFull;

    // Data yang dibutuhkan InventoryManager
    public ItemDetails itemDetails;
    public bool isEmpty => !isFull;

    // Komponen UI
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private int MaskStackState = 90;

    // Pengelolaan slot
    public GameObject selectedPanel;
    public bool isItemSelected;
    public InventoryManager inventoryManager;

    private void Start()
    {
        if (!isFull)
        {
            if (quantityText != null)
                quantityText.enabled = false;

            if (itemImage != null)
                itemImage.enabled = false;
        }

        if (selectedPanel != null)
            selectedPanel.SetActive(false);
    }

    public void AddItem(ItemDetails item)
    {
        if (item == null)
            return;

        itemDetails = item;
        itemName = item.ItemName;
        itemQuantity = Mathf.Min(item.Quantity, MaskStackState);
        itemIcon = item.ItemIcon;
        isFull = itemQuantity > 0;

        UpdateSlotUI();
    }

    public bool CanStackWith(string otherItemName)
    {
        return isFull
            && itemName == otherItemName
            && itemQuantity < MaskStackState;
    }

    public void AddItemQuantity(int quantity)
    {
        if (!isFull || quantity <= 0)
            return;

        itemQuantity = Mathf.Min(
            itemQuantity + quantity,
            MaskStackState
        );

        UpdateSlotUI();
    }

    private void UpdateSlotUI()
    {
        if (quantityText != null)
        {
            quantityText.text = itemQuantity.ToString();
            quantityText.enabled = isFull;
        }

        if (itemImage != null)
        {
            itemImage.sprite = itemIcon;
            itemImage.enabled = isFull && itemIcon != null;
            itemImage.color = Color.yellow;
        }

        if (!isFull)
        {
            itemName = "";
            itemDetails = null;
            itemIcon = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClick();
        }
    }


    private void LeftClick()
    {
        if (inventoryManager == null) return;

        // Klik kiri hanya menampilkan detail
        inventoryManager.selectSlot(this);
    }

    private void RightClick()
    {
        if (isEmpty)
            return;

        // Klik kanan menggunakan item
        UseItem();
    }
    private void UseItem()
    {
        if (itemDetails == null)
            return;

        if (itemDetails.IsSuperHeal)
        {
            PlayerHealth playerHealth = FindFirstObjectByType<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.HealFull();
                Debug.Log("Super Heal digunakan!");

                itemQuantity--;

                if (itemQuantity <= 0)
                {
                    isFull = false;
                    itemQuantity = 0;
                    itemName = "";
                    itemDetails = null;
                    itemIcon = null;
                }

                UpdateSlotUI();
            }
        }
    }
}
