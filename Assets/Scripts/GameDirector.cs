using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public Text Txt_Time_Hour; //�� �ؽ�Ʈ
    public Text Txt_Time_Min;  //�� �ؽ�Ʈ
    public Text Txt_Time_Second; //�� �ؽ�Ʈ

    public float time; //�ð�
    public int playerType = 0; //�÷��̸���Լ�
    public int Enemy_Num = 1000; //���� ����
    public int Animal_Num = 1000; //�Ʊ� ����
    public int Enemy_Count = 0; //���� óġ Ƚ��
    // Start is called before the first frame update
    void Awake()
    {
        //DontDestroy�� ���� ó�� ������ ���ƿ��� �� �ߺ� ����
        var obj = FindObjectsOfType<GameDirector>();

        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        time -= Time.deltaTime; //�ð� ����
        text(); //TEXTǥ���ϴ� �Լ� ȣ��

        //���� Ŭ����, ���� ����
        if (time <= 0)
        {
            time = 1000000;
            if (Enemy_Num > 0 || Animal_Num > 0)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().Is_Success = false;
                GameObject.Find("Player").GetComponent<PlayerController>().Is_Fail = true;
            }
        }

        if (Enemy_Num == 0 && Animal_Num == 0)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().Is_Success = true;
        }

    }
    void text()
    {
        //�ð��� 60�ʷ� ���� �ؽ�Ʈ�� ǥ��
        if (GameObject.Find("txt_Time_Hour") != null) GameObject.Find("txt_Time_Hour").GetComponent<Text>().text = ((int)time / 3600).ToString() + " :";
        if (GameObject.Find("txt_Time_Min") != null) GameObject.Find("txt_Time_Min").GetComponent<Text>().text = ((int)time / 60 % 60).ToString() + " :";
        if (GameObject.Find("txt_Time_Second") != null) GameObject.Find("txt_Time_Second").GetComponent<Text>().text = ((int)time % 60).ToString();

        //����, �Ʊ� ���� �ؽ�Ʈ�� ǥ��
        if (GameObject.Find("Txt_Enemy_Num") != null) GameObject.Find("Txt_Enemy_Num").GetComponent<Text>().text = Enemy_Num.ToString() + " x";
        if (GameObject.Find("Txt_Animal_Num") != null) GameObject.Find("Txt_Animal_Num").GetComponent<Text>().text = Animal_Num.ToString() + " x";
        if (GameObject.Find("Txt_Enemy_Count") != null) GameObject.Find("Txt_Enemy_Count").GetComponent<Text>().text = Enemy_Count.ToString() + " x";
    }
}