using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public PlantType plantData;
    public SpriteRenderer spriteRenderer;
    

    private int currentStage = 0;
    private float growthTimer = 0f;
    private bool isPlayerPresent = false;

    void Start()
    {
        UpdateSprite();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && isPlayerPresent && currentStage == plantData.cuttableStage)
        {
            Cut();
        }

        Grow();
    }

    void Grow()
    {
        if (currentStage < plantData.stageSprites.Length - 1)
        {
            growthTimer += Time.deltaTime;

            if (growthTimer >= plantData.stageDurations[currentStage])
            {
                currentStage++;
                growthTimer = 0f;
                UpdateSprite();

                
                if (currentStage == plantData.stageSprites.Length - 1)
                {
                    if (plantData.enemyToSpawn != null)
                    {
                        Instantiate(plantData.enemyToSpawn, transform.position, Quaternion.identity);
                        Debug.Log("Enemy spawned!");
                    }

                    
                }
            }
        }
    }

    void Cut()
    {
        currentStage = Mathf.Max(0, currentStage - 1);
        growthTimer = 0f;
        UpdateSprite();

        if (plantData.trimmingPrefab != null)
        {
            GameObject trimmings = Instantiate(plantData.trimmingPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = trimmings.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = Random.onUnitSphere;
                randomDirection.y = Mathf.Abs(randomDirection.y);
                float force = Random.Range(2f, 5f);
                rb.AddForce(randomDirection * force, ForceMode.Impulse);
            }
        }
    }

    void UpdateSprite()
    {
        if (spriteRenderer != null && plantData.stageSprites.Length > currentStage)
        {
            spriteRenderer.sprite = plantData.stageSprites[currentStage];
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerPresent = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerPresent = false;
        }
    }
}
