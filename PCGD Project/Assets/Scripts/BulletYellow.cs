using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletYellow : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "YellowEnemy")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
