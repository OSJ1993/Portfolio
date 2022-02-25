using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    Animator playerAnim;

    //�޺� �Է� ���� üũ.
    bool comboPossible;

    //�޺� ���� �ܰ�.
    public int comboStep;

    //���Ž� �Է� ���� üũ
    bool inputSmash;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    //�޺��� ������. �޺��� �����ϰ� ���ش�.
    public void ComboPossible()
    {
        comboPossible = true;
    }

    //�Էµ� Ű�� �޺� �ܰ迡 ���� ���� ���� ����ϴ� ���.
    public void NextAtk()
    {
        if (!inputSmash)
        {
            if (comboStep == 2)
                playerAnim.Play("Knight_NormalAtk_B");
            if (comboStep == 3)
                playerAnim.Play("Knight_NormalAtk_C");
        }
        if (inputSmash)
        {
            if (comboStep == 1)
                playerAnim.Play("Knight_SmashAtk_A");
            if (comboStep == 2)
                playerAnim.Play("Knight_SmashAtk_B");
            if (comboStep == 3)
                playerAnim.Play("Knight_SmashAtk_C");
        }
    }

    //�޺� �ܰ� ó������ �ʱ�ȭ ��Ű�� ���.
    public void ResetCombo()
    {
        comboPossible = false;
        inputSmash = false;
        comboStep = 0;
    }

    //�⺻ ���� ���.
    void NormalAttack()
    {
        //���� ���ݽ� ù��° ���ݸ�� ��� ���.
        if (comboStep == 0)
        {
            playerAnim.Play("Knight_NormalAtk_A");
            comboStep = 1;
            return;
        }
        //�޺����� 1�� �����ϴ� ���.
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                //������ ���� ����.
                comboPossible = false;
                comboStep += 1;
            }
        }
    }

    void SmashAttack()
    {
        if (comboPossible)
        {
            comboPossible = false;
            inputSmash = true;
        }
    }

    void Update()
    {
        //���콺 ���� ��ư�� �븻 ����.
        if (Input.GetMouseButtonDown(0))
            NormalAttack();
        
        //���콺 ������ ��ư�� ���Ž� ����.
        if (Input.GetMouseButtonDown(1))
            SmashAttack();
    }
}
