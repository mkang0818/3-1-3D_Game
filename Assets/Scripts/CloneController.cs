using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloneController : MonoBehaviour
{
    NavMeshAgent agent;//navmesh agent 선언

    Animator Anim;

    public GameObject Sleep_img; //자는 표시 오브젝트
    public GameObject player; //플레이어
    public GameObject bulletPrefab; //총알 프리팹
    public Transform target; //아군이 따라갈 타겟 오브젝트

    public int CloneHP = 2; //아군 체력
    public float C_AttackSpeed = 0.5f; //아군 속도
    public int CloneNum; //아군 개수
    public bool Isfree = false; //아군 확인 bool변수

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        C_AttackSpeed -= Time.deltaTime;

        if (Isfree)
        {
            Anim.SetTrigger("Walk"); //걷는 애니메이션

            UpdateClone(); //아군 꼬리잡기 구현
            isDead();  //죽었는지 
            MouseLookAt(); //마우스방향으로 바라보기
            if (C_AttackSpeed < 0)  Fire(); //총알 발사
        }
    }
    void UpdateClone()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 6 * Time.deltaTime); //타겟위치로 이동
        }
        else if (target == null)
        {
            Sleep_img.SetActive(false); //자는 이모티콘 비활성화
            
            if (CloneNum >= 2) CloneNum -= 1;   //타겟이 삭제되면 앞에 클론을 target으로 변경하기 위해 CloneNum -1
            else target = player.GetComponent<PlayerController>().ClonePrefab[0].transform; //제일 앞 클론이 죽으면 두번째 클론은 플레이어(0) 타겟지정

            target = player.GetComponent<PlayerController>().ClonePrefab[CloneNum - 1].transform; //타겟을 3->1 이렇게 재지정
            player.GetComponent<PlayerController>().ClonePrefab[CloneNum] = gameObject; //만약 2번일때 [1]로 변경해주고 밑에 포문에서 전부 밀어주기

            for (int i = CloneNum; i < player.GetComponent<PlayerController>().CloneCount; i++)
            {
                player.GetComponent<PlayerController>().ClonePrefab[i] = player.GetComponent<PlayerController>().ClonePrefab[i + 1];
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, 6 * Time.deltaTime); //타겟 위치로 이동
        }
    }
    void isDead()
    {
        if (CloneHP <= 0) //hp 0이하일 때
        {
            GameObject.Find("Player").GetComponent<PlayerController>().CloneCount -= 1; //클론 개수 감소
            Destroy(gameObject); //클론 삭제
        }
    }
    void Fire()
    {
        if (Input.GetMouseButtonDown(0)) //좌클릭 시
        {
            Vector3 firePos = transform.position + transform.forward + new Vector3(0f, 0.5f, 0f); //총알 발사위치 변수 선언
            var bullet = Instantiate(bulletPrefab, firePos, Quaternion.identity).GetComponent<Bullet>(); //총알 생성
            bullet.Fire(transform.forward); //총알 발사

            C_AttackSpeed = 0.3f; //공격 쿨타임 초기화
        }
    }
    void MouseLookAt() //마우스 위치 받아오기
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Sleep_img.SetActive(false);
        }
    }
}