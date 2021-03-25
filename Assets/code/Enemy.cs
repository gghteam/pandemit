using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    public GameObject PlayerObj;
    public HPbar healthbar;

    public bool gogo;
    int attacking;
    public int HP;
    public int MAXHP;
    public int damage;

    Rigidbody2D Rigid;
    // Start is called before the first frame update
    void Start()
    {
        PlayerObj = GameObject.Find("player1");
        HP = MAXHP;
        healthbar.sethealth(HP, MAXHP);
        gogo = true;
        animator = GetComponent<Animator>();
        Rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hit(int damege) //데미지를 받음
    {

        if (HP > 0)
        {
            HP -= damege;
            healthbar.sethealth(HP, MAXHP);
            if (PlayerObj.transform.position.x > Rigid.transform.position.x)
            {
                attacking = 1;
            }
            else if (PlayerObj.transform.position.x < Rigid.transform.position.x)
            {
                attacking = -1;
            }
            Rigid.velocity = new Vector2(-3 * attacking, Rigid.velocity.y);
            gogo = false;
            animator.SetTrigger("hit");
        }
    }
}
