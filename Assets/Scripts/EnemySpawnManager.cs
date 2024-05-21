using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] gameObjects; //생성된 오브젝트를 담을 오브젝트형 배열

    public Transform[] spawnPosArray; //스폰 장소를 가지고 있는 배열
    public GameObject Enemy; //생성할 프리팹
    public int pivot; //생성 개수 변수
    public float level = 2.0f; //생성 속도 변수
    // Start is called before the first frame update
    void Start()
    {
        gameObjects = new GameObject[100]; ///오브젝트 풀링 기법
        for (int i = 0; i < 100; i++)
        {
            GameObject gameObject = Instantiate(Enemy, spawnPosArray[Random.Range(0, 4)].position, Quaternion.identity); //몬스터 오브젝트 생성
            gameObjects[i] = gameObject; //배열에 오브젝트 저장
            gameObject.SetActive(false); //오브젝트 비활성화
        }
        StartCoroutine(SpawnZombie()); //코루틴 시작
    }
    IEnumerator SpawnZombie()
    {
        yield return new WaitForSeconds(level); //level초마다 생성
        gameObjects[pivot++].SetActive(true); //오브젝트 활성화
        if (pivot == 100) pivot = 0;
        StartCoroutine(SpawnZombie());
    }
}
