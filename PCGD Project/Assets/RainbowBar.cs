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

    private void FixedUpdate()
    {
        if (gm.rainbowBullet)
        {
            StartCoroutine(sliderDown());
        }

        SliderFill();
    }

    void SliderFill()
    {
        if (slider.value == 0)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator sliderDown()
    {
        slider.value = speedTimer -= Time.deltaTime / 5f;
        yield return null;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.01f);
        speedTimer = 1f;
        slider.value = 1f;
    }
}