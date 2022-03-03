using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    Transform shootPoint;
    [SerializeField]
    GameObject bullet;

    Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {   
 
        if(Input.GetButtonDown("Fire1"))
        {
            if (Time.timeScale > 0 && player.alive)
            {
                Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            }        
        }
    }
}
