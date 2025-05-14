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

    
    public int playerMoney = 0;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats != null)
        {
            playerMoney = playerStats.coins;
        }
        else
        {
            Debug.LogWarning("PlayerStats not found!");
        }
    }

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


