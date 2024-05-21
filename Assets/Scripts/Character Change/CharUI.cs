using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharUI : MonoBehaviour
{
    GameObject panel; // ĳ���� ����â

    private void Start()
    {
       panel = GameObject.FindWithTag("CharacterPanel");
    }

    private void OnTriggerEnter(Collider other)
    {
        panel.SetActive(false);

        //�÷��̾ �ݶ��̴��� ������ ui ���̰�
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