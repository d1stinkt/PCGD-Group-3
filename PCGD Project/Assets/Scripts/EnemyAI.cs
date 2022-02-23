using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    Transform player;
    NavMeshAgent agent;
    Rigidbody2D enemyRb;

    float rotationOffset = 270f;
    float scale = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        transform.localScale = new Vector3(scale, scale, scale);

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        Rotate();
    }

    void Rotate()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyRb.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + rotationOffset));
    }
}
