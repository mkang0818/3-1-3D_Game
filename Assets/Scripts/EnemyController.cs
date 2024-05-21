using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent; //NavMesh
    Animator Enemy_Anim; //애니메이터
    new Rigidbody rigidbody;
    Transform playertarget; //플레이어 위치값

    public GameObject player; //플레이어 오브젝트 
    public GameObject EnemybulletPrefab; //몬스터 총알 프리팹
    private Vector3 targetPosition; //타겟 위치값

    public float Enemy_Speed; //몬스터 이동속도
    public int range; //몬스터 범위
    public int EnemyHp = 2; //몬스터 체력
    bool FirstBullet = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Enemy_Speed = agent.speed;
        Enemy_Anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        playertarget = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        //오브젝트 밀림 방지
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        lookPlayer();
        isDead();

        if (GameObject.Find("GameDirector").GetComponent<GameDirector>().playerType != 3) FindPlayer(); //이지모드,하드모드일 때
        else if (GameObject.Find("GameDirector").GetComponent<GameDirector>().playerType == 3) //서바이벌 모드일 때
        {
            Enemy_Speed *= 3f; //속도 3배 증가
            agent.SetDestination(playertarget.position); //NAVMESH 시작
            var distance = Vector3.Distance(this.transform.position, playertarget.position); //거리 변수 선언

            if (distance <= range) EnemyFire(); //범위 내 플레이어 발견 시 총알 발사
        }
    }
    void lookPlayer()
    {
        targetPosition = new Vector3(playertarget.position.x, transform.position.y, playertarget.position.z); //플레이어 위치값 
        transform.LookAt(targetPosition); //플레이어 바라보기
    }
    void isDead()
    {
        if (EnemyHp <= 0)
        {
            if (GameObject.Find("GameDirector") != null) GameObject.Find("GameDirector").GetComponent<GameDirector>().Enemy_Num -= 1;
            if (GameObject.Find("GameDirector") != null) GameObject.Find("GameDirector").GetComponent<GameDirector>().Enemy_Count += 1;
            Destroy(gameObject);
        }
    }
    void FindPlayer() //플레이어 탐지
    {
        var distance = Vector3.Distance(this.transform.position, playertarget.position); //거리 변수 선언

        if (distance <= range) //거리 변수가 범위 변수보다 작아 질 때
        {
            agent.SetDestination(playertarget.position); //NAVMESH기능 시작
            EnemyFire(); //총알발사 함수 호출
        }
    }
    void EnemyFire() //총알 발사
    {
        if (!FirstBullet)
        {
            InvokeRepeating("newBullet", 0f, 1f);
        }
    }
    void newBullet()
    {
        Enemy_Anim.SetBool("Enemy_Attack", true); //공격 애니메이션

        Vector3 EnemyfirePos = transform.position + transform.forward + new Vector3(0f, 0.5f, 0f); //총알 발사 위치 변수 선언
        var Enemybullet = Instantiate(EnemybulletPrefab, EnemyfirePos, Quaternion.identity).GetComponent<EnemyBullet>(); //총알 생성
        Enemybullet.Fire(transform.forward); //총알 발사
        FirstBullet = true;
    }
    private void OnDrawGizmosSelected() //빨간색으로 범위 표시함수
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
