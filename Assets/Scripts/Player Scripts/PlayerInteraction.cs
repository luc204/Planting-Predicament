using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject inventoryObject; 
    private Inventory2 inventory;

   
    

    private ItemPickup currentPickup;
    public bool CanPlaceHere = false;
    public PlantScript2 currentPlantSpot;
    public bool CanPlaceUp = false;

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
        
        if (Input.GetKeyDown(KeyCode.E) && currentPlantSpot != null && CanPlaceHere)
        {
            if (inventory != null)
            {
                inventory.TryPlantAt(currentPlantSpot);
                Debug.Log("Attempted to plant at spot.");
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ItemPickup>(out var pickup))
        {
            currentPickup = pickup;
        }
        if (other.CompareTag("PlantingSpot"))
        {
            CanPlaceHere = true;

            Debug.Log("Can place here: " + CanPlaceHere);
        }

        if (other.CompareTag("PlantingSpot"))
        {
            currentPlantSpot = other.GetComponent<PlantScript2>();

            Debug.Log("can see planting spot");
            if (currentPlantSpot != null && currentPlantSpot.IsPlanted)
            {
                Debug.Log("Planting spot is already occupied.");
                currentPlantSpot = null; 
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ItemPickup>(out var pickup) && pickup == currentPickup)
        {
            currentPickup = null;
        }

        if (other.CompareTag("PlantingSpot"))
        {
            CanPlaceHere = false;
        }
        if (other.CompareTag("PlantingSpot"))
        {
            currentPlantSpot = other.GetComponent<PlantScript2>();
        }
    }
   

}


