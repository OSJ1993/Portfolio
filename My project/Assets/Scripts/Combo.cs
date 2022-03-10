using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    Animator playerAnim;


    //Combo ���� ���� Ȯ��. /by 2022.03.10  ������
    bool comboPossible;

    //Combo ���� �ܰ� Ȯ��. /by 2022.03.10  ������
    public int comboStep;

    //Smash Ű�� �Է� ���� Ȯ��. /by 2022.03.10  ������
    bool inputSmash;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    //Combo ������ Combo�Է��� �����ϰ� ���ִ� ���.. /by 2022.03.10  ������
    public void ComboPossible()
    {
        comboPossible = true;
    }

    //�Էµ� Ű�� Combo �ܰ迡 ���� ���� ������ ����ϴ� ���. /by 2022.03.10  ������
    public void NextAtk()
    {
        if (!inputSmash)
        {
            if (comboStep == 2)
                playerAnim.Play("NomalAttackB");
            if (comboStep == 3)
                playerAnim.Play("NomalAttackC");
        }
        if (inputSmash)
        {
            if (comboStep == 1)
                playerAnim.Play("SmashAttackA");
            if(comboStep==2)
                playerAnim.Play("SmashAttackB");
            if(comboStep==3)
                playerAnim.Play("SmashAttackC");

        }

    }

    //Combo �ܰ踦 �ٽ� ó������ �ʱ�ȭ ��Ű�� ���. /by 2022.03.10  ������
    public void ResetCombo()
    {
        comboPossible = false;
        inputSmash = false;
        comboStep = 0;
    }

    //�⺻ ���ݱ��. /by 2022.03.10  ������
    void NormalAttack()
    {
        //���� �Է� �� ù�� ° ���� ��� ��� /by 2022.03.10  ������
        if (comboStep == 0)
        {
            playerAnim.Play("NomalAttackA");
            comboStep = 1;
            return;
        }

        // �� ���ķ� Combo ���� 1�� �߰��ϴ� ���. /by 2022.03.10  ������
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                //������ �Է� ����. /by 2022.03.10  ������
                comboPossible = false;
                comboStep += 1;
            }
        }
    }

    //���Ž� ���. /by 2022.03.10  ������
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
        //���콺 ���� ��ư ��� ����. /by 2022.03.10  ������
        if (Input.GetMouseButtonDown(0))
            NormalAttack();

        ///���콺 ������ ��ư ���Ž� ����.by 2022.03.10  ������
        if (Input.GetMouseButtonDown(1))
            SmashAttack();
    }
}
