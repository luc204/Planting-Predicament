using UnityEngine;

public class DeliveryBox : MonoBehaviour
{
    public ItemData itemToGive; 
    private bool playerInRange = false;
    
    

    public void Init(ItemData data)
    {
        itemToGive = data;
       
    }
    

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (Inventory2.Instance != null && itemToGive != null)
            {
                Inventory2.Instance.AddItem(itemToGive);
                Debug.Log("Added " + itemToGive.itemName + " to inventory.");
                Destroy(gameObject); 
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            
        }
    }
}

