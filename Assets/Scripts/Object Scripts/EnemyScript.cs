using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3f;
    public float attackRange = 1.5f;
    public int damage = 10;
    public float attackCooldown = 1f;

    private Transform player; // Changed from float to Transform
    private float lastAttackTime = 0f;

    void Start()
    {
        // Find the player object
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

        // Calculate distance to player
        float distance = Vector3.Distance(transform.position, player.position);
        
        // Move towards player if out of attack range
        if (distance > attackRange)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                player.position, 
                speed * Time.deltaTime
            );
        }
        // Attack if within range and cooldown has elapsed
        else if (Time.time >= lastAttackTime + attackCooldown)
        {
           
            lastAttackTime = Time.time;
        }
    }

    
}
