using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Target;

    [SerializeField]
    private float speed;

    // ���� ���� �ִ� ī�޶� ������ Ÿ���� �Ÿ���ŭ ������ ���·� ���󰡰� �ϱ�.
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
