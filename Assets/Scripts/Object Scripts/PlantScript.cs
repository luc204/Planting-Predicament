using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] StageSprites;
    public float []StageDurations = { 10f, 10f, 10f, 5F };
    public int CurrentStage = 0;
    private float growthTimer = 0f;
    private bool IsPlayerPresent = false;
    public GameObject Enemy;
    void Start()
    {
        UpdateSprite();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && CurrentStage > 0 && IsPlayerPresent)

        { 
            if (CurrentStage == 3)
            {
            Cut();
            }
            else
            {
                Debug.Log("You can't cut this plant yet.");
                //add a message to the player
            }
        }
        Grow(); 


        
    }
    void UpdateSprite()
    {
        if (spriteRenderer != null && StageSprites != null && StageSprites.Length > CurrentStage)
        {
            spriteRenderer.sprite = StageSprites[CurrentStage];
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerPresent = true;
            Debug.Log("Player entered plant trigger.");
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerPresent = false;
            Debug.Log("Player exited plant trigger.");
        }
    }
    void Cut()
    {
        CurrentStage--;
        growthTimer = 0f;
        UpdateSprite();
    }
    void Grow()
    {
        if (CurrentStage < StageSprites.Length - 1)
        {
            growthTimer += Time.deltaTime;
            if (growthTimer >= StageDurations[CurrentStage])
            {
                CurrentStage++;
                growthTimer = 0f;
                UpdateSprite();
                if (CurrentStage == 4)
                {
                    if (Enemy != null)
                    {
                        Vector3 spawnPosition = transform.position;
                        Instantiate(Enemy, spawnPosition, Quaternion.identity);
                        Debug.Log("Enemy spawned at " + spawnPosition);
                    }
                    else
                    {
                        Debug.Log("No enemy to spawn.");
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
}
