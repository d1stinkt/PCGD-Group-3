using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public bool alive;

    public Rigidbody2D rb;
    public Camera mainCamera;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    void Start()
    {
        alive = true;    
    }

    void Update()
    {
        if (alive)
        {
            PlayerInput();
        }
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            Move();
        }
    }


    void PlayerInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
