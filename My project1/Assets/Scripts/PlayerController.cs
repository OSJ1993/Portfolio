using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public Rigidbody rigid;
    Camera _camera;
    //최종 결정 스피드.
    public float finalSpeed;
    //대쉬 스피드
    public float dashspeed;
    //bool dash;
    Animator anim;
    //배그처럼 alt 시점
    //마우스 감도.
    public float sensitivity = 100f;
    CharacterController _controller;


    bool att;

    //승주
    public bool togglecameraRotation;
    public bool run;
    public float smoothness = 10f;
    //승주



    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        // dash = false;
        
        _camera = Camera.main;
        _controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        Attack();
        animset();
        Dash();
        LookAround();

    }

    void Move()
    {
        //이동 입력 값
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0; // 이동입력 발생체크
       bool backward = Input.GetKeyDown(KeyCode.S);
       anim.SetBool("isMove", isMove);
        anim.SetBool("isBackward", backward);
        if(isMove)
        {

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            //최종적으로 움직일 방향.
            Vector3 moveDireation = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

            //_controller.Move(moveDireation.normalized * finalSpeed * Time.deltaTime);

            //승주
            float percent = ((run) ? 1 : 1f) * moveDireation.magnitude;
            //승주

        }

    }


    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))

            rigid.AddRelativeForce(Vector3.forward * dashspeed, ForceMode.VelocityChange);
    }
    private void LookAround()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

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
        if (togglecameraRotation != true)
        {
            Vector3 playerrotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(playerrotate), Time.deltaTime * smoothness);

        }


    }
        void Attack()
        {
            att = Input.GetButton("Attack");
        }

        void animset()
        {
            anim.SetBool("isAttack", att);
            //  anim.SetBool("isDash", dash);


        }
    } 
