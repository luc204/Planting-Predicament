using System.Collections.Generic;
using UnityEngine;

public class Inventory2 : MonoBehaviour
{
    public int currentIndex;
    int maxIndex;

    
    public PlayerStats playerStats;

    public ItemSlot[] itemSlots;

    void Start()
    {
        if (itemSlots.Length == 0)
        {
            itemSlots = GetComponentsInChildren<ItemSlot>();
        }

        foreach (var slot in itemSlots)
        {
            slot.Init();
        }

        maxIndex = itemSlots.Length;

        
    }

    void Update()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].index = i;
            itemSlots[i].selected = i == currentIndex;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            currentIndex = (currentIndex + 1) % maxIndex;
        }
        else if (scroll < 0f)
        {
            currentIndex = (currentIndex - 1 + maxIndex) % maxIndex;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropSelectedItem();
        }
    }

    public void AddItem(ItemData item)
    {
        // Stack if already present
        foreach (var slot in itemSlots)
        {
            if (slot.itemInSlot == item)
            {
                slot.itemCount++;
                slot.itemCountText.text = slot.itemCount.ToString();
                return;
            }
        }

        // Add to new slot
        foreach (var slot in itemSlots)
        {
            if (slot.itemInSlot == null)
            {
                slot.itemInSlot = item;
                slot.itemCount = 1;

                slot.SpriteImage.sprite = item.itemSprite;
                slot.SpriteImage.enabled = true;

                slot.itemCountText.text = "1";
                slot.itemCountText.enabled = true;

                return;
            }
        }

        Debug.LogWarning("Inventory full — couldn't add item.");
    }

    public void RemoveItem(ItemData item)
    {
        foreach (var slot in itemSlots)
        {
            if (slot.itemInSlot == item)
            {
                slot.itemCount--;

                if (slot.itemCount <= 0)
                {
                    slot.itemInSlot = null;
                    slot.itemCount = 0;
                    slot.SpriteImage.enabled = false;
                    slot.itemCountText.enabled = false;
                }
                else
                {
                    slot.itemCountText.text = slot.itemCount.ToString();
                }

                return;
            }
        }
    }
    public void SellSelectedItem()
    {
        if (currentIndex < 0 || currentIndex >= itemSlots.Length)
            return;

        ItemSlot selectedSlot = itemSlots[currentIndex];

        if (selectedSlot.itemInSlot != null && selectedSlot.itemCount > 0)
        {
            int itemValue = selectedSlot.itemInSlot.sellValue;
            int totalCoins = selectedSlot.itemCount * itemValue;

            // Add coins to player
            if (playerStats != null)
            {
                playerStats.AddCoins(totalCoins);
            }

            Debug.Log($"Sold {selectedSlot.itemCount} x {selectedSlot.itemInSlot.itemName} for {totalCoins} coins.");

            // Clear the slot
            selectedSlot.itemInSlot = null;
            selectedSlot.itemCount = 0;
            selectedSlot.SpriteImage.enabled = false;
            selectedSlot.itemCountText.enabled = false;
        }
        else
        {
            Debug.Log("No item in selected slot to sell.");
        }
    }

    public void DropSelectedItem()
    {
        if (currentIndex < 0 || currentIndex >= itemSlots.Length)
            return;

        ItemSlot selectedSlot = itemSlots[currentIndex];

        if (selectedSlot.itemInSlot != null && selectedSlot.itemCount > 0)
        {
            GameObject prefab = selectedSlot.itemInSlot.pickupPrefab;

            if (prefab != null)
            {

                Vector3 dropPosition = transform.position + transform.forward + Vector3.up * 0.5f; // Slightly in front and above the player
                GameObject droppedItem = Instantiate(prefab, dropPosition, Quaternion.identity);
                droppedItem.transform.SetParent(null);

                Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(transform.forward * 2f + Vector3.up * 1f, ForceMode.Impulse);
                }
            }
            else
            {
                Debug.LogWarning("No pickupPrefab assigned to " + selectedSlot.itemInSlot.name);
            }

            // Remove one from inventory
            selectedSlot.itemCount--;

            if (selectedSlot.itemCount <= 0)
            {
                selectedSlot.itemInSlot = null;
                selectedSlot.itemCount = 0;
                selectedSlot.SpriteImage.enabled = false;
                selectedSlot.itemCountText.enabled = false;
            }
            else
            {
                selectedSlot.itemCountText.text = selectedSlot.itemCount.ToString();
            }
        }
        else
        {
            Debug.Log("No item to drop.");
        }
    }

}
