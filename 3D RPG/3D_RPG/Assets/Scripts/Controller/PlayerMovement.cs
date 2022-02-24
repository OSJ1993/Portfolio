using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator _animator;
    Camera _camera;
    CharacterController _controller;




    //플레이어 속도. 
    public float speed = 5f;
    public float runSpeed = 8f;



    //최종 결정 스피드.
    public float finalSpeed;


    //배틀그라운드처럼 Alt를 누르면 둘러보게 하는 기능.
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
            //둘러보기 활성.
            togglecameraRotation = true;

        }
        else
        {
            //둘러보기 비활성.
            togglecameraRotation = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }

        InputMoveMent();
        Zoom();
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

        //최종적으로 움직일 방향.
        Vector3 moveDireation = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

        if (moveDireation != Vector3.zero)
        {
            _controller.GetComponent<Animator>().SetBool("Walk", true);
        }
        if (moveDireation == Vector3.zero)
        {
            _controller.GetComponent<Animator>().SetBool("Walk", false);
        }


        _controller.Move(moveDireation.normalized * finalSpeed * Time.deltaTime);

        float percent = ((run) ? 1 : 0.5f) * moveDireation.magnitude;



    }
    public void Zoom()
    {
        var scroll = Input.mouseScrollDelta;
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView - scroll.y, 30f, 70f);

    }
}
