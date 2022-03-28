using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    Camera _camera;
    Rigidbody _rigid;
    //������ �÷��̾�
    //ī�޶�������
    [SerializeField]
    Transform cameraPos;
    // �̵��ӵ�
    public float speed;
    // �뽬�ӵ�
    public float dashspeed;
    // �뽬����
    public float dashing;
    // �뽬
    private bool isDodge;
    // �뽬�ܻ�
    public GameObject tail;
    //��Ʋ�׶���ó�� Alt�� ������ �ѷ����� �ϴ� ���.
    public bool togglecameraRotation;
    public float smoothness = 10f;
    // �뽬 �غ�
    public bool wDodgeready, aDodgeready, sDodgeready, dDodgeready;
    // �����ð� �̻��� Walk����
    public bool wWalking, aWalking, sWalking, dWalking;
    // Ű�� �°� ������ �ð�
    float wDodgeCheck, aDodgeCheck, sDodgeCheck, dDodgeCheck = 0.3f;
    // Ű�� �ٽ� �������� �ð�
    float wWalkTime, aWalkTime, sWalkTime, dWalkTime = 0.15f;
    bool Idle;

    void Start()
    {
        _camera = Camera.main;
        _rigid = this.GetComponent<Rigidbody>();
    }


    void Update()
    {
        LookAround();
        InputMoveMent();
        Dodge();
        idle();
    }


    void InputMoveMent()
    {
        //�̵�����
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 lookForward = new Vector3(cameraPos.forward.x, 0f, cameraPos.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraPos.right.x, 0f, cameraPos.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
        transform.position += moveDir * Time.deltaTime * speed;
    }
    void Tail()
    {
        speed = dashspeed;
        isDodge = true;
        tail.SetActive(true);
        Invoke("TailOut", 0.5f);
        Invoke("DodgeOut", dashing);
        wDodgeready = false;
        aDodgeready = false;
        sDodgeready = false;
        dDodgeready = false;
        wWalking = false;
        aWalking = false;
        sWalking = false;
        dWalking = false;
     
    }
    void idle()
    {
        Idle = _rigid.velocity == Vector3.zero;
        if (Idle)
        {
            wWalking = false;
            aWalking = false;
            sWalking = false;
            dWalking = false;
            wWalkTime = 0.3f;
            aWalkTime = 0.3f;
            sWalkTime = 0.3f;
            dWalkTime = 0.3f;     
        }
    }
    void TailOut()
    {
        tail.SetActive(false);
    }
    void DodgeOut()
    {
        speed = 10;
        isDodge = false;
    }
    void LookAround()
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
            Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.EndGame();
    }
    void Dodge()
    {
        WDodge();
        ADodge();
        SDodge();
        DDodge();
    }
    void WDodge()
    {
        // WŰ
        if (Input.GetKey(KeyCode.W))
        { wWalkTime -= Time.deltaTime; }

        if (wWalkTime < 0)
        { wWalking = true; }
        else
        { wWalking = false; }

        if (wDodgeready)
        { wDodgeCheck -= Time.deltaTime; }

        if (wDodgeCheck < 0)
        { wDodgeready = false; }

        if (Input.GetKeyUp(KeyCode.W) && wDodgeready == false)
        {
            wDodgeready = true;
            wDodgeCheck = 0.3f;
        }
        else if (Input.GetKeyUp(KeyCode.W) && wWalking == true)
        { wWalkTime = 0.15f; }
        if (Input.GetKeyDown(KeyCode.W) && wDodgeready == true && wWalking == false)
        {
            Tail();
        }
    }
    void ADodge()
    {
        // AŰ
        if (Input.GetKey(KeyCode.A))
        { aWalkTime -= Time.deltaTime; }

        if (aWalkTime < 0)
        { aWalking = true; }
        else
        { aWalking = false; }


        if (aDodgeready)
        { aDodgeCheck -= Time.deltaTime; }


        if (aDodgeCheck < 0)
        { aDodgeready = false; }

        if (Input.GetKeyUp(KeyCode.A) && aDodgeready == false)
        {
            aDodgeready = true;
            aDodgeCheck = 0.3f;
        }
        else if (Input.GetKeyUp(KeyCode.A) && aWalking == true)
        { aWalkTime = 0.15f; }
        if (Input.GetKeyDown(KeyCode.A) && aDodgeready == true && aWalking == false)
        {
            Tail();
        }
    }
    void SDodge()
    {
        // SŰ
        if (Input.GetKey(KeyCode.S))
        { sWalkTime -= Time.deltaTime; }

        if (sWalkTime < 0)
        { sWalking = true; }
        else
        { sWalking = false; }


        if (sDodgeready)
        { sDodgeCheck -= Time.deltaTime; }


        if (sDodgeCheck < 0)
        { sDodgeready = false; }

        if (Input.GetKeyUp(KeyCode.S) && sDodgeready == false)
        {
            sDodgeready = true;
            sDodgeCheck = 0.3f;
        }
        else if (Input.GetKeyUp(KeyCode.S) && sWalking == true)
        { sWalkTime = 0.15f; }
        if (Input.GetKeyDown(KeyCode.S) && sDodgeready == true && sWalking == false)
        {
            Tail();
        }
    }
    void DDodge()
    {
        // DŰ
        if (Input.GetKey(KeyCode.D))
        { dWalkTime -= Time.deltaTime; }

        if (dWalkTime < 0)
        { dWalking = true; }
        else
        { dWalking = false; }


        if (dDodgeready)
        { dDodgeCheck -= Time.deltaTime; }


        if (dDodgeCheck < 0)
        { dDodgeready = false; }

        if (Input.GetKeyUp(KeyCode.D) && dDodgeready == false)
        {
            dDodgeready = true;
            dDodgeCheck = 0.3f;
        }
        else if (Input.GetKeyUp(KeyCode.D) && dWalking == true)
        { dWalkTime = 0.15f; }
        if (Input.GetKeyDown(KeyCode.D) && dDodgeready == true && dWalking == false)
        {
            Tail();
        }
    }


    }
    





