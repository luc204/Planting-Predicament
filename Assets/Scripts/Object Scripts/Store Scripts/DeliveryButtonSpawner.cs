using UnityEngine;

public class DeliveryButtonSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject deliveryCarPrefab;
    public Transform spawnPoint;

    [Header("Delivery Settings")]
    public ItemData[] seedOptions; // Seeds to choose from

    public void SpawnDeliveryCar(ItemData itemToDeliver)
    {
        if (deliveryCarPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("Missing car prefab or spawn point.");
            return;
        }

        GameObject car = Instantiate(deliveryCarPrefab, spawnPoint.position, spawnPoint.rotation);
        DeliveryCar carScript = car.GetComponent<DeliveryCar>();

        if (carScript != null && seedOptions != null && seedOptions.Length > 0)
        {
            int randomIndex = Random.Range(0, seedOptions.Length);
            carScript.cargoItem = seedOptions[randomIndex];
        }
        else
        {
            Debug.LogWarning("Missing DeliveryCar script or seed options.");
        }
    }
}
