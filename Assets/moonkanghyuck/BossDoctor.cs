using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoctor : Enemy
{
    Animator anim; // �ִϸ�����

    [SerializeField]
    private int rangestat; // �Ÿ� ���� ���� 0.��� 1.�������� 2.������� 3.�޸��� 4.�鿪
    [SerializeField]
    private float rangth; // �Ÿ�
    [SerializeField]
    private bool onMove;
    //�鿪 ��Ÿ��
    private float timer = 0f; // �鿪 Ÿ�̸�
    private float coltime = 5f; // �鿪 ��Ÿ��
    //�鿪ȿ��
    private float effecttimer = 0f; // �鿪 ���� Ÿ�̸�
    //��Ʈ�ڽ�
    [SerializeField]
    private Transform pos;
    [SerializeField]
    private Vector2 boxsize;
    //�๰������
    [SerializeField]
    private GameObject potionprefeb;
    //������ ����
    [SerializeField]
    private int pase = 1;
    //��ȯ��
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
            if (HP / MAXHP < 0.5f) // HP�� MAXHP�� ������ ����� ����
            {
                pase = 3;
            }
            else if (HP / MAXHP < 0.7f) // HP�� MAXHP�� ������ ����� ����
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
            if (player.transform.position.x > transform.position.x) //�÷��̾ �����ʿ� ���� ��
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.y); //���������� �ٶ�
            }
            else if (player.transform.position.x < transform.position.x) //�÷��̾ ���ʿ� ���� ��
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.y); //�������� �ٶ�
            }
            rangth = Mathf.Abs(transform.position.x - player.transform.position.x);
            switch(pase)
            {
                case 1: // ������1
                    Pattern();
                    break;
                case 2: // ������2
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
            rangestat = 1; // ���

        }
        else if (rangth < 4)
        {
            rangestat = 2; // ������
        }
        else if (rangth < 5)
        {
            rangestat = 0; //�����ֻ��
        }
        else
        {
            rangestat = 0; // ���
        }
    }

    private void Attack()
    {
        //���
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
                    playercamera.GetComponent<playercamera>().startshake(0.4f, 0.2f);
                }
            }
        }
    }
    private void Throw()
    {
        //������
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
        //�޸���
        rigid.velocity = new Vector2(3 * -transform.lossyScale.x, rigid.velocity.y);
    }

    public void OffMove() // �ִϸ��̼� ���� ���� �Լ�. �Ÿ����� �ٸ� ���۵��� ������ �� �ְ� ��
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
        //������ ����
    }

    private void Death()
    {
        //���� �Լ�
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxsize);
    }
    public void Destroyobject()
    { //�����鼭 ������Ʈ ����
        Destroy(gameObject);
    }
}
