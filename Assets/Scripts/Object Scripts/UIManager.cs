using UnityEngine;
using TMPro; // Use UnityEngine.UI for legacy Text

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI inventoryText; // Assign in Inspector
    public TextMeshProUGUI storePromptText; // Assign in Inspector
    private Inventory inventory;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        if (inventory == null)
        {
            Debug.LogWarning("Inventory not found!");
        }
        UpdateInventoryUI();
        SetStorePrompt(false);
    }

    public void UpdateInventoryUI()
    {
        if (inventory != null && inventoryText != null)
        {
            int plantItemCount = 0;
            inventory.Items.TryGetValue("PlantItem", out plantItemCount);
            inventoryText.text = $"Coins: {inventory.Coins}, PlantItem: {plantItemCount}";
        }
    }

    public void SetStorePrompt(bool isVisible)
    {
        if (storePromptText != null)
        {
            storePromptText.text = isVisible ? "Press E to sell items" : "";
        }
    }
}
