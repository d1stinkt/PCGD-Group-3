using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    AudioManager AudioManager;
    Transform player;
    NavMeshAgent agent;
    Rigidbody2D enemyRb;

    public int ID;
 
    float rotationOffset = 270f;
    float scale = 1.2f;
    public float minWaitBetweenPlays = 4f;
    public float maxWaitBetweenPlays = 8f;
    public float waitTimeCountdown = -1f;
    private bool GameIsPaused;

    // Start is called before the first frame update
    void Start()
    {
        GameIsPaused = Global.GamePaused;
        agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        agent.updateUpAxis = false;
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        transform.localScale = new Vector3(scale, scale, scale);

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        Rotate();
        while(GameIsPaused == false)
        {
            AudioManager.PlayZombieNoises("ZombieNoise");
        }
        
    }

    void Rotate()
    {
        var turnTowardNavSteeringTarget = agent.steeringTarget;
        Vector3 direction = (turnTowardNavSteeringTarget - transform.position);
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyRb.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + rotationOffset));
    }
}
