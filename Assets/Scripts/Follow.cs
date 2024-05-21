using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target; //따라나딜 오브젝트
    public Vector3 offset; //카메라 위치

    void Update()
    {
        transform.position = target.position + offset; //타겟 오브젝트로 이동
    }
}
