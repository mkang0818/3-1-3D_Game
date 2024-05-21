using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public Text Txt_Time_Hour; //시 텍스트
    public Text Txt_Time_Min;  //분 텍스트
    public Text Txt_Time_Second; //초 텍스트

    public float time; //시간
    public int playerType = 0; //플레이모드함수
    public int Enemy_Num = 1000; //몬스터 개수
    public int Animal_Num = 1000; //아군 개수
    public int Enemy_Count = 0; //몬스터 처치 횟수
    // Start is called before the first frame update
    void Awake()
    {
        //DontDestroy로 인해 처음 씬으로 돌아왔을 때 중복 방지
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
        time -= Time.deltaTime; //시간 감소
        text(); //TEXT표시하는 함수 호출

        //게임 클리어, 오버 구현
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
        //시간을 60초로 나눠 텍스트에 표시
        if (GameObject.Find("txt_Time_Hour") != null) GameObject.Find("txt_Time_Hour").GetComponent<Text>().text = ((int)time / 3600).ToString() + " :";
        if (GameObject.Find("txt_Time_Min") != null) GameObject.Find("txt_Time_Min").GetComponent<Text>().text = ((int)time / 60 % 60).ToString() + " :";
        if (GameObject.Find("txt_Time_Second") != null) GameObject.Find("txt_Time_Second").GetComponent<Text>().text = ((int)time % 60).ToString();

        //몬스터, 아군 개수 텍스트에 표시
        if (GameObject.Find("Txt_Enemy_Num") != null) GameObject.Find("Txt_Enemy_Num").GetComponent<Text>().text = Enemy_Num.ToString() + " x";
        if (GameObject.Find("Txt_Animal_Num") != null) GameObject.Find("Txt_Animal_Num").GetComponent<Text>().text = Animal_Num.ToString() + " x";
        if (GameObject.Find("Txt_Enemy_Count") != null) GameObject.Find("Txt_Enemy_Count").GetComponent<Text>().text = Enemy_Count.ToString() + " x";
    }
}