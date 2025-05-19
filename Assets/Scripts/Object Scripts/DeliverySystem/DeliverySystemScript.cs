using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliverySystemScript : MonoBehaviour
{
    public PlayerStats playerStats;

    public bool PlayerInRange = false;
    public List<ItemData> itemsToDeliver;
    public bool RemoveItemsAfterDelivery = true;
    public ItemData newPlantItem;

    void Update()
    {
        if (PlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Inventory2 inventory = FindObjectOfType<Inventory2>();

            if (inventory != null)
            {
                foreach (ItemData item in itemsToDeliver)
                {
                    inventory.AddItem(item);
                    Debug.Log($"Delivered: {item.itemName}");
                }

                if (RemoveItemsAfterDelivery)
                {
                    itemsToDeliver.Clear(); // So it doesn’t repeat
                }
            }
            else
            {
                Debug.LogWarning("Inventory not found!");
            }
        }
       
    }
    public void AddToDelivery()
    {
        if (newPlantItem != null)
        {
            itemsToDeliver.Add(newPlantItem);
            Debug.Log($"{newPlantItem.name} added to delivery system.");
        }
        else
        {
            Debug.LogWarning("Missing reference to delivery system or item.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
}


