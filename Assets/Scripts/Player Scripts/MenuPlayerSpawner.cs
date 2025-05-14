using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 3f; 
    public float characterLifetime = 5f; 

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCharacter), 0f, spawnInterval);
    }

    void SpawnCharacter()
    {
        GameObject character = Instantiate(characterPrefab, spawnPoint.position, Quaternion.identity);
        Destroy(character, characterLifetime); 
    }
}

