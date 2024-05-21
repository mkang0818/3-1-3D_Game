using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleHpBar : MonoBehaviour
{
    Image HPSlider; //�����̴�
    float maxHealth = 5f; //�ִ�ü��
    private void Start()
    {
        HPSlider = GetComponent<Image>();
    }

    void Update()
    {
        HPSlider.fillAmount = GameObject.Find("Player").GetComponent<PlayerController>().playerHP / maxHealth; //ü�� ����
    }
}
