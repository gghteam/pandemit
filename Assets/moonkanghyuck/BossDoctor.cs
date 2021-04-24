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
                onMove = true;
                break;
            case 1: // 찌르기
                onMove = true;
                Attack();
                break;
            case 2: // 던지기
                onMove = true;
                Throw();
                break;
            case 3: // 달리기
                onMove = true;
                Run();
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
        //찌르기
    }
    private void Throw()
    {
        //던지기
    }

    private void Run()
    {
        //달리기
    }

    public void OffMove() // 애니메이션 끝에 넣을 함수. 거리재기와 다른 동작들을 실행할 수 있게 함
    {
        onMove = false;
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
