using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleHpBar : MonoBehaviour
{
    Image HPSlider; //슬라이더
    float maxHealth = 5f; //최대체력
    private void Start()
    {
        HPSlider = GetComponent<Image>();
    }

    void Update()
    {
        HPSlider.fillAmount = GameObject.Find("Player").GetComponent<PlayerController>().playerHP / maxHealth; //체력 구현
    }
}
