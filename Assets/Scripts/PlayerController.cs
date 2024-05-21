using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    AudioSource Player_Audio; //플레이어 사운드
    public Transform player_Dir; //플레이어 
    public GameObject Player_Pos; // 플레이어 위치 오브젝트
    public GameObject Enemy_bul; // 몬스터 총알
    public GameObject Enemy; //몬스터

    //오브젝트형 UI 변수
    public GameObject UI_EasyMode;
    public GameObject UI_HardMode; 
    public GameObject UI_SurvivalMode;
    public GameObject UI_GameOver;
    public GameObject UI_GameClear;

    [SerializeField]
    private Animator animator; //애니메이터

    [SerializeField]
    public GameObject[] ClonePrefab = new GameObject[30]; //빈 클론오브젝트 배열
    public GameObject bulletPrefab; //총알 프리팹

    public int playerHP = 2; //플레이어 체력
    public int CloneCount = 0; //아군 개수
    public float Speed;  //플레이어 스피드값
    public float AttackSpeed = 0.3f; //플레이어 공격 속도
    public int PlayMode = 0; //INT형으로 표시하는 플레이 모드

    [HideInInspector]
    public Vector3 moveVec;
    // 상하좌우 변수
    public float hAxis;
    public float vAxis;
    public bool Is_Success = false; //성공 여부
    public bool Is_Fail = false; //성공 여부

    Rigidbody rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        Player_Audio = GetComponent<AudioSource>();
        ClonePrefab[CloneCount] = Player_Pos;
        CloneCount++;
        Is_Success = false;
        Is_Fail = false;
    }
    // Update is called once per frame
    void Update()
    {
        AttackSpeed -= Time.deltaTime; //공격 쿨타임 줄어드는 문
        GetComponent<Rigidbody>().velocity = Vector3.zero; //오브젝트 충돌 시 밀려남 방지
        if (Is_Success) if(UI_GameClear != null) UI_GameClear.SetActive(true); //게임 클리어 시 UI 활성화
        if (Is_Fail) if(UI_GameOver != null) UI_GameOver.SetActive(true); //게임 오버 시 UI 활성화

        InputMove(); //키 입력 시 이동 함수 호출
        LookMouseCursor(); //마우스 쪽 바라보는 함수 호출
        if (AttackSpeed < 0) Fire(); //쿨타임 충족 시 총알 발사
        IsDead(); //죽음
        slowMode(); //타임슬로우모드 함수 호출
    }
    void slowMode()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) //컨트롤 키를 누르고 있을 때
        {
            Enemy_bul.GetComponent<EnemyBullet>().EnemyBullet_speed = 2.0f; //적총알 속도 감소
            Enemy.GetComponent<EnemyController>().Enemy_Speed = 0.08f; //적 이동속도 감소
        }
        if (Input.GetKeyUp(KeyCode.LeftControl)) //컨트롤 키를 뗏을 때
        {
            Enemy_bul.GetComponent<EnemyBullet>().EnemyBullet_speed = 10.0f; //적총알 속도 초기화 
            Enemy.GetComponent<EnemyController>().Enemy_Speed = 0.5f; //적 이동속도 초기화
        }
    }
    void IsDead()
    {
        //by 민준, 체력
        if (playerHP <= 0) 
        {
            if (CloneCount > 1) //아군이 1명 이상일 때
            {
                Destroy(ClonePrefab[CloneCount].gameObject); //맨 뒤 아군 삭제
                --CloneCount; //아군개수 1감소
            }
            if (CloneCount == 1) //아군이 1명일 때
            {
                //게임 오버 UI 활성화
                UI_GameOver.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
    void InputMove()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized; //벡터 정규화
        player_Dir.transform.rotation = Quaternion.Lerp(player_Dir.transform.rotation, Quaternion.LookRotation(moveVec), Time.deltaTime * 4); //플레이어 방향키 이미지 방향조절

        transform.position += moveVec * Speed * Time.deltaTime; // 정규화된 벡터값과 스피드값을 곱하여 위치 이동
        animator.SetBool("Is_Run", moveVec != Vector3.zero); //걷는 애니메이션 추가
    }
    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Player_Audio.Play();
            animator.SetTrigger("do_Attack"); //by 민준, 공격 애니메이션

            Vector3 firePos = transform.position + transform.forward + new Vector3(0f, 0.5f, 0f); //by 민준, 총알 발사위치 설정
            var bullet = Instantiate(bulletPrefab, firePos, Quaternion.identity).GetComponent<Bullet>(); //by 민준, 총알 생성
            bullet.Fire(transform.forward); //by 민준, 오브젝트 앞방향으로 발사

            AttackSpeed = 0.3f; //by 민준, 공격속도 초기화
        }
    }
    public void LookMouseCursor() //by 민준, 마우스 방향 바라보기
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;
        if (Physics.Raycast(ray, out hitResult))
        {
            Vector3 mouseDir = new Vector3(hitResult.point.x, transform.position.y, hitResult.point.z) - transform.position;
            transform.forward = mouseDir;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //by 민준, 아군과 충돌 시
        if (other.transform.tag == "Clone")
        {
            if (!other.gameObject.GetComponent<CloneController>().Isfree)
            {
                //by 민준, 가만히 있는 아군과 충돌 시 
                //가만히 있는 클론과 충돌 시 ClonePrefab[CloneCount]에 저장하여 따라다니는 클론으로 변경
                ClonePrefab[CloneCount] = other.gameObject.transform.GetChild(0).gameObject; //아군배열 가장 마지막에 저장
                other.gameObject.GetComponent<CloneController>().target = ClonePrefab[CloneCount - 1].GetComponent<Transform>(); //충돌아군의 타겟을 가장 끝 배열에 위치한 다른 아군을 타겟으로 변경
                other.gameObject.GetComponent<CloneController>().CloneNum = CloneCount; //클론마다 배열수를 지정
                CloneCount++; //아군 개수 1증가

                other.gameObject.GetComponent<CloneController>().Isfree = true; //충돌 체크 bool변수 true
                bulletPrefab.GetComponent<Bullet>().IsDead = true;

                if(GameObject.Find("GameDirector") != null) GameObject.Find("GameDirector").GetComponent<GameDirector>().Animal_Num -= 1;
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        //by 민준, 모드 오브젝트와 충돌 시 UI 활성화
        if (other.transform.tag == "EasyMode")
        {
            UI_EasyMode.SetActive(true);
        }
        else if (other.transform.tag == "HardMode")
        {
            UI_HardMode.SetActive(true);
        }
        else if (other.transform.tag == "SurvivalMode")
        {
            UI_SurvivalMode.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision other)
    {
        //by 민준, 모드 오브젝트와 충돌 시 UI 비활성화
        if (other.transform.tag == "EasyMode")
        {
            UI_EasyMode.SetActive(false);
        }
        else if (other.transform.tag == "HardMode")
        {
            UI_HardMode.SetActive(false);
        }
        else if (other.transform.tag == "SurvivalMode")
        {
            UI_SurvivalMode.SetActive(false);
        }
    }
}
