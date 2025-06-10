using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;
    public Animator Animator;
    public bool IsWalking = false;
    private bool wasWalking = false;
    public bool isfacingright = true;
    public bool IsAttacking= false;

    public AudioSource audioSource;
    public AudioClip walk;
    public AudioClip attack;
    

    public void Start()
    {
        if (Animator == null)
        {
            Animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
            if (isfacingright)
            {
                flip();
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
            if (!isfacingright)
            {
                flip();
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.back;
        }

        IsWalking = moveDirection != Vector3.zero;

        if (IsWalking)
        {
            moveDirection.Normalize();
            transform.position += moveDirection * Speed * Time.deltaTime;
        }

        
        if (IsWalking && !wasWalking)
        {
            PlayClip(walk);
        }
        else if (!IsWalking && wasWalking)
        {
            StopClip();
        }

        Animator.SetBool("IsWalking", IsWalking);
        Animator.SetBool("IsAttacking", IsAttacking);

        if (Input.GetKeyDown(KeyCode.F))
        {
            IsAttacking = true;

            if (IsAttacking = true)
            {
                PlayClipAttack(attack);
            }
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            IsAttacking = false;
            
        }

        wasWalking = IsWalking;
    }

    public void flip()
    {
        isfacingright = !isfacingright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
            audioSource.loop = true;
        }
    }
    public void StopClip()
    {
        audioSource.Stop();
        audioSource.loop = false;
    }
    void PlayClipAttack(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
            
        }
    }
}

