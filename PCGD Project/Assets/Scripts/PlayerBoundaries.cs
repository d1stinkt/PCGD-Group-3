using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundaries : MonoBehaviour
{
    public float xLimit = 35f;
    public float yLimit = 19.4f;

    // Update is called once per frame
    void Update()
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
