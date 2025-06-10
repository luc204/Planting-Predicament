using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 1.5f;
    public float runAwayDistance = 3f;
    public int maxHealth = 3;
    public float attackCooldown = 1f;

    public ItemData newPlantItem;
    public List<ItemData> itemsToDeliver;

    private int currentHealth;
    private Transform player;
   

    void Start()
    {
        currentHealth = maxHealth;

        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            player = playerMovement.transform;
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < runAwayDistance)
        {
            RunAwayFromPlayer();
        }

        
        if (distance <= runAwayDistance && Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage();
        }
        
    }

    void RunAwayFromPlayer()
    {
        Vector3 directionAway = (transform.position - player.position).normalized;
        transform.position += directionAway * speed * Time.deltaTime;
    }

    public void TakeDamage()
    {
        currentHealth--;
        Debug.Log("Enemy hit! Remaining health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy destroyed.");
            Inventory2 inventory = FindObjectOfType<Inventory2>();

            if (inventory != null)
            {
                foreach (ItemData item in itemsToDeliver)
                {
                    inventory.AddItem(item);
                    Debug.Log($"Delivered: {item.itemName}");

                }
            }
            else
            {
                Debug.LogWarning("Inventory not found!");
            }

            Destroy(gameObject);
        }
    }
}
