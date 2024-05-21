using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloneController : MonoBehaviour
{
    NavMeshAgent agent;//navmesh agent ����

    Animator Anim;

    public GameObject Sleep_img; //�ڴ� ǥ�� ������Ʈ
    public GameObject player; //�÷��̾�
    public GameObject bulletPrefab; //�Ѿ� ������
    public Transform target; //�Ʊ��� ���� Ÿ�� ������Ʈ

    public int CloneHP = 2; //�Ʊ� ü��
    public float C_AttackSpeed = 0.5f; //�Ʊ� �ӵ�
    public int CloneNum; //�Ʊ� ����
    public bool Isfree = false; //�Ʊ� Ȯ�� bool����

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
            Anim.SetTrigger("Walk"); //�ȴ� �ִϸ��̼�

            UpdateClone(); //�Ʊ� ������� ����
            isDead();  //�׾����� 
            MouseLookAt(); //���콺�������� �ٶ󺸱�
            if (C_AttackSpeed < 0)  Fire(); //�Ѿ� �߻�
        }
    }
    void UpdateClone()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 6 * Time.deltaTime); //Ÿ����ġ�� �̵�
        }
        else if (target == null)
        {
            Sleep_img.SetActive(false); //�ڴ� �̸�Ƽ�� ��Ȱ��ȭ
            
            if (CloneNum >= 2) CloneNum -= 1;   //Ÿ���� �����Ǹ� �տ� Ŭ���� target���� �����ϱ� ���� CloneNum -1
            else target = player.GetComponent<PlayerController>().ClonePrefab[0].transform; //���� �� Ŭ���� ������ �ι�° Ŭ���� �÷��̾�(0) Ÿ������

            target = player.GetComponent<PlayerController>().ClonePrefab[CloneNum - 1].transform; //Ÿ���� 3->1 �̷��� ������
            player.GetComponent<PlayerController>().ClonePrefab[CloneNum] = gameObject; //���� 2���϶� [1]�� �������ְ� �ؿ� �������� ���� �о��ֱ�

            for (int i = CloneNum; i < player.GetComponent<PlayerController>().CloneCount; i++)
            {
                player.GetComponent<PlayerController>().ClonePrefab[i] = player.GetComponent<PlayerController>().ClonePrefab[i + 1];
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, 6 * Time.deltaTime); //Ÿ�� ��ġ�� �̵�
        }
    }
    void isDead()
    {
        if (CloneHP <= 0) //hp 0������ ��
        {
            GameObject.Find("Player").GetComponent<PlayerController>().CloneCount -= 1; //Ŭ�� ���� ����
            Destroy(gameObject); //Ŭ�� ����
        }
    }
    void Fire()
    {
        if (Input.GetMouseButtonDown(0)) //��Ŭ�� ��
        {
            Vector3 firePos = transform.position + transform.forward + new Vector3(0f, 0.5f, 0f); //�Ѿ� �߻���ġ ���� ����
            var bullet = Instantiate(bulletPrefab, firePos, Quaternion.identity).GetComponent<Bullet>(); //�Ѿ� ����
            bullet.Fire(transform.forward); //�Ѿ� �߻�

            C_AttackSpeed = 0.3f; //���� ��Ÿ�� �ʱ�ȭ
        }
    }
    void MouseLookAt() //���콺 ��ġ �޾ƿ���
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