using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform bulletStartpoint;
    public PlayerBoundaries color;

    public GameObject bulletBlue;
    public GameObject bulletRed;
    public GameObject bulletGreen;
    public GameObject bulletYellow;

    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (color.Blue)
        {
            GameObject bullet = Instantiate(bulletBlue, bulletStartpoint.position, bulletStartpoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bulletStartpoint.transform.up * bulletForce, ForceMode2D.Impulse);
        }
        if (color.Red)
        {
            GameObject bullet = Instantiate(bulletRed, bulletStartpoint.position, bulletStartpoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bulletStartpoint.transform.up * bulletForce, ForceMode2D.Impulse);
        }
        if (color.Green)
        {
            GameObject bullet = Instantiate(bulletGreen, bulletStartpoint.position, bulletStartpoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bulletStartpoint.transform.up * bulletForce, ForceMode2D.Impulse);
        }
        if (color.Yellow)
        {
            GameObject bullet = Instantiate(bulletYellow, bulletStartpoint.position, bulletStartpoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bulletStartpoint.transform.up * bulletForce, ForceMode2D.Impulse);
        }
    }
}
