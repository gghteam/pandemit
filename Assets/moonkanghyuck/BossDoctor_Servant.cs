using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoctor_Servant : Enemy
{
    Rigidbody2D rigid;
    Animator anim;
    [SerializeField]
    private int rangestat; // 거리 상태 변수
    [SerializeField]
    private float rangth; // 거리


    [SerializeField]
    private Transform[] pos;

    [SerializeField]
    private Vector2[] boxsize;

    [SerializeField]
    private GameObject dil;

    [SerializeField]
    private bool onmove;
    [SerializeField]
    private int damage = 5;

    [SerializeField]
    private bool die;

    [SerializeField]
    private float cooltime;


    private playercamera playercamera;
    Enemy enemy;

    private void FixedUpdate()
    {
        if (HP < 0)
        {
            anim.SetBool("die?", true);
            die = true;
        }
        cooltime += Time.deltaTime;
    }

    protected override void Awake()
    {
        enemy = GetComponent<Enemy>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PrototypeHero>().gameObject;
        playercamera = FindObjectOfType<playercamera>();
        anim.SetTrigger("start?");
        StartCoroutine(AttackSelect());
        base.Awake();
    }

    private void Range()
    {
        if (player.transform.position.x > transform.position.x) //플레이어가 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.y); //오른쪽으로 바라봄
        }
        else if (player.transform.position.x < transform.position.x) //플레이어가 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.y); //왼쪽으로 바라봄
        }
        rangth = Mathf.Abs(transform.position.x - player.transform.position.x);
        if (rangth < 1.4)
        {
            rangestat = Random.Range(0, 3);
            if(rangestat >= 1)
            {
                rangestat = 1;
            }
            else
            {
                rangestat = 2;
            }

        }
        else 
        {
            rangestat = 3; // 돌진
        }
    }

    IEnumerator AttackSelect()
    {
        while(true)
        {
            if(die)
            {
                anim.SetTrigger("hit?");
                break;
            }
            if(!onmove)
            {
                if(cooltime > 1)
                {
                    cooltime = 0;
                    Range();
                    anim.SetInteger("Range?", rangestat);
                    onmove = true;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
    private void Attack(int attacknum)
    {
        //찌르기
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos[attacknum].position, boxsize[attacknum], 0);
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
                }
            }
        }
    }
    private void Rush()
    {
        rigid.velocity = new Vector2(7 * -transform.lossyScale.x, rigid.velocity.y);
    }
    public override void Hit(int damege)
    {
        base.Hit(damege);
        if (HP <= 0)
        {
            if (!anim.GetBool("die?")) anim.SetTrigger("hit?");
            anim.SetBool("die?", true);
        }
        if (HP > 0) anim.SetTrigger("hit?");
    }
    public void OffMove()
    {
        anim.SetInteger("Range?", 0);
        onmove = false;
    }
    public void Destroyobject()
    { //죽으면서 오브젝트 삭제
        transform.SetParent(poolManager.transform);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for(int i = 0; i<pos.Length;i++)
        {
            Gizmos.DrawWireCube(pos[i].position, boxsize[i]);
        }
    }
}
