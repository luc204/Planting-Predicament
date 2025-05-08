using UnityEngine;

public class Inventory2 : MonoBehaviour
{
    public int currentIndex;
    int maxIndex;
   
    public int playerCoins = 0;
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
    public void SellAllItems(int pricePerItem)
    {
        int totalSold = 0;

        foreach (var slot in itemSlots)
        {
            if (slot.itemInSlot != null && slot.itemCount > 0)
            {
                totalSold += slot.itemCount;

                slot.itemInSlot = null;
                slot.itemCount = 0;
                slot.SpriteImage.enabled = false;
                slot.itemCountText.enabled = false;
            }
        }

        int totalCoins = totalSold * pricePerItem;

        if (playerStats != null)
        {
            playerStats.AddCoins(totalCoins);
        }

        Debug.Log($"Sold {totalSold} items for {totalCoins} coins.");
    }
}
