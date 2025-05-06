using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;
    public Animator Animator;
    public bool IsWalking = false;
    public bool isfacingright = true;
    public bool IsAttacking= false;
    

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

        // Check if moving
        IsWalking = moveDirection != Vector3.zero;

        // Move player if moving
        if (IsWalking)
        {
            moveDirection.Normalize();
            transform.position += moveDirection * Speed * Time.deltaTime;
        }

        // Update the animator
        Animator.SetBool("IsWalking", IsWalking);
        Animator.SetBool("IsAttacking", IsAttacking);


        if (Input.GetKeyDown(KeyCode.F))
        {
            IsAttacking = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            IsAttacking = false;
        }
        
    }

    public void flip()
    {
        isfacingright = !isfacingright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

