using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool GameIsPaused;

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
    public GameObject armorIcon;
    AudioManager AudioManager;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    public Animator bottomAnimator;

    GameManager gm;

    void Start()
    {
        alive = true;
        // Setting health to max at the beginning
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        GameIsPaused = Global.GamePaused;
    }

    void Update()
    {
        if (alive)
        {
            PlayerInput();
            damageTimer = damageTimer - Time.deltaTime;
        }
        else
        {
            Debug.Log("Test");
            bottomAnimator.SetFloat("Magnitude", 0);
        }
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

        //Animations
        bottomAnimator.SetFloat("Horizontal", moveX);
        bottomAnimator.SetFloat("Vertical", moveY);
        bottomAnimator.SetFloat("Magnitude", moveDirection.magnitude);
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
        if (collision.gameObject.tag == "Enemy" && gm.armor)
        {
            Vector2 direction = transform.position - collision.transform.position;
            direction.Normalize();
            rb.AddForce(direction * bounce, ForceMode2D.Impulse);
            enemyHit = true;
            Invoke("StopBounce", 0.2f);
            StartCoroutine(ArmorBreak());
        }

        else if (collision.gameObject.tag == "Enemy" && !gm.armor)
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



        //Power-ups
        if (collision.tag == "PowerUp")
        {
            Physics2D.IgnoreCollision(collision, GetComponent<BoxCollider2D>());
            int ID = collision.GetComponent<PowerUp>().powerUpID;

            if (ID == 2 && gm.armor) { Destroy(collision.gameObject); }
            if ((ID == 1 || ID == 0) && (gm.speedUp || gm.rainbowBullet)) { Destroy(collision.gameObject); }

            else
            {
                StartCoroutine(gm.PowerUp(collision.GetComponent<PowerUp>().powerUpID));
                Destroy(collision.gameObject);
            }
        }
    }

    void StopBounce()
    {
        enemyHit = false;
    }

    IEnumerator ArmorBreak()
    {
        armorIcon.SetActive(false);
        AudioManager.Play("ShieldBreak");
        yield return new WaitForSeconds(1.0f);
        gm.armor = false;
    }

    void LoseHealth(int damage)
    {
        AudioManager.Play("BouncerPain");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
