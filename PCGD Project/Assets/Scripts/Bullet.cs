using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    AudioManager AudioManager;
    GameManager gm;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public int colorID;
    bool splatterForm;

    [SerializeField]
    Sprite rainbowSprite;
    [SerializeField]
    Sprite splatter;

    //public GameObject splatter;

    private void Awake()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        splatterForm = false;
    }

    void Start()
    {
        rb.AddForce(gm.bulletForce * transform.up, ForceMode2D.Impulse);
        colorID = gm.ColorID;
        if (gm.rainbowBullet) { colorID = 4; }
        ColorChange();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (!splatterForm)
            {
                int enemyColor = collision.gameObject.GetComponent<EnemyAI>().ID;
                Vector3 spawnPos = transform.position;
                Quaternion rotation = transform.rotation;

                if (colorID == enemyColor)
                {
                    splatterForm = true;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    sr.sprite = splatter;
                    rb.isKinematic = true;
                    Destroy(collision.gameObject);
                    AudioManager.Play("ZombieDeath");
                    StartCoroutine(Cleaning());
                }
                else if (colorID == 4)
                {
                    colorID = enemyColor;
                    ColorChange();
                    splatterForm = true;
                    Destroy(collision.gameObject);
                    AudioManager.Play("ZombieDeath");
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    sr.sprite = splatter;
                    rb.isKinematic = true;
                    StartCoroutine(Cleaning());
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        if (collision.tag == "GameAreaBorder")
        {
            Destroy(gameObject);
        }
    }

    void ColorChange()
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
                sr.color = new Color(0.5f, 0, 0.5f, 1);
                break;

            case 4:
                sr.sprite = rainbowSprite;
                break;
        }
    }

    IEnumerator Cleaning()
    {
        yield return new WaitForSeconds(35);
        Destroy(gameObject);
    }
}
