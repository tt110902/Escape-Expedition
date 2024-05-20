using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    private Rigidbody2D rd;

    public Vector3 moveInput;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
                transform.localScale = new Vector3(1, 1, 0);
            else
                transform.localScale = new Vector3(-1, 1, 0);
        }
    }
}
