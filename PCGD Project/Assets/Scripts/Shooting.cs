using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    Transform shootPoint;
    [SerializeField]
    GameObject bullet;


    void Update()
    {   
 
        if(Input.GetButtonDown("Fire1"))
        {
            if (Time.timeScale > 0)
            {
                Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            }        
        }
    }
}
