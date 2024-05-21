using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharUI : MonoBehaviour
{
    GameObject panel; // 캐릭터 선택창

    private void Start()
    {
       panel = GameObject.FindWithTag("CharacterPanel");
    }

    private void OnTriggerEnter(Collider other)
    {
        panel.SetActive(false);

        //플레이어가 콜라이더에 닿으면 ui 보이게
        if (other.gameObject.tag == "Player")
        {
            if (panel != null)
            {
                panel.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Panel is not assigned");
            }
        }
    }
}