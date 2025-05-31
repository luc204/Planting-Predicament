using UnityEngine;

public class PlantScript2 : MonoBehaviour
{
    public ItemData plantData;
    public Transform visualHolder;
    private GameObject currentVisual;
    public bool IsPlanted { get; private set; } = false;

    private int currentStage = 0;
    private float growthTimer = 0f;
    private bool isPlayerPresent = false;


    void Start()
    {
        UpdateStageVisual();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && isPlayerPresent && currentStage == plantData.cuttableStage)
        {
            Cut();
        }
        if (IsPlanted)
        {
            Grow();
        }
    }

    void Grow()
    {
        if (plantData != null && plantData.stageModels != null && plantData.stageModels.Length > 0)
        {
            if (currentStage < plantData.stageModels.Length - 1)
            {
                growthTimer += Time.deltaTime;

                if (growthTimer >= plantData.stageDurations[currentStage])
                {
                    currentStage++;
                    growthTimer = 0f;
                    UpdateStageVisual();

                    if (currentStage == plantData.stageModels.Length - 1 && plantData.enemyToSpawn != null)
                    {
                        Instantiate(plantData.enemyToSpawn, transform.position, Quaternion.identity);
                    }
                }
            }
        }
    }

    void Cut()
    {
        currentStage = Mathf.Max(0, currentStage - 1);
        growthTimer = 0f;
        UpdateStageVisual();

        if (plantData.trimmingPrefab != null)
        {
            GameObject trimmings = Instantiate(plantData.trimmingPrefab, transform.position + Vector3.up * 1, Quaternion.identity);
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

    public void SetPlantData(ItemData data)
    {
        if (IsPlanted || data == null) return;

        plantData = data;
        IsPlanted = true;
        currentStage = 0;
        growthTimer = 0f;

        UpdateStageVisual();
    }

    void UpdateStageVisual()

    {
        if (currentVisual != null)
        {
            GameObject.Destroy(currentVisual);
        }
        if (plantData != null && plantData.stageModels != null && plantData.stageModels.Length > 0)
        {
            if (plantData.stageModels.Length > currentStage && plantData.stageModels[currentStage] != null)
            {
                currentVisual = Instantiate(plantData.stageModels[currentStage], visualHolder.position, visualHolder.rotation, visualHolder);
            }
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
