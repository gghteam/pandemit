using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class wolf : Enemy
{
    private Animator animator;
    public Transform pos;
    public Vector2 boxsize;
    public GameObject dil, playercamera;
    Rigidbody2D Rigid;

    public double attacklt;//공격거리
    public double rangth; //플레이어와 늑대의 거리차이
    public int speed; //이동속도
    public double rangthrun; //달리는거리
    public double rangthf; //탐지거리
    bool coltime = true;
    int attacking;
    public float curtime;
    public float cooltime;
    [SerializeField]
    private int damage = 5;


    protected override void Start()
    {
        playercamera = FindObjectOfType<playercamera>().gameObject;
        gogo = true;
        animator = GetComponent<Animator>();
        Rigid = gameObject.GetComponent<Rigidbody2D>();
        base.Start();
    }

    void FixedUpdate()
    {
        if (HP<=0){
            animator.SetBool("die",true); // 체력이 0 이면 죽는 애니메이션 출력
        }
        else if (gogo) //gogo가 켜져있을 때
            {
                rangth = Mathf.Abs(Mathf.Abs(player.transform.position.x) - Mathf.Abs(Rigid.transform.position.x)); //플레이어 오브젝트의 x좌표에서 늑대의 x좌표를 빼서 절대값으로 만듬
                if (rangth < rangthf) //사정거리 안에 플에이어가 들어올 때
                {

                    if (player.transform.position.x > Rigid.transform.position.x) //플레이어가 오른쪽에 있을 때
                    {
                        transform.localScale = new Vector3(-Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.y); //오른쪽으로 바라봄
                        attacking = 1;
                    }
                    else if (player.transform.position.x < Rigid.transform.position.x) //플레이어가 왼쪽에 있을 때
                    {
                        transform.localScale = new Vector3(Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.y); //왼쪽으로 바라봄
                        attacking = -1;
                    }
                    if (rangth < attacklt) //공격범위 안에 들어옴
                    {
                        if (coltime)
                            Attack();

                        //Debug.Log("dd");
                    }
                    else if(rangth < rangthrun) //달리는 거리안에 들어오면 달려서 가까우면 달림
                    {
                        animator.SetBool("run", true); //달리는 애니메이션 나옴
                        Rigid.velocity = new Vector2(speed * attacking, Rigid.velocity.y); //attacking은 방향
                    }
                    else //달리는 거리안에 없지만 사정거리 안에 있으면 천천히 걸어서 접근함
                    {
                        animator.SetBool("walk", true); //걷는 애니메이션 나옴
                        animator.SetBool("run", false);
                    Rigid.velocity = new Vector2(speed / 2 * attacking, Rigid.velocity.y); //attacking은 방향
                }
                }
                else //사정거리 안에 없을 때 이동을 멈추고 냄새를 맡는 애니메이션이 나옴
                {
                    animator.SetBool("run", false); //달리기 애니매이션이 꺼짐
                    animator.SetBool("walk", false); //걷기 애니매이션이 꺼짐
                    
                    //animator.SetBool("walk", true);
                    //Rigid.velocity = new Vector2(speed*attacking*0.6f, Rigid.velocity.y);
                }
            }
        else
        {
            animator.SetBool("die", false);
        }
    }
    void Attack()
    {/*
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxsize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "player"){
                //collider.GetComponent<PrototypeHero>().
            }
        }*/
        animator.SetBool("run", false); // 달리기 꺼짐
        animator.SetBool("walk", false); // 달리기 꺼짐
        animator.SetTrigger("attack"); //공격 애니메이션 켜짐
        gogo = false; //이동,거리측정 모두 멈춤
        coltime = false;//쿨타임
        Invoke("Colcol", Random.Range(2.1f,3.1f)); //쿨타임 충전
    }




    void Colcol() //쿨타임이 충전됨
    {
        coltime = true;
    }
    public void Randomway()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxsize);
    }

    //애니메이션에서 사용하는 함수들
    public void Attacknow() //공격
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxsize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player") //공격해서 플레이어를 맞춤
            {
                if(!collider.GetComponent<PrototypeHero>().m_dodging){
                    collider.GetComponent<PrototypeHero>().damagedani();

                    //데미지 출력
                    GameObject hello = Instantiate(dil);
                    hello.transform.position = (collider.transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0));
                    hello.GetComponent<damage>().damagechk = damage;

                    //hello.transform.GetChild(0).GetComponent<ParticleSystem.startcolor

                    //진동
                    playercamera.GetComponent<playercamera>().startshake(0.4f, 0.2f);
                }
            }
        }
    }
    public void Andattack()
    {
        Rigid.velocity = new Vector2(5 * attacking, Rigid.velocity.y);//공격하면서 이동   
    }

    public override void Hit(int damege)
    {
        base.Hit(damege);
        if(HP > 0) animator.SetTrigger("hit");
    }
    public void Andwate() //gogo가 켜짐
    {
        //Debug.Log("dd");
        gogo = true;
    }
    public void Destroyobject()
    { //죽으면서 오브젝트 삭제
        Destroy(gameObject);
    }
}
