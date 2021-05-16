using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : Enemy
{
    Vector2 startPosition;
    [SerializeField]
    private bool attackon; // 공격모드
    [SerializeField]
    private float range = 3f;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private Transform pos;
    [SerializeField]
    private Vector2 boxsize;
    [SerializeField]
    private GameObject dil;
    private playercamera playercamera;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float cooltime = 1f;

    protected override void Start()
    {
        startPosition = transform.position;
        playercamera = FindObjectOfType<playercamera>();
        base.Start();
    }

    private void FixedUpdate()
    {
        cooltime -= Time.deltaTime;
        float rangth;
        rangth = Vector2.Distance(transform.position, player.transform.position);
        if(transform.position.y == startPosition.y && attackon == false)
        {
            cooltime = 1f;
            attackon = true;
        }
        else if(attackon == false || rangth > range)
        {
            transform.position = Vector2.MoveTowards(transform.position,startPosition,speed);
        }
        if (rangth <= range && attackon && cooltime <= 0)
        {
            if (player.transform.position.x > transform.position.x) //플레이어가 오른쪽에 있을 때
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.y); //오른쪽으로 바라봄
            }
            else if (player.transform.position.x < transform.position.x) //플레이어가 왼쪽에 있을 때
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.y); //왼쪽으로 바라봄
            }
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);
            Attack();
        }

    }
    private void Attack()
    {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxsize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player") //공격해서 플레이어를 맞춤
            {
                if (!collider.GetComponent<PrototypeHero>().m_dodging)
                {
                    collider.GetComponent<PrototypeHero>().damagedani();

                    //데미지 출력
                    GameObject hello = Instantiate(dil);
                    hello.transform.position = (collider.transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0));
                    hello.GetComponent<damage>().damagechk = damage;
                    playercamera.GetComponent<playercamera>().startshake(0.4f, 0.2f);
                    attackon = false;
                }
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxsize);
    }
}
