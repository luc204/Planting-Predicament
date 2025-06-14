using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemData itemData;
    private bool isPickupable = false;
    private Inventory2 nearbyInventory;

    public void Init(ItemData data)
    {
        itemData = data;
    }

    void Update()
    {
        if (isPickupable && Input.GetKeyDown(KeyCode.E))
        {
            if (nearbyInventory != null && itemData != null)
            {
                nearbyInventory.AddItem(itemData);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPickupable = true;
            nearbyInventory = other.GetComponent<Inventory2>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPickupable = false;
            nearbyInventory = null;
        }
    }
}


