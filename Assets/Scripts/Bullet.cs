using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    AudioSource Audio;
    Vector3 direction;
    public GameObject player; //플레이어 오브젝트

    bool isFire; //총알 발사 bool함수
    float Player_speed = 15f; //총알 속도
    public bool IsDead = false;

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        Destroy(gameObject,0.7f); //0.7초 후 삭제
    }

    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
            transform.Translate(direction * Time.deltaTime * Player_speed); //총알 앞으로 이동
        }
    }
    public void Fire(Vector3 dir)
    {
        direction = dir;
        isFire = true;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Object")
        {
            Destroy(gameObject);
        }
        if (other.transform.tag == "Bullet")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (other.transform.tag =="Player")
        {
            if(GameObject.Find("Player").GetComponent<PlayerController>().ClonePrefab[1] != null)
            {
                Destroy(GameObject.Find("Player").GetComponent<PlayerController>().ClonePrefab[GameObject.Find("Player").GetComponent<PlayerController>().CloneCount - 1].transform.parent.gameObject);
                GameObject.Find("Player").GetComponent<PlayerController>().CloneCount -= 1;
            }
            if (GameObject.Find("Player").GetComponent<PlayerController>().ClonePrefab[1] == null)
            {
                other.gameObject.GetComponent<PlayerController>().playerHP -= 1;
            }
        }
        
        if (other.transform.tag == "Enemy")
        {
            Audio.Play();
            other.gameObject.GetComponent<EnemyController>().EnemyHp -= 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Clone")
        {
            GameObject.Find("Player").GetComponent<PlayerController>().ClonePrefab[GameObject.Find("Player").GetComponent<PlayerController>().CloneCount - 1].transform.parent.GetComponent<CloneController>().CloneHP -= 1;
        }
    }
}
