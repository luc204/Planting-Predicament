using UnityEngine;
using System.Collections;


public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public Vector3 spawnPosition = Vector3.zero;

    void Start()
    {
        StartCoroutine(SpawnCarIE());
    }

    IEnumerator SpawnCarIE()
    {
        while (true)
        {
            SpawnCar();
            yield return new WaitForSeconds(30f);
        }
    }

    void SpawnCar()
    {
        GameObject spawnedCar = Instantiate(carPrefab, spawnPosition, Quaternion.identity);
        Destroy(spawnedCar, 10f); 
        Debug.Log("Car spawned!");
    }
}


