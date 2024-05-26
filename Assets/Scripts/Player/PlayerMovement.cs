using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 movement;
    public bool facingRight = true;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetBool("isMoving", movement != Vector2.zero);

        if (movement.x < 0 && facingRight)
            Flip();
        else if (movement.x > 0 && !facingRight)
            Flip();
    }

    void FixedUpdate()
    {
        Vector2 targetPosition = rb.position + movement * speed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
