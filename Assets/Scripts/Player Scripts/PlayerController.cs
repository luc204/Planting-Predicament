using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    [SerializeField] private ParticleSystem dustParticles;
    // this is the particle system for the dust particles

    public float Speed = 2f;
    public float JumpForce = 1f;
    
    public bool isGrounded;
    public bool IsWalking ;
    public bool isAttacking;
    private bool facingRight =true;

    public GameObject sword;

    private Rigidbody2D rb;
    private Animator animator;

    public AudioSource audioSource;
    public AudioClip WalkSound;
    public AudioClip JumpSound;
    public AudioClip LandSound;
    public AudioClip AttackSound;

    float footstepTimer = 0f;
    public float footstepInterval = 0.4f;

    void Start()
    {
        sword.SetActive(true);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameObject.tag = "Player";
        // this is so that im declaring the rigidbody and the tag of the player at the start
    }

    void Update()
    {
        if (IsWalking && isGrounded)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                PlayWalkSound();
                footstepTimer = footstepInterval; // reset timer
            }
        }
        else
        {
            footstepTimer = 0f; // reset if not walking
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Speed * Time.deltaTime;
            animator.SetBool("IsWalking", true);
            IsWalking = true;
            if (facingRight) Flip();
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
            animator.SetBool("IsWalking", true);
            IsWalking = true;
            if (!facingRight) Flip();
        }
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
            PlayJumpSound();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
            PlayAttackSound();
            attack();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ReappearSword();
        }


        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("IsWalking", false);
            IsWalking = false;
        }

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            dustParticles.Play();
            PlayLandSound();


        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

    public void attack()
    {  
        sword.SetActive(false);
        animator.SetTrigger("Attack");
    }
    public void ReappearSword()
    {
        sword.SetActive(true);
        isAttacking = false;
    }

    void PlayWalkSound()
    {
        if (audioSource != null && WalkSound != null)
        {
            audioSource.PlayOneShot(WalkSound);
        }
    }
     void PlayJumpSound()
    {
        if (audioSource != null && JumpSound != null)
        {
            audioSource.PlayOneShot(JumpSound);
        }
    }
      void PlayLandSound()
    {
        if (audioSource != null && LandSound != null)
        {
            audioSource.PlayOneShot(LandSound);
        }
    }
      void PlayAttackSound()
    {
        if (audioSource != null && AttackSound != null)
        {
            audioSource.PlayOneShot(AttackSound);
        }
    }
}
