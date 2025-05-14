using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject inventoryObject; // Drag the Inventory GameObject from your Canvas
    private Inventory2 inventory;
    
    private ItemPickup currentPickup;
    public bool CanPlaceHere = false;

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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            inventory.DropSelectedItem(transform);
        }
        if (Input.GetKeyDown(KeyCode.E) && CanPlaceHere)
        {
            inventory.PlaceSelectedItem(transform); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ItemPickup>(out var pickup))
        {
            currentPickup = pickup;
        }
        if (other.CompareTag("Placeable"))
        {
            CanPlaceHere = true;

            Debug.Log("Can place here: " + CanPlaceHere);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ItemPickup>(out var pickup) && pickup == currentPickup)
        {
            currentPickup = null;
        }

        if (other.CompareTag("Placeable"))
        {
            CanPlaceHere = false;
        }
    }
  
}


