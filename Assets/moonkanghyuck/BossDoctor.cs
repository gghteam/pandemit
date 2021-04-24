using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoctor : MonoBehaviour
{
    Animator anim; // 애니메이터
    [SerializeField]
    private GameObject player; // 플레이어

    [SerializeField]
    private int rangestat; // 거리 상태 변수
    [SerializeField]
    private float rangth; // 거리
    [SerializeField]
    private bool onMove;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PrototypeHero>().gameObject;
    }

    private void FixedUpdate()
    {
        if (onMove) return;
        Range();
        switch (rangestat)
        {
            case 0: // 대기
                break;
            case 1: // 찌르기
                break;
            case 2: // 던지기
                break;
            case 3: // 달리기
                break;
        }
    }

    private void Range() 
    {
        rangth = Mathf.Abs(transform.position.x - player.transform.position.x);
        if(rangth < 1)
        {
            rangestat = 1; // 찌르기
        }
        else if (rangth < 1.5)
        {
            rangestat = 2; // 던지기
        }
        else if (rangth < 2)
        {
            rangestat = 3; // 달리기
        }
        else
        {
            rangestat = 0; // 대기
        }
    }    

    private void Attack()
    {
        //찌르기 또는 던지기 거리 변수에 따라 작동
    }    

    private void Run()
    {
        //달리기
    }

    private void PaseTwo()
    {
        //페이즈 변경
    }

    private void Death()
    {
        //죽음 함수
    }
}
