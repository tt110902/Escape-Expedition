using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ di chuyển
    public Animator animator; // Animator của nhân vật

    private Rigidbody2D rb; // Rigidbody của nhân vật
    private Vector2 movement; // Hướng di chuyển

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Lấy component Rigidbody2D
    }

    void Update()
    {
        // Lấy input từ bàn phím
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");

        // Xác định hướng di chuyển
        movement = new Vector2(moveInputX, moveInputY).normalized;

        // Cập nhật trạng thái của animation
        if (movement != Vector2.zero)
        {
            animator.SetBool("isMoving", true); // Nếu di chuyển, set isMoving thành true
        }
        else
        {
            animator.SetBool("isMoving", false); // Ngược lại, set isMoving thành false
        }
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
