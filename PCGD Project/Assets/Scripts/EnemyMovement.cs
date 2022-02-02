using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;

    public float enemySpeed;

    public int ID;

    private void Start()
    {
        player = GameObject.Find("Player");
        enemySpeed = Random.Range(5f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
    }
}
