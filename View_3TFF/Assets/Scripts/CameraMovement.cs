using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //���󰡾���  ������Ʈ�� ����
    public Transform objectTofollow;

    //���󰡾��� ���ǵ�
    public float followSpeed = 10f;

    //���콺 ����.
    public float sensitivity = 100f;

    //ī�޶� ����(�ޱ� ����)
    public float clampAngle = 70f;

    //���콺 ��ǲ
    private float rotX;
    private float rotY;

    //ī�޶� ����,
    public Transform realCamera;

    //ī�޶� ����.
    public Vector3 dirNormalized;

    //�������� ����.
    public Vector3 finalDir;

    //ī�޶�� �÷��̾��� �ּҰŸ�, �ִ�Ÿ�.
    public float minDistance;
    public float maxDistance;

    //����������?
    public float finalDistance;

    public float smoothness=10f;

    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;

        //���콺 Ŀ�� �� ���̰� �ϱ�.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
         
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        //�ּҰ�, �ִ밪
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }

    //������Ʈ�� ������ �����ϴ� ������Ʈ(LateUpdate)
    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;

        if (Physics.Linecast(transform.position,finalDir,out hit))
        {
            //���࿡ ���� �ִٸ�.
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else //���࿡ ���� ���ٸ�.
        {
            finalDistance = maxDistance;
        }

        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
    }
}
