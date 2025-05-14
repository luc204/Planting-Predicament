using System.Collections;
using UnityEngine;

public class TimedDelivery : MonoBehaviour
{
    public DeliverySystemScript deliverySystem;
    public ItemData itemToDeliver;
    public float deliveryDelay = 60f; // 60 seconds

    void Start()
    {
        StartCoroutine(DeliverAfterDelay());
    }

    IEnumerator DeliverAfterDelay()
    {
        yield return new WaitForSeconds(deliveryDelay);

        if (deliverySystem != null && itemToDeliver != null)
        {
            deliverySystem.itemsToDeliver.Add(itemToDeliver);
            Debug.Log($"Item {itemToDeliver.itemName} added to delivery.");
        }
    }
}


