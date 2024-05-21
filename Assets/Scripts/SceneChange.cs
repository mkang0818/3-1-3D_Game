using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //������Ʈ�� UI
    public GameObject UI_EasyMode;
    public GameObject UI_HardMode;
    public GameObject UI_SurvivalMode;
    //���Ӿ����� ��ȯ
    public void RobbyScene() //�κ������ �̵�
    {
        if (GameObject.Find("GameDirector") != null) //����ó��
        {
            //����� ����, ���
            GameObject.Find("GameDirector").GetComponent<AudioSource>().Stop();
            GameObject.Find("Main_Audio").GetComponent<AudioSource>().Play();
        }

        if (GameObject.Find("GameDirector") != null) GameObject.Find("GameDirector").GetComponent<GameDirector>().playerType = 0; //�÷��� ���
        SceneManager.LoadScene("RobbyScene");
    }
    public void EasyMode()//������� ������ �̵�
    {
        //����� ����, ���
        GameObject.Find("Main_Audio").GetComponent<AudioSource>().Stop();
        GameObject.Find("GameDirector").GetComponent<AudioSource>().Play();

        //��忡 �°� �ð�,����,�Ʊ����� �ʱ�ȭ
        GameObject.Find("GameDirector").GetComponent<GameDirector>().time = 120;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().Enemy_Num = 13;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().Animal_Num = 13;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().playerType = 1;
        SceneManager.LoadScene("EasyModeScene");
    }
    public void HardMode()//�ϵ��� ������ �̵�
    {
        //����� ����, ���
        GameObject.Find("Main_Audio").GetComponent<AudioSource>().Stop();
        GameObject.Find("GameDirector").GetComponent<AudioSource>().Play();

        //��忡 �°� �ð�,����,�Ʊ����� �ʱ�ȭ
        GameObject.Find("GameDirector").GetComponent<GameDirector>().time = 180;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().Enemy_Num = 25;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().Animal_Num = 13;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().playerType = 2;
        SceneManager.LoadScene("HardModeScene");
    }
    public void SurvivalMode()//�����̹������� �̵�
    {
        //����� ����, ���
        GameObject.Find("Main_Audio").GetComponent<AudioSource>().Stop();
        GameObject.Find("GameDirector").GetComponent<AudioSource>().Play();

        //��忡 �°� �ð�,����,�Ʊ����� �ʱ�ȭ
        GameObject.Find("GameDirector").GetComponent<GameDirector>().playerType = 3;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().Enemy_Count = 0;
        SceneManager.LoadScene("SurvivalModeScene");
    }
    public void Bt_Back() //�ڷΰ����Լ�
    {
        //������Ʈ ��� ��Ȱ��ȭ
        UI_EasyMode.SetActive(false);
        UI_HardMode.SetActive(false);
        UI_SurvivalMode.SetActive(false);
        GameObject.Find("GameDirector").GetComponent<GameDirector>().playerType = 3;
    }
}
