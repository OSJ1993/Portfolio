using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public Rigidbody rigid;
    Camera _camera;
    //���� ���� ���ǵ�.
    public float finalSpeed;
    //�뽬 ���ǵ�
    public float dashspeed;
    //bool dash;
    Animator anim;
    //���ó�� alt ����
    //���콺 ����.
    public float sensitivity = 100f;
    CharacterController _controller;


    bool att;

    //����
    public bool togglecameraRotation;
    public bool run;
    public float smoothness = 10f;
    //����



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
        //�̵� �Է� ��
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0; // �̵��Է� �߻�üũ
       bool backward = Input.GetKeyDown(KeyCode.S);
       anim.SetBool("isMove", isMove);
        anim.SetBool("isBackward", backward);
        if(isMove)
        {

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            //���������� ������ ����.
            Vector3 moveDireation = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

            //_controller.Move(moveDireation.normalized * finalSpeed * Time.deltaTime);

            //����
            float percent = ((run) ? 1 : 1f) * moveDireation.magnitude;
            //����

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
            //�ѷ����� Ȱ��.
            togglecameraRotation = true;
          
        }
        else
        {
            //�ѷ����� ��Ȱ��.
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
