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
    public float speed = 5f;

        //��Ʋ�׶���ó�� Alt�� ������ �ѷ����� �ϴ� ���.
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
            //�̵�����
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
    

}
