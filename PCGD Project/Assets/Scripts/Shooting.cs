using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    AudioManager AudioManager;

    [SerializeField]
    Transform shootPoint;
    [SerializeField]
    GameObject bullet;

    Player player;
    float timer = 0f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        timer += Time.deltaTime;
 
        if(Input.GetButtonDown("Fire1") && timer > 0.5f)
        {
            
            if (Time.timeScale > 0 && player.alive)
            {
                AudioManager.Play("BulletNoise");
                timer = 0f;
                Instantiate(bullet, shootPoint.position, shootPoint.rotation);
                
            }        
        }
    }
}
