using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Transform origin, end;
    Transform player;
    public float radarSpd;
    public bool playerDetected;

    public static bool playerIsDetected;

    private int playerLayer = 1 << 6;
    //public Rigidbody2D enemyRb;
    private Vector3 facePlayer;

    private void Start()
    {
        playerIsDetected = false;
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update() // Do the radar until player is detected
    {
        if (playerDetected == false)
        {
            PlayerDetector();
            Radar();
        }
        else 
        { 
            PlayerIsDetected();
        }

    }

    void PlayerDetector()
    {
        Debug.DrawLine(origin.position, end.position, Color.red);
        playerDetected = Physics2D.Linecast(origin.position, end.position, playerLayer);
    }

    void Radar()
    {
        end.RotateAround(origin.position, Vector3.forward, radarSpd * Time.deltaTime);
        playerIsDetected = false;
    }

    void PlayersPosition() // Turning the enemy towards player, NOT USED
    {
        Vector2 direction = player.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        //facePlayer = player.position - enemyRb.transform.position;
        //enemyRb.transform.up = -facePlayer;
    }

    void PlayerIsDetected()
    {
        if (playerDetected == true)
        {
            playerIsDetected = true;
            end.position = player.position;
            //PlayersPosition();
        }
    }
}
