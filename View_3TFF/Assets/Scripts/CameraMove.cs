using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Target;

    [SerializeField]
    private float speed;

    // 현재 보고 있는 카메라 시점과 타겟의 거리만큼 유지한 상태로 따라가게 하기.
    private Vector3 difValue;

    void Start()
    {
        difValue = transform.position = go_Target.transform.position;
        difValue = new Vector3(Mathf.Abs(difValue.x), Mathf.Abs(difValue.y), Mathf.Abs(difValue.z));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
