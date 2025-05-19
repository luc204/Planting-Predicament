using System.Collections;
using UnityEngine;

public class TimedDelivery : MonoBehaviour
{
    public DeliverySystemScript deliverySystem;
    public ItemData AppleSeeds;
    public ItemData WheatSeeds;
    public ItemData PumpkinSeeds;
    public float deliveryDelay = 5f;

    public int deliveryCoinThreshold = 10;
    private int lastDeliveryCoins = 0;

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        StartCoroutine(DeliverWheatAfterDelay());
    }

    IEnumerator DeliverWheatAfterDelay()
    {
        yield return new WaitForSeconds(deliveryDelay);

        if (deliverySystem != null && WheatSeeds != null)
        {
            deliverySystem.itemsToDeliver.Add(WheatSeeds);
            Debug.Log($"Item {WheatSeeds.itemName} added to delivery.");
        }
    }

    void Update()
    {
        if (playerStats != null)
        {
            int coins = playerStats.GetCoins();

            if (coins >= lastDeliveryCoins + deliveryCoinThreshold)
            {

                DeliverPumpkinSeeds();
                lastDeliveryCoins = coins; 
            }
        }
    }

    public void DeliverPumpkinSeeds()
    {
        if (deliverySystem != null && PumpkinSeeds != null)
        {
            deliverySystem.itemsToDeliver.Add(PumpkinSeeds);
            Debug.Log($"Item {PumpkinSeeds.itemName} added to delivery.");
        }
    }

    public void DeliverAppleSeeds()
    {
        if (deliverySystem != null && AppleSeeds != null)
        {
            deliverySystem.itemsToDeliver.Add(AppleSeeds);
            Debug.Log($"Item {AppleSeeds.itemName} added to delivery.");
        }
    }
}
