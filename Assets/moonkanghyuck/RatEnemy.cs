using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatEnemy : Enemy
{
    private Animator anim;
    public Transform pos;
    public Vector2 boxsize;
    public GameObject dil, playercamera;
    Rigidbody2D Rigid;

    public double attacklt;//���ݰŸ�
    public double rangth; //�÷��̾�� ���� �Ÿ�����
    public int speed; //�̵��ӵ�
    public double rangthrun; //�޸��°Ÿ�
    public double rangthf; //Ž���Ÿ�
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
        anim = GetComponent<Animator>();
        Rigid = gameObject.GetComponent<Rigidbody2D>();
        base.Start();
    }

    void FixedUpdate()
    {
        if (HP <= 0)
        {
            anim.SetBool("die", true); // ü���� 0 �̸� �״� �ִϸ��̼� ���
        }
        else if (gogo) //gogo�� �������� ��
        {
            rangth = Mathf.Abs(Mathf.Abs(player.transform.position.x) - Mathf.Abs(Rigid.transform.position.x)); //�÷��̾� ������Ʈ�� x��ǥ���� ������ x��ǥ�� ���� ���밪���� ����
            if (rangth < rangthf) //�����Ÿ� �ȿ� �ÿ��̾ ���� ��
            {

                if (player.transform.position.x > Rigid.transform.position.x) //�÷��̾ �����ʿ� ���� ��
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.y); //���������� �ٶ�
                    attacking = 1;
                }
                else if (player.transform.position.x < Rigid.transform.position.x) //�÷��̾ ���ʿ� ���� ��
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.y); //�������� �ٶ�
                    attacking = -1;
                }
                if (rangth < attacklt) //���ݹ��� �ȿ� ����
                {
                    if (coltime)
                        Attack();

                    //Debug.Log("dd");
                }
                else if (rangth < rangthrun) //�޸��� �Ÿ��ȿ� ������ �޷��� ������ �޸�
                {
                    anim.SetBool("run", true); //�޸��� �ִϸ��̼� ����
                    Rigid.velocity = new Vector2(speed * attacking, Rigid.velocity.y); //attacking�� ����
                }
            }
            else //�����Ÿ� �ȿ� ���� �� �̵��� ���߰� ������ �ô� �ִϸ��̼��� ����
            {
                anim.SetBool("run", false); //�޸��� �ִϸ��̼��� ����

                //animator.SetBool("walk", true);
                //Rigid.velocity = new Vector2(speed*attacking*0.6f, Rigid.velocity.y);
            }
        }
        else
        {
            anim.SetBool("die", false);
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
        anim.SetBool("run", false); // �޸��� ����
        anim.SetTrigger("attack"); //���� �ִϸ��̼� ����
        gogo = false; //�̵�,�Ÿ����� ��� ����
        coltime = false;//��Ÿ��
        Invoke("Colcol", Random.Range(2.1f, 3.1f)); //��Ÿ�� ����
    }




    void Colcol() //��Ÿ���� ������
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

    //�ִϸ��̼ǿ��� ����ϴ� �Լ���
    public void Attacknow() //����
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxsize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player") //�����ؼ� �÷��̾ ����
            {
                if (!collider.GetComponent<PrototypeHero>().m_dodging)
                {
                    collider.GetComponent<PrototypeHero>().damagedani();

                    //������ ���
                    GameObject hello = Instantiate(dil);
                    hello.transform.position = (collider.transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0));
                    hello.GetComponent<damage>().damagechk = damage;

                    //hello.transform.GetChild(0).GetComponent<ParticleSystem.startcolor

                    //����
                    playercamera.GetComponent<playercamera>().startshake(0.4f, 0.2f);
                }
            }
        }
    }
    public void Andattack()
    {
        Rigid.velocity = new Vector2(5 * attacking, Rigid.velocity.y);//�����ϸ鼭 �̵�   
    }

    public override void Hit(int damege)
    {
        base.Hit(damege);
        if (HP <= 0)
        {
            if (!anim.GetBool("die")) anim.SetTrigger("hit");
            anim.SetBool("die", true);
        }
        if (HP > 0) anim.SetTrigger("hit");
    }
    public void Andwate() //gogo�� ����
    {
        //Debug.Log("dd");
        gogo = true;
    }
    public void Destroyobject()
    { //�����鼭 ������Ʈ ����
        Destroy(gameObject);
    }
}
