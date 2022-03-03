using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowBar : MonoBehaviour
{
    public Slider slider;
    float speedTimer;

    GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        speedTimer = 1f;
    }

    private void Update()
    {
        if (gm.rainbowBullet)
        {
            StartCoroutine(sliderDown());
        }
        else
        {
            speedTimer = 1f;
            slider.value = 1f;
        }
    }

    IEnumerator sliderDown()
    {
        slider.value = speedTimer -= Time.deltaTime / 5f;
        yield return null;
    }
}