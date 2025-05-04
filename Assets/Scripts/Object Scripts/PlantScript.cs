using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] StageSprites;
    public float []StageDurations = { 10f, 10f, 10f };
    public int CurrentStage = 0;
    private float growthTimmer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentStage < StageSprites.Length - 1)
        {
            growthTimmer += Time.deltaTime;
            if (growthTimmer >= StageDurations[CurrentStage])
            {
                CurrentStage++;
                growthTimmer = 0f;
                UpdateSprite();
            }
        }
    }
    void UpdateSprite()
    {
        if (spriteRenderer != null && StageSprites != null && StageSprites.Length > CurrentStage)
        {
            spriteRenderer.sprite = StageSprites[CurrentStage];
        }
    }
}
