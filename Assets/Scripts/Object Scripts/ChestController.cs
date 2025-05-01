using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false; // Track if the chest is open
    public bool playerInRange = false;

    private AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange) // When you press E
        {
            ToggleChest();
            PlaySound();
        }
    }
    void PlaySound()
    {
        if (audioSource != null)
        {
            if (isOpened && openSound != null)
            {
                audioSource.PlayOneShot(openSound);
            }
            else if (!isOpened && closeSound != null)
            {
                audioSource.PlayOneShot(closeSound);
            }
        }
    }
    void ToggleChest()
    {
        isOpened = !isOpened; // Flip the bool (if open, close; if closed, open)
        animator.SetBool("IsOpened", isOpened);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

