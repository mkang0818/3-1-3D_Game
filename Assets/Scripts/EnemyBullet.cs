using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Vector3 Enemy_direction;

    public float EnemyBullet_speed = 6f; //�� �Ѿ� �ӵ�
    bool Enemy_isFire; //�� �Ѿ� �߻� üũ bool����

    void Start()
    {
        Destroy(gameObject, 0.7f); //0.7�� �� ����
    }
    void Update()
    {
        if (Enemy_isFire)
        {
            transform.Translate(Enemy_direction * Time.deltaTime * EnemyBullet_speed); //�Ѿ� �߻�
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
                //�Ʊ��� ���� �� ���� �ڿ� �ִ� �Ʊ� ����
                GameObject.Find("Player").GetComponent<PlayerController>().ClonePrefab[GameObject.Find("Player").GetComponent<PlayerController>().CloneCount - 1].transform.parent.GetComponent<CloneController>().CloneHP-=1;

                Destroy(gameObject);
            }
            else if (other.gameObject.GetComponent<PlayerController>().ClonePrefab[1] == null)
            {
                //�Ʊ��� ���� �� �÷��̾� ü�� ����
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
