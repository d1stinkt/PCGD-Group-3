using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float bounce; // Value of bounce when colliding with enemy

    public bool alive;  // True as long as player got health left
    public bool enemyHit;   // Boolean to switch off RigidBody manipulation for the time of bounce when colliding with an enemy

    public int maxHealth = 100;
    public int currentHealth;
    public float damageTimer = 0.5f;
   
    public Rigidbody2D rb;
    public Camera mainCamera;
    public HealthBar healthBar;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    GameManager gm;

    void Start()
    {
        alive = true;
        // Setting health to max at the beginning
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (alive)
        {
            PlayerInput();
        }

        damageTimer = damageTimer - Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (alive && !enemyHit)
        {
            Move();
        }

        if(!alive)
        {
            rb.velocity = new Vector2(0f, 0f); // Stopping player movement after dying
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
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector2 direction = transform.position - collision.transform.position;
            direction.Normalize();
            rb.AddForce(direction * bounce, ForceMode2D.Impulse);
            enemyHit = true;
            Invoke("StopBounce", 0.2f);

            if (damageTimer < 0)
            {
                if (currentHealth > 20)
                {
                    LoseHealth(20);
                    damageTimer = 0.5f;
                }
                else
                {
                    healthBar.SetHealth(0);
                    alive = false;
                    gm.GameOver();
                }
            }
        }
    }

    void StopBounce()
    {
        enemyHit = false;
    }

    void LoseHealth(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
