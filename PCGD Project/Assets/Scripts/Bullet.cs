using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameManager gm;
    Rigidbody2D rb;
    SpriteRenderer sr;

    int color;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        rb.AddForce(gm.bulletForce * transform.up, ForceMode2D.Impulse);
        color = gm.ColorID;
        
        switch (color)
        {
            case 0:
                sr.color = Color.yellow;
                break;

            case 1:
                sr.color = Color.blue;
                break;

            case 2:
                sr.color =new Color(0,0.5f,0,1);
                break;

            case 3:
                sr.color = new Color(0.5f, 0, 0, 1);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            int enemyColor = collision.gameObject.GetComponent<EnemyMovement>().ID;
            if (enemyColor == color)
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
