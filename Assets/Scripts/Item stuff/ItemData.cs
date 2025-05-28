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
        Plant,
        Item,
        Tool,
        DontUse
    }
    public string plantName;
    public Sprite[] stageSprites;
    public GameObject[] stageModels;
    public float[] stageDurations;
    public int cuttableStage = 3;

    public GameObject enemyToSpawn;
    public GameObject trimmingPrefab;

}
