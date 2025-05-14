using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPackage : MonoBehaviour
{
    public DeliverySystemScript deliverySystemScript;
    public ItemData newPlantItem;

    public void AddToDelivery()
    {
        if (deliverySystemScript != null && newPlantItem != null)
        {
            deliverySystemScript.itemsToDeliver.Add(newPlantItem);
            Debug.Log($"{newPlantItem.name} added to delivery system.");
        }
        else
        {
            Debug.LogWarning("Missing reference to delivery system or item.");
        }
    }
}

