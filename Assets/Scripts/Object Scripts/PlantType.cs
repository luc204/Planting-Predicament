using UnityEngine;

[CreateAssetMenu(menuName = "Plants/Plant Type")]
public class PlantType : ScriptableObject
{
    public string plantName;
    public Sprite[] stageSprites;
    public GameObject[] stageModels;
    public float[] stageDurations;
    public int cuttableStage = 3;
   
    public GameObject enemyToSpawn;
    public GameObject trimmingPrefab;

   
}

