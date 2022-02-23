using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    public Rigidbody rigid;
    Camera _camera;
    CharacterController _controller;
    public float speed;
    public float dashspeed;
    //bool dash;
    Animator anim;
    public bool togglecameraRotation;
    public float CameraSpeed;


    public float smoothness = 10f;

    bool att;



    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        // dash = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
       // bool backward = Input.GetKeyDown(KeyCode.S);
       anim.SetBool("isMove", isMove);
      //  anim.SetBool("isBackward", backward)
        if(isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            
            characterBody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * speed;
        
        }
       
    }


    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))

            rigid.AddRelativeForce(Vector3.forward * dashspeed, ForceMode.VelocityChange);
    }
    private void LookAround()
    {
        // ���� ���� ���� ���� ����
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles; //���Ϸ�����ȯ
        float x = camAngle.x - mouseDelta.y*CameraSpeed;
        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x*CameraSpeed, camAngle.z);

        

        // ī�޶� ������ ���������� ����
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);

        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }
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
            Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
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
