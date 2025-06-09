using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab; 
    public Vector3 spawnPosition = Vector3.zero;
    public bool canSpawn;

    void Start()
    {
        SpawnCar();
    }
    private void Update()
    {
        if (canSpawn)
        {
            SpawnCar();
        }
    }

    void SpawnCar()
    {
        GameObject spawnedCar = Instantiate(carPrefab, spawnPosition, Quaternion.identity);
        Destroy(spawnedCar,10f);
    }
}

