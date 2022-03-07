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
    public float speed = 5f;

        //배틀그라운드처럼 Alt를 누르면 둘러보게 하는 기능.
    public bool togglecameraRotation;


    public float smoothness = 10f;

    void Start()
    {
        _camera = Camera.main;
        _rigid = this.GetComponent<Rigidbody>();
    }


    void Update()
    {
        LookAround();
        InputMoveMent();   
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
    

}
