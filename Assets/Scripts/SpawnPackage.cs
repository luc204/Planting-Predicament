using UnityEngine;

public class SpawnPackage : MonoBehaviour
{
    public GameObject package;
    public Vector3 spawnPosition = Vector3.zero;
    public bool canSpawn;
    public bool hasSpawned = false;

    void Start()
    {
        
        canSpawn = false;
    }
    void Update()
    {
        if (canSpawn && !hasSpawned)
        {
            spawnPackage();
            hasSpawned = true;
        }
    }

    void spawnPackage()
    {
        GameObject spawnedPackage = Instantiate(package, spawnPosition, Quaternion.identity);
        Destroy(spawnedPackage, 10f);
        Debug.Log("Package Spawned");
    }

    void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Car"))
        {
            canSpawn = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            canSpawn = false;
            hasSpawned = false;
        }
    }

}