using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] gameObjects; //������ ������Ʈ�� ���� ������Ʈ�� �迭

    public Transform[] spawnPosArray; //���� ��Ҹ� ������ �ִ� �迭
    public GameObject Enemy; //������ ������
    public int pivot; //���� ���� ����
    public float level = 2.0f; //���� �ӵ� ����
    // Start is called before the first frame update
    void Start()
    {
        gameObjects = new GameObject[100]; ///������Ʈ Ǯ�� ���
        for (int i = 0; i < 100; i++)
        {
            GameObject gameObject = Instantiate(Enemy, spawnPosArray[Random.Range(0, 4)].position, Quaternion.identity); //���� ������Ʈ ����
            gameObjects[i] = gameObject; //�迭�� ������Ʈ ����
            gameObject.SetActive(false); //������Ʈ ��Ȱ��ȭ
        }
        StartCoroutine(SpawnZombie()); //�ڷ�ƾ ����
    }
    IEnumerator SpawnZombie()
    {
        yield return new WaitForSeconds(level); //level�ʸ��� ����
        gameObjects[pivot++].SetActive(true); //������Ʈ Ȱ��ȭ
        if (pivot == 100) pivot = 0;
        StartCoroutine(SpawnZombie());
    }
}
