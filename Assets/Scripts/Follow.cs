using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target; //���󳪵� ������Ʈ
    public Vector3 offset; //ī�޶� ��ġ

    void Update()
    {
        transform.position = target.position + offset; //Ÿ�� ������Ʈ�� �̵�
    }
}
