using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Transform origin, end, player;
    public float radarSpd;
    public bool playerDetected;

    public static bool playerIsDetected;

    private int playerLayer = 1 << 6;
    public Rigidbody2D enemyRb;
    private Vector3 facePlayer;

    private void Start()
    {
        playerIsDetected = false;
    }

    private void Update()
    {
        if (playerDetected == false)
        {
            PlayerDetector();
            Radar();
        }
        else { PlayerIsDetected(); }

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

    void PlayersPosition()
    {
        facePlayer = player.position - enemyRb.transform.position;
        enemyRb.transform.up = -facePlayer;
    }

    void PlayerIsDetected()
    {
        if (playerDetected == true)
        {
            playerIsDetected = true;
            end.position = player.position;
            PlayersPosition();
        }
    }
}
