using System.Collections;
using UnityEngine;

public class DummyHit : MonoBehaviour
{
    [SerializeField] private ParticleSystem dustParticles;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public bool playerInRange;

    public AudioSource audioSource;
    public AudioClip hitSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        // Check for input or trigger for hitting the dummy (this is a placeholder)
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            TriggerHit();
            PlayHitSound();
            StartCoroutine(FlashRed());
            dustParticles.Play();
            
        }
        
    }

    void TriggerHit()
    {
        animator.ResetTrigger("Hit"); 
        animator.SetTrigger("Hit");
        

    }
    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f); // flashes red for 0.1 seconds
        spriteRenderer.color = originalColor;
        
    }

    void PlayHitSound()
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }


    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

