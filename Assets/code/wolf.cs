using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class wolf : MonoBehaviour
{
    private Animator animator;
    public GameObject PlayerObj;
    public double attacklt;
    public double rangth; //����� �÷��̾��� �Ÿ� üũ ����
    public int speed; //������ �ӵ�
    public double rangthf; //�����Ÿ�
    bool coltime = true;
    public bool gogo;
    int attacking;
    Rigidbody2D Rigid;
    public float curtime;
    public float cooltime;
    public Transform pos;
    public Vector2 boxsize;
    public int HP;
    public int MAXHP;
    public HPbar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        PlayerObj=GameObject.Find("player1");
        HP=MAXHP;
        healthbar.sethealth(HP,MAXHP);
        gogo = true;
        animator = GetComponent<Animator>();
        Rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HP<=0){
            animator.SetBool("die",true);
        }
        else{
            if (gogo)
            {
                rangth = Mathf.Abs(Mathf.Abs(PlayerObj.transform.position.x) - Mathf.Abs(Rigid.transform.position.x)); //����� �÷��̾��� �Ÿ�
                if (rangth < rangthf)
                {

                    if (PlayerObj.transform.position.x > Rigid.transform.position.x)
                    {
                        transform.localScale = new Vector3(-Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.y);
                        attacking = 1;
                    }
                    else if (PlayerObj.transform.position.x < Rigid.transform.position.x)
                    {
                        transform.localScale = new Vector3(Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.y);
                        attacking = -1;
                    }
                    if (rangth < attacklt)
                    {
                        if (coltime)
                            attack();

                        //Debug.Log("dd");
                    }

                    else
                    {
                        animator.SetBool("run", true);
                        Rigid.velocity = new Vector2(speed * attacking, Rigid.velocity.y);
                    }
                }
                else
                {
                    animator.SetBool("run", false);
                    //animator.SetBool("walk", true);
                    //Rigid.velocity = new Vector2(speed*attacking*0.6f, Rigid.velocity.y);
                }
            }
        }

    }
    void attack()
    {/*
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxsize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "player"){
                //collider.GetComponent<PrototypeHero>().
            }
        }*/
        animator.SetBool("run", false);
        animator.SetTrigger("attack");
        gogo = false;
        coltime = false;

        Invoke("colcol", 1);
    }
    public void andattack()
    {
        Rigid.velocity = new Vector2(5*attacking, Rigid.velocity.y);
    }
    public void andwate()
    {
        //Debug.Log("dd");
        gogo = true;
    }
    public void hit(int damege)
    {
        
        if (HP>0){
            HP-=damege;
            healthbar.sethealth(HP,MAXHP);
            if (PlayerObj.transform.position.x > Rigid.transform.position.x)
            {
                attacking = 1;
            }
            else if (PlayerObj.transform.position.x < Rigid.transform.position.x)
            {
                attacking = -1;
            }
            Rigid.velocity = new Vector2(-3*attacking, Rigid.velocity.y);
            gogo=false;
            animator.SetTrigger("hit");
        }
    }

    void colcol()
    {
        coltime = true;
    }
    public void randomway()
    {

    }
    public void destroyobject(){
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxsize);
    }
}
