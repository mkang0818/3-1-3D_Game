using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Vector3 Enemy_direction;

    public float EnemyBullet_speed = 6f; //적 총알 속도
    bool Enemy_isFire; //적 총알 발사 체크 bool변수

    void Start()
    {
        Destroy(gameObject, 0.7f); //0.7초 후 삭제
    }
    void Update()
    {
        if (Enemy_isFire)
        {
            transform.Translate(Enemy_direction * Time.deltaTime * EnemyBullet_speed); //총알 발사
        }
    }
    public void Fire(Vector3 dir)
    {
        Enemy_direction = dir;
        Enemy_isFire = true;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            Destroy(gameObject);
            if (other.gameObject.GetComponent<PlayerController>().ClonePrefab[1] != null)
            {
                //아군이 있을 때 가장 뒤에 있는 아군 삭제
                GameObject.Find("Player").GetComponent<PlayerController>().ClonePrefab[GameObject.Find("Player").GetComponent<PlayerController>().CloneCount - 1].transform.parent.GetComponent<CloneController>().CloneHP-=1;

                Destroy(gameObject);
            }
            else if (other.gameObject.GetComponent<PlayerController>().ClonePrefab[1] == null)
            {
                //아군이 없을 때 플레이어 체력 감소
                other.gameObject.GetComponent<PlayerController>().playerHP -= 1;
                Destroy(gameObject);
            }
        }
        if (other.transform.tag == "Object")
        {
            Destroy(gameObject);
        }
        if (other.transform.tag == "Bullet")
        {
            Destroy(gameObject);
        }
        if (other.transform.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
