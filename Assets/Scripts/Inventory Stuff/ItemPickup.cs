using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemToGive;
    private bool isPlayerNearby = false;
    private Inventory2 playerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerInventory = other.GetComponentInChildren<Inventory2>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            playerInventory = null;
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory != null)
            {
                playerInventory.AddItem(itemToGive);
                Destroy(gameObject);
            }
        }
    }
}
