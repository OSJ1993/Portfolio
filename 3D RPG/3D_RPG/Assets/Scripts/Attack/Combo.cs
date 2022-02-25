using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    Animator playerAnim;

    //콤보 입력 가능 체크.
    bool comboPossible;

    //콤보 진행 단계.
    public int comboStep;

    //스매쉬 입력 가능 체크
    bool inputSmash;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    //콤보의 시작점. 콤보가 가능하게 해준다.
    public void ComboPossible()
    {
        comboPossible = true;
    }

    //입력된 키와 콤보 단계에 따라서 다음 동작 재생하는 기능.
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

    //콤보 단계 처음으로 초기화 시키는 기능.
    public void ResetCombo()
    {
        comboPossible = false;
        inputSmash = false;
        comboStep = 0;
    }

    //기본 공격 기능.
    void NormalAttack()
    {
        //최초 공격시 첫번째 공격모션 사용 기능.
        if (comboStep == 0)
        {
            playerAnim.Play("Knight_NormalAtk_A");
            comboStep = 1;
            return;
        }
        //콤보스텍 1씩 증가하는 기능.
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                //무차별 공격 방지.
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
        //마우스 왼쪽 버튼은 노말 어택.
        if (Input.GetMouseButtonDown(0))
            NormalAttack();
        
        //마우스 오른쪽 버튼은 스매쉬 어택.
        if (Input.GetMouseButtonDown(1))
            SmashAttack();
    }
}
