using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    Animator playerAnim;


    //Combo 가능 여부 확인. /by 2022.03.10  오승주
    bool comboPossible;

    //Combo 진행 단계 확인. /by 2022.03.10  오승주
    public int comboStep;

    //Smash 키의 입력 여부 확인. /by 2022.03.10  오승주
    bool inputSmash;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    //Combo 시작점 Combo입력이 가능하게 해주는 기능.. /by 2022.03.10  오승주
    public void ComboPossible()
    {
        comboPossible = true;
    }

    //입력된 키와 Combo 단계에 따라 다음 동작을 재생하는 기능. /by 2022.03.10  오승주
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

    //Combo 단계를 다시 처음으로 초기화 시키는 기능. /by 2022.03.10  오승주
    public void ResetCombo()
    {
        comboPossible = false;
        inputSmash = false;
        comboStep = 0;
    }

    //기본 공격기능. /by 2022.03.10  오승주
    void NormalAttack()
    {
        //최초 입력 시 첫번 째 공격 모션 재생 /by 2022.03.10  오승주
        if (comboStep == 0)
        {
            playerAnim.Play("NomalAttackA");
            comboStep = 1;
            return;
        }

        // 그 이후로 Combo 스텝 1씩 추가하는 기능. /by 2022.03.10  오승주
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                //무차별 입력 방지. /by 2022.03.10  오승주
                comboPossible = false;
                comboStep += 1;
            }
        }
    }

    //스매쉬 기능. /by 2022.03.10  오승주
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
        //마우스 왼쪽 버튼 노멀 어택. /by 2022.03.10  오승주
        if (Input.GetMouseButtonDown(0))
            NormalAttack();

        ///마우스 오른쪽 버튼 스매쉬 어택.by 2022.03.10  오승주
        if (Input.GetMouseButtonDown(1))
            SmashAttack();
    }
}
