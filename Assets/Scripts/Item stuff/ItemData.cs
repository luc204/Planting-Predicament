using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public int sellValue;
    
    public GameObject pickupPrefab;
    public GameObject PlaceablePrefab;

    public ItemType itemType;
    public enum ItemType
    {
        Placeable,
        Dropable,
        Tool
    }


}
