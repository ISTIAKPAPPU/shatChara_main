using UnityEngine;

public class MoveTransformVelocity : MonoBehaviour, IMoveVelocity
{
    public float moveSpeed;

    private Vector3 velocityVector;
    private float movementSpeed;
    public Animator animator;
    private void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        
    }
    public void SetVelocity(Vector3 velocityVector)
    {
        this.velocityVector = velocityVector;
    }

    private void Update()
    {
        transform.position += velocityVector * moveSpeed * Time.deltaTime;
        movementSpeed = Mathf.Clamp(velocityVector.magnitude, 0.0f, 1.0f);
        Anim();
    }

    public void Disable()
    {
        this.enabled = false;
    }

    public void Enable()
    {
        this.enabled = true;
    }
    private void Anim()
    {
        if (velocityVector != Vector3.zero)
        {
            animator.SetFloat("Horizontal", velocityVector.x);
            animator.SetFloat("Vertical", velocityVector.y);
            animator.SetBool("Seat", false);
            // animator.SetBool("Seat", true);
            // animator.SetBool("Seat", false);
        }
        

        animator.SetFloat("Speed", movementSpeed);
    }
    
    
}