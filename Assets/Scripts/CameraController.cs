using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; //����ٴ� ������Ʈ
    public Vector3 offset; //ī�޶� ��ġ

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset; //Ÿ�� ������Ʈ ��ġ�� �̵�
    }
}
