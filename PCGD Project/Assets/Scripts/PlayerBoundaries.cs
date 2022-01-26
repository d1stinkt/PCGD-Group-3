using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundaries : MonoBehaviour
{
    public float xLimit = 35f;
    public float yLimit = 19.5f;

    public bool Blue;
    public bool Red;
    public bool Green;
    public bool Yellow;

    // Update is called once per frame
    void Update()
    {
        LevelBoundaries();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BlueArea")
        {
            Red = false;
            Green = false;
            Yellow = false;
            Blue = true;
        }
        if (other.gameObject.tag == "RedArea")
        {
            Green = false;
            Yellow = false;
            Blue = false;
            Red = true;
        }
        if (other.gameObject.tag == "GreenArea")
        {
            Red = false;
            Yellow = false;
            Blue = false;
            Green = true;
        }
        if (other.gameObject.tag == "YellowArea")
        {
            Red = false;
            Green = false;
            Blue = false;
            Yellow = true;
        }
    }

    void LevelBoundaries()
    {
        if (transform.position.x < -xLimit)
        {
            transform.position = new Vector2(-xLimit, transform.position.y);
        }

        if (transform.position.x > xLimit)
        {
            transform.position = new Vector2(xLimit, transform.position.y);
        }

        if (transform.position.y < -yLimit)
        {
            transform.position = new Vector2(transform.position.x, -yLimit);
        }

        if (transform.position.y > yLimit)
        {
            transform.position = new Vector2(transform.position.x, yLimit);
        }
    }
}
