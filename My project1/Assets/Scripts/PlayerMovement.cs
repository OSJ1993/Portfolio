using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator _animator;
    Camera _camera;
    CharacterController _controller;

    public float speed = 5f;
    public float runSpeed = 8f;

    //���� ���� ���ǵ�.
    public float finalSpeed;



    //��Ʋ�׶���ó�� Alt�� ������ �ѷ����� �ϴ� ���.
    public bool togglecameraRotation;

    public bool run;

    public float smoothness = 10f;

    void Start()
    {
        _animator = this.GetComponent<Animator>();
        _camera = Camera.main;
        _controller = this.GetComponent<CharacterController>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            //�ѷ����� Ȱ��.
            togglecameraRotation = true;

        }
        else
        {
            //�ѷ����� ��Ȱ��.
            togglecameraRotation = false;
        }


        InputMoveMent();
    }

    private void LateUpdate()
    {
        if (togglecameraRotation != true)
        {
            Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }
    }

    void InputMoveMent()
    {
        finalSpeed = (run) ? runSpeed : speed;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //���������� ������ ����.
        Vector3 moveDireation = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

        _controller.Move(moveDireation.normalized * finalSpeed * Time.deltaTime);

        float percent = ((run) ? 1 : 0.5f) * moveDireation.magnitude;
        _animator.SetFloat("Blend", percent, 0.1f, Time.deltaTime);
    }
}
