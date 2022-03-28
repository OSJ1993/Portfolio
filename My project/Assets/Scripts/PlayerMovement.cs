using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    Camera _camera;
    Rigidbody _rigid;
    //지정할 플레이어
    //카메라포지션
    [SerializeField]
    Transform cameraPos;
    // 이동속도
    public float speed;
    // 대쉬속도
    public float dashspeed;
    // 대쉬지속
    public float dashing;
    // 대쉬
    private bool isDodge;
    // 대쉬잔상
    public GameObject tail;
    //배틀그라운드처럼 Alt를 누르면 둘러보게 하는 기능.
    public bool togglecameraRotation;
    public float smoothness = 10f;
    // 대쉬 준비
    public bool wDodgeready, aDodgeready, sDodgeready, dDodgeready;
    // 일정시간 이상은 Walk상태
    public bool wWalking, aWalking, sWalking, dWalking;
    // 키를 뗏고 나서의 시간
    float wDodgeCheck, aDodgeCheck, sDodgeCheck, dDodgeCheck = 0.3f;
    // 키를 다시 누르고의 시간
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
        //이동구현
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
            //둘러보기 활성.
            togglecameraRotation = true;
        }
        else
        {
            //둘러보기 비활성.
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
        // W키
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
        // A키
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
        // S키
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
        // D키
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
    





