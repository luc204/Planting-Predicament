using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemToGive;
    public bool IsPlayerPresent = false;
    private Inventory2 inventory;


    void Start()
    {
        GameObject inventoryObject = GameObject.FindWithTag("Inventory");
        if (inventoryObject != null)
        {
            inventory = inventoryObject.GetComponent<Inventory2>();
        }
    }

    private void Update()
    {
        if (IsPlayerPresent = true && Input.GetKeyDown(KeyCode.E))
        {
            if (inventory != null)
            {
                inventory.AddItem(itemToGive);
                Destroy(gameObject);
            }
        }
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventory = collision.GetComponent<Inventory2>();
            IsPlayerPresent = true;
            Debug.Log("Player can pickup item");
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerPresent = false;
            Debug.Log("Player can no longer pick up item");
        }
    }
}

