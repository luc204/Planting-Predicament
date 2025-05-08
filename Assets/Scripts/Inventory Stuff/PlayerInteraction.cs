using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject inventoryObject; // Drag the Inventory GameObject from your Canvas
    private Inventory2 inventory;

    private ItemPickup currentPickup;

    void Start()
    {
        inventory = inventoryObject.GetComponent<Inventory2>();
    }

    void Update()
    {
        if (currentPickup != null && Input.GetKeyDown(KeyCode.E))
        {
            inventory.AddItem(currentPickup.itemToGive);
            Destroy(currentPickup.gameObject);
            currentPickup = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ItemPickup>(out var pickup))
        {
            currentPickup = pickup;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ItemPickup>(out var pickup) && pickup == currentPickup)
        {
            currentPickup = null;
        }
    }
}


