using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameManager gm;
    Rigidbody2D rb;
    SpriteRenderer sr;

    int colorID;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        rb.AddForce(gm.bulletForce * transform.up, ForceMode2D.Impulse);
        colorID = gm.ColorID;
    }

    private void Update()
    {
        if (!gm.rainbowBullet)
        {
            switch (colorID)
            {
                case 0:
                    sr.color = Color.yellow;
                    break;

                case 1:
                    sr.color = Color.blue;
                    break;

                case 2:
                    sr.color = new Color(0, 0.5f, 0, 1);
                    break;

                case 3:
                    sr.color = new Color(0.5f, 0, 0, 1);
                    break;
            }
        }
        else
        {
            sr.color = Color.black;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            int enemyColor = collision.gameObject.GetComponent<EnemyAI>().ID;
            if (enemyColor == colorID || gm.rainbowBullet)
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if (collision.tag == "GameAreaBorder")
        {
            Destroy(gameObject);
        }
    }
}
