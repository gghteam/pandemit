using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoctor : Enemy
{
    Animator anim; // 애니메이터

    [SerializeField]
    private int rangestat; // 거리 상태 변수 0.대기 1.근접공격 2.약던지기 3.달리기 4.면역
    [SerializeField]
    private float rangth; // 거리
    [SerializeField]
    private bool onMove;
    //면역 쿨타임
    private float timer = 0f; // 면역 타이머
    private float coltime = 5f; // 면역 쿨타임
    //면역효과
    private float effecttimer = 0f; // 면역 적용 타이머
    //히트박스
    [SerializeField]
    private Transform pos;
    [SerializeField]
    private Vector2 boxsize;
    //약물던지기
    [SerializeField]
    private GameObject potionprefeb;
    //페이즈 변수
    [SerializeField]
    private int pase = 1;
    //소환수
    [SerializeField]
    private GameObject[] sevant = new GameObject[3];
    [SerializeField]
    private bool[] sevanton = new bool[2];
    [SerializeField]
    private GameObject sevantprefeb;

    [SerializeField]
    private GameObject dil;
    
    private playercamera playercamera;

    [SerializeField]
    private int damage;

    Enemy enemy;
    Rigidbody2D rigid;

    protected override void Start()
    {
        enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        playercamera = FindObjectOfType<playercamera>();
        rigid = GetComponent<Rigidbody2D>();
        base.Start();
    }

    private void FixedUpdate()
    {
        
        if (HP > 0)
        {
            if (HP / MAXHP < 0.5f) // HP를 MAXHP로 나눠서 백분율 만듬
            {
                pase = 3;
            }
            else if (HP / MAXHP < 0.7f) // HP를 MAXHP로 나눠서 백분율 만듬
            {
                pase = 2;
            }
            if (pase > 1) timer += Time.deltaTime;
            if (onMove) return;
            Range();
            if (rangestat != 0) onMove = true;
        }
        else if(HP <= 0)
        {
            anim.SetBool("die?", true);
        }
       
    }

    private void Range()
    {
        if(!onMove)
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
            switch(pase)
            {
                case 1: // 페이즈1
                    Pattern();
                    break;
                case 2: // 페이즈2
                    if (!sevanton[0])
                    {
                        StartCoroutine(Sumon(0,1));
                        sevanton[0] = true;
                    }

                    Pattern();
                    if (timer > coltime)
                    {
                        timer = 0;
                        rangestat = 4;
                    }
                    break;
                case 3:
                    if (!sevanton[1])
                    {
                        StartCoroutine(Sumon(1, 3));
                        sevanton[1] = true;
                    }
                    Pattern();
                    if (timer > coltime)
                    {
                        timer = 0;
                        rangestat = 4;
                    }
                    break;
            }
            
            anim.SetInteger("Range?", rangestat);
        }
    }    

    IEnumerator Sumon(int num, int range)
    {
        for(int i = num; i < range; i++)
        {
            sevant[i] = Instantiate(sevantprefeb, transform.localPosition, Quaternion.identity);
            sevant[i].transform.SetParent(null);
            yield return new WaitForSeconds(0.2f);
        }
    }
    private void Pattern()
    {
        if (rangth < 2.5)
        {
            rangestat = 1; // 찌르기

        }
        else if (rangth < 4)
        {
            rangestat = 2; // 던지기
        }
        else if (rangth < 5)
        {
            rangestat = 0; //각혈주사기
        }
        else
        {
            rangestat = 0; // 대기
        }
    }

    private void Attack()
    {
        //찌르기
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
                }
            }
        }
    }
    private void Throw()
    {
        //던지기
        GameObject potion = null;
        potion = Instantiate(potionprefeb, transform.position, Quaternion.identity);
        int randomangle = Random.Range(0,2);
        int xnuck = 0, ynuck = 0;
        switch(randomangle)
        {
            case 0:
                ynuck = 200;
                xnuck = 200;
                break;
            case 1:
                ynuck = 200;
                xnuck = 50;
                break;

        }
        potion.GetComponent<Rigidbody2D>().AddForce(Vector2.up * ynuck);
        potion.GetComponent<Rigidbody2D>().AddForce(Vector2.right * (this.gameObject.transform.lossyScale.x == 1 ? -xnuck : xnuck));
        potion.transform.localScale = transform.lossyScale;
        if(transform.lossyScale.x == -1) potion.GetComponent<Animator>().SetBool("Right?", true);
    }

    private void Run()
    {
        //달리기
        rigid.velocity = new Vector2(3 * -transform.lossyScale.x, rigid.velocity.y);
    }

    public void OffMove() // 애니메이션 끝에 넣을 함수. 거리재기와 다른 동작들을 실행할 수 있게 함
    {
        onMove = false;
    }

    public override void Hit(int damege)
    {

        base.Hit(damege);
        if (HP <= 0)
        {
            if(!anim.GetBool("die?")) anim.SetTrigger("hit?");
            anim.SetBool("die?", true);
        }
        if (HP > 0) anim.SetTrigger("hit?");
        
    }

    private void PaseTwo()
    {
        //페이즈 변경
    }

    private void Death()
    {
        //죽음 함수
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxsize);
    }
    public void Destroyobject()
    { //죽으면서 오브젝트 삭제
        Destroy(gameObject);
    }
}
