using UnityEngine;

public class Store : MonoBehaviour
{
    private bool isPlayerPresent = false;

    void Update()
    {
        // Sell items when player presses 'E' in store area
        if (isPlayerPresent && Input.GetKeyDown(KeyCode.E))
        {
            InventoryScript inventory = FindObjectOfType<InventoryScript>();
            if (inventory != null)
            {
                inventory.SellAllItems(10); // 10 coins per item
            }
            else
            {
                Debug.LogWarning("Inventory not found!");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerPresent = true;
            Debug.Log("Player entered store. Press E to sell items.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerPresent = false;
            Debug.Log("Player left store.");
        }
    }
}
