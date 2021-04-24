using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoctor : MonoBehaviour
{
    Animator anim; // �ִϸ�����
    [SerializeField]
    private GameObject player; // �÷��̾�

    [SerializeField]
    private int rangestat; // �Ÿ� ���� ����
    [SerializeField]
    private float rangth; // �Ÿ�
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
            case 0: // ���
                onMove = true;
                break;
            case 1: // ���
                onMove = true;
                Attack();
                break;
            case 2: // ������
                onMove = true;
                Throw();
                break;
            case 3: // �޸���
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
            rangestat = 1; // ���
        }
        else if (rangth < 1.5)
        {
            rangestat = 2; // ������
        }
        else if (rangth < 2)
        {
            rangestat = 3; // �޸���
        }
        else
        {
            rangestat = 0; // ���
        }
    }    

    private void Attack()
    {
        //���
    }
    private void Throw()
    {
        //������
    }

    private void Run()
    {
        //�޸���
    }

    public void OffMove() // �ִϸ��̼� ���� ���� �Լ�. �Ÿ����� �ٸ� ���۵��� ������ �� �ְ� ��
    {
        onMove = false;
    }

    private void PaseTwo()
    {
        //������ ����
    }

    private void Death()
    {
        //���� �Լ�
    }
}
