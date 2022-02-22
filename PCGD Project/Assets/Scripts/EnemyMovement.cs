using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;
    Rigidbody2D enemyRb;

    public float enemySpeed;
    float rotationOffset = 270f;
    float scale = 1.2f;

    public int ID;

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        enemySpeed = Random.Range(5f, 10f);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        enemyRb.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
    }

    void Rotate()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyRb.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + rotationOffset));
    }
}