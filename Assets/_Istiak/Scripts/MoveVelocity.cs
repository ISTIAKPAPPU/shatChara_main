using UnityEngine;

public class MoveVelocity : MonoBehaviour, IMoveVelocity
{
    public float moveSpeed;
    private Vector3 velocityVector;

    private Rigidbody2D rigidbody2D;
    public Animator animator;

    private float movementSpeed;


    private void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        // characterBase = GetComponent<Character_Base>();
    }

    public void SetVelocity(Vector3 velocityVector)
    {
        this.velocityVector = velocityVector;
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = velocityVector * moveSpeed;
        movementSpeed = Mathf.Clamp(velocityVector.magnitude, 0.0f, 1.0f);
        Anim();
    }

    public void Disable()
    {
        this.enabled = false;
        rigidbody2D.velocity = Vector3.zero;
    }

    public void Enable()
    {
        this.enabled = true;
    }

    private void Anim()
    {
        if (velocityVector != Vector3.zero)
        {
            animator.enabled = true;
            animator.SetFloat("Horizontal", velocityVector.x);
            animator.SetFloat("Vertical", velocityVector.y);
            animator.SetBool("Stack", false);
        }
        
        animator.SetFloat("Speed", movementSpeed);
    }
}