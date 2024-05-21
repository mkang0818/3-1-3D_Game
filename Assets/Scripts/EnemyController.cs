using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent; //NavMesh
    Animator Enemy_Anim; //�ִϸ�����
    new Rigidbody rigidbody;
    Transform playertarget; //�÷��̾� ��ġ��

    public GameObject player; //�÷��̾� ������Ʈ 
    public GameObject EnemybulletPrefab; //���� �Ѿ� ������
    private Vector3 targetPosition; //Ÿ�� ��ġ��

    public float Enemy_Speed; //���� �̵��ӵ�
    public int range; //���� ����
    public int EnemyHp = 2; //���� ü��
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
        //������Ʈ �и� ����
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        lookPlayer();
        isDead();

        if (GameObject.Find("GameDirector").GetComponent<GameDirector>().playerType != 3) FindPlayer(); //�������,�ϵ����� ��
        else if (GameObject.Find("GameDirector").GetComponent<GameDirector>().playerType == 3) //�����̹� ����� ��
        {
            Enemy_Speed *= 3f; //�ӵ� 3�� ����
            agent.SetDestination(playertarget.position); //NAVMESH ����
            var distance = Vector3.Distance(this.transform.position, playertarget.position); //�Ÿ� ���� ����

            if (distance <= range) EnemyFire(); //���� �� �÷��̾� �߰� �� �Ѿ� �߻�
        }
    }
    void lookPlayer()
    {
        targetPosition = new Vector3(playertarget.position.x, transform.position.y, playertarget.position.z); //�÷��̾� ��ġ�� 
        transform.LookAt(targetPosition); //�÷��̾� �ٶ󺸱�
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
    void FindPlayer() //�÷��̾� Ž��
    {
        var distance = Vector3.Distance(this.transform.position, playertarget.position); //�Ÿ� ���� ����

        if (distance <= range) //�Ÿ� ������ ���� �������� �۾� �� ��
        {
            agent.SetDestination(playertarget.position); //NAVMESH��� ����
            EnemyFire(); //�Ѿ˹߻� �Լ� ȣ��
        }
    }
    void EnemyFire() //�Ѿ� �߻�
    {
        if (!FirstBullet)
        {
            InvokeRepeating("newBullet", 0f, 1f);
        }
    }
    void newBullet()
    {
        Enemy_Anim.SetBool("Enemy_Attack", true); //���� �ִϸ��̼�

        Vector3 EnemyfirePos = transform.position + transform.forward + new Vector3(0f, 0.5f, 0f); //�Ѿ� �߻� ��ġ ���� ����
        var Enemybullet = Instantiate(EnemybulletPrefab, EnemyfirePos, Quaternion.identity).GetComponent<EnemyBullet>(); //�Ѿ� ����
        Enemybullet.Fire(transform.forward); //�Ѿ� �߻�
        FirstBullet = true;
    }
    private void OnDrawGizmosSelected() //���������� ���� ǥ���Լ�
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
