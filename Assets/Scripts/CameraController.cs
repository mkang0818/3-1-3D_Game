using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; //따라다닐 오브젝트
    public Vector3 offset; //카메라 위치

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset; //타겟 오브젝트 위치로 이동
    }
}
