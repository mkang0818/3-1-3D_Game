using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //오브젝트형 UI
    public GameObject UI_EasyMode;
    public GameObject UI_HardMode;
    public GameObject UI_SurvivalMode;
    //게임씬으로 전환
    public void RobbyScene() //로비씬으로 이동
    {
        if (GameObject.Find("GameDirector") != null) //예외처리
        {
            //오디오 정지, 재생
            GameObject.Find("GameDirector").GetComponent<AudioSource>().Stop();
            GameObject.Find("Main_Audio").GetComponent<AudioSource>().Play();
        }

        if (GameObject.Find("GameDirector") != null) GameObject.Find("GameDirector").GetComponent<GameDirector>().playerType = 0; //플레이 모드
        SceneManager.LoadScene("RobbyScene");
    }
    public void EasyMode()//이지모드 씬으로 이동
    {
        //오디오 정지, 재생
        GameObject.Find("Main_Audio").GetComponent<AudioSource>().Stop();
        GameObject.Find("GameDirector").GetComponent<AudioSource>().Play();

        //모드에 맞게 시간,몬스터,아군개수 초기화
        GameObject.Find("GameDirector").GetComponent<GameDirector>().time = 120;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().Enemy_Num = 13;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().Animal_Num = 13;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().playerType = 1;
        SceneManager.LoadScene("EasyModeScene");
    }
    public void HardMode()//하드모드 씬으로 이동
    {
        //오디오 정지, 재생
        GameObject.Find("Main_Audio").GetComponent<AudioSource>().Stop();
        GameObject.Find("GameDirector").GetComponent<AudioSource>().Play();

        //모드에 맞게 시간,몬스터,아군개수 초기화
        GameObject.Find("GameDirector").GetComponent<GameDirector>().time = 180;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().Enemy_Num = 25;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().Animal_Num = 13;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().playerType = 2;
        SceneManager.LoadScene("HardModeScene");
    }
    public void SurvivalMode()//서바이벌씬으로 이동
    {
        //오디오 정지, 재생
        GameObject.Find("Main_Audio").GetComponent<AudioSource>().Stop();
        GameObject.Find("GameDirector").GetComponent<AudioSource>().Play();

        //모드에 맞게 시간,몬스터,아군개수 초기화
        GameObject.Find("GameDirector").GetComponent<GameDirector>().playerType = 3;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().Enemy_Count = 0;
        SceneManager.LoadScene("SurvivalModeScene");
    }
    public void Bt_Back() //뒤로가기함수
    {
        //오브젝트 모두 비활성화
        UI_EasyMode.SetActive(false);
        UI_HardMode.SetActive(false);
        UI_SurvivalMode.SetActive(false);
        GameObject.Find("GameDirector").GetComponent<GameDirector>().playerType = 3;
    }
}
