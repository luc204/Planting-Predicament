using UnityEngine;

public class DeliverySystem : MonoBehaviour
{
    [Header("Car Spawn Settings")]
    public GameObject deliveryCarPrefab;
    public Transform spawnPoint;

    [Header("Delivery Settings")]
    public ItemData[] possibleSeeds; // Array of seeds to randomly choose from
    public float spawnInterval = 30f; // How often a car spawns
    private float timer;

    void Start()
    {
        timer = spawnInterval; // So the first car spawns immediately or after delay
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnCar();
            timer = spawnInterval; // reset timer
        }
    }

    void SpawnCar()
    {
        if (deliveryCarPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("DeliverySystem missing prefab or spawn point");
            return;
        }

        GameObject carInstance = Instantiate(deliveryCarPrefab, spawnPoint.position, spawnPoint.rotation);

        DeliveryCar carScript = carInstance.GetComponent<DeliveryCar>();

        if (carScript != null)
        {
            // Choose a random seed from possibleSeeds
            if (possibleSeeds != null && possibleSeeds.Length > 0)
            {
                int randomIndex = Random.Range(0, possibleSeeds.Length);
                carScript.cargoItem = possibleSeeds[randomIndex];
            }
            else
            {
                Debug.LogWarning("No seeds assigned in DeliverySystem.possibleSeeds");
            }
        }
        else
        {
            Debug.LogWarning("DeliveryCar component not found on prefab.");
        }
    }
}

