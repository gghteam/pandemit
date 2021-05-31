using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

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
    [SerializeField]
    private GameObject reversepotionprefeb;
    //�����ֻ��
    [SerializeField]
    private GameObject syringerprefeb;
    [SerializeField]
    private GameObject boundingprefeb;
    [SerializeField]
    private float syringertimer = 0.5f;
    //������ ����
    [SerializeField]
    private int pase = 1;
    //��ȯ��
    [SerializeField]
    private bool[] sevanton = new bool[2];
    [SerializeField]
    private GameObject sevantprefeb;

    [SerializeField]
    private GameObject dil;
    
    private playercamera playercamera;
    [SerializeField]
    private Light2D bosslight;

    [SerializeField]
    private int damage;

    Rigidbody2D rigid;


    protected override void Awake()
    {
        anim = GetComponent<Animator>();
        playercamera = FindObjectOfType<playercamera>();
        rigid = GetComponent<Rigidbody2D>();
        base.Awake();
    }
    private void OnEnable()
    {
        HP = MAXHP;
        anim.SetBool("die?", false);
    }

    private void FixedUpdate()
    {
        
        if (HP > 0)
        {
            if (HP / MAXHP < 0.5f) // HP�� MAXHP�� ������ ����� ����
            {
                bosslight.intensity = 2f;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
                pase = 3;
            }
            else if (HP / MAXHP < 0.7f) // HP�� MAXHP�� ������ ����� ����
            {
                bosslight.intensity = 1f;
                pase = 2;
            }
            if (pase > 1) timer += Time.deltaTime;
            if (onMove) return;
            Range();
            syringertimer -= Time.deltaTime;
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
                        GetPoolServant();
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
                        GetPoolServant();
                        GetPoolServant();
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
            if (syringertimer <= 0)
            {
                GetSyringer();
                syringertimer = 0.5f;
            }
            else
            {
                rangestat = 3; // �޸���
            }
            
        }
        else if (rangth < 6)
        {
            if (pase > 2)
            {
                int random = Random.Range(0, 2);
                switch (random)
                {
                    case 0:
                        rangestat = 5;
                        break;
                    case 1:
                        GetBounding();
                        break;
                }
            }
            else
            {
                rangestat = 3; // �޸���
            }

        }
        else
        {
            rangestat = 0; // ���
        }
    }
    private void GetSyringer()
    {
        
        float syringertimer = 0;
        for (int i = 0; i < 3; i++)
        {
            var syringerson = poolManager.transform.GetComponentInChildren<BossDoctor_syringe>(true);
            if (syringerson == null)
            {
                Instantiate(syringerprefeb, new Vector2(transform.position.x + (transform.localScale.x * i), transform.position.y + 1 + ((i * 3) / 3)), Quaternion.identity);
            }
            else
            {
                syringerson.transform.position = new Vector2(transform.position.x + (transform.localScale.x * i), transform.position.y + 1 + ((i * 3) / 3));
                syringerson.transform.SetParent(null);
                syringerson.gameObject.SetActive(true);
            }
            while(syringertimer < 0.2f)
            {
                syringertimer += Time.deltaTime;
            }
            syringertimer = 0;
        }
    }
    private void GetBounding()
    {
        var syringerson = poolManager.transform.GetComponentInChildren<BossDoctor_BoundingSyringer>(true);
        if (syringerson == null)
        {
            Instantiate(boundingprefeb, new Vector2(transform.position.x + (transform.localScale.x * 1), transform.position.y + 2), Quaternion.identity);
        }
        else
        {
            syringerson.transform.position = new Vector2(transform.position.x + (transform.localScale.x * 1), transform.position.y + 2);
            syringerson.transform.SetParent(null);
            syringerson.gameObject.SetActive(true);
        }
    }
    private void GetPoolServant()
    {
        BossDoctor_Servant servantson_script = poolManager.transform.GetComponentInChildren<BossDoctor_Servant>();
        if (servantson_script == null)
        {
            Instantiate(sevantprefeb, transform.localPosition, Quaternion.identity);
        }
        else
        {
            GameObject servantson = servantson_script.gameObject;
            servantson.transform.position = transform.position;
            servantson.transform.SetParent(null);
            servantson.SetActive(true);
        }
    }
    private GameObject GetPostion()
    {
        GameObject potion;
        if(poolManager.GetComponentInChildren<BossDoctor_NormalPotion>(true) == null)
        {
            potion = Instantiate(potionprefeb, transform.position, Quaternion.identity);
        }
        else
        {
            potion = poolManager.GetComponentInChildren<BossDoctor_NormalPotion>(true).gameObject;
            potion.transform.SetParent(null);
            potion.transform.position = transform.position;
            potion.SetActive(true);
        }
        return potion;
    }
    private GameObject GetReversePostion()
    {
        GameObject potion;
        if (poolManager.GetComponentInChildren<BossDoctor_ReversePotion>(true) == null)
        {
            potion = Instantiate(reversepotionprefeb, transform.position, Quaternion.identity);
        }
        else
        {
            potion = poolManager.GetComponentInChildren<BossDoctor_ReversePotion>(true).gameObject;
            potion.transform.SetParent(null);
            potion.transform.position = transform.position;
            potion.SetActive(true);
        }
        return potion;
    }
    //�ִϸ��̼ǿ� �ִ� �Լ���
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
        potion = GetPostion();
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
    private void ThrowReverse()
    {
        //������
        GameObject potion = null;
        potion = GetReversePostion();
        int xnuck = 50, ynuck = 400;
        potion.GetComponent<Rigidbody2D>().AddForce(Vector2.up * ynuck);
        potion.GetComponent<Rigidbody2D>().AddForce(Vector2.right * (this.gameObject.transform.lossyScale.x == 1 ? -xnuck : xnuck));
        potion.transform.localScale = transform.lossyScale;
        if (transform.lossyScale.x == -1) potion.GetComponent<Animator>().SetBool("Right?", true);
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
        gameObject.transform.SetParent(poolManager.transform);
        gameObject.SetActive(false);
    }
}
