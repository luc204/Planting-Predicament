using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, int> items = new Dictionary<string, int>();
    public Dictionary<string, int> Items => items;
    public int Coins { get; private set; } = 0;

    public void AddItem(string PlantItem, int quantity = 1)
    {
        if (items.ContainsKey(PlantItem))
            items[PlantItem] += quantity;
        else
            items.Add(PlantItem, quantity);
        Debug.Log($"Added {quantity} {PlantItem}. Inventory: {GetInventoryString()}");
    }

    public void SellAllItems(int coinValuePerItem = 10)
    {
        int totalItems = 0;
        foreach (var item in items)
            totalItems += item.Value;

        int earnedCoins = totalItems * coinValuePerItem;
        Coins += earnedCoins;
        items.Clear();
        Debug.Log($"Sold {totalItems} items for {earnedCoins} coins. Total coins: {Coins}");
    }

    private string GetInventoryString()
    {
        string result = "";
        foreach (var item in items)
            result += $"{item.Key}: {item.Value}, ";
        return result.Length > 0 ? result.Substring(0, result.Length - 2) : "Empty";
    }
}