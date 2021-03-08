using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolf : MonoBehaviour
{
    private Animator animator;
    public GameObject PlayerObj;
    public GameObject attackcol;
    public double attacklt;
    public double rangth; //����� �÷��̾��� �Ÿ� üũ ����
    public int speed; //������ �ӵ�
    public double rangthf; //�����Ÿ�
    bool coltime=true;
    int attacking;
    Rigidbody2D Rigid;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Rigid = gameObject.GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        
        rangth = Mathf.Abs(PlayerObj.transform.position.x) - Mathf.Abs(Rigid.transform.position.x); //����� �÷��̾��� �Ÿ�
        if (rangth < rangthf && rangth > -rangthf)
        {
            
            if (PlayerObj.transform.position.x > Rigid.transform.position.x)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.lossyScale.x),transform.lossyScale.y,transform.lossyScale.y);
                attacking = 1;
            }
            else if (PlayerObj.transform.position.x < Rigid.transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.lossyScale.x),transform.lossyScale.y,transform.lossyScale.y);
                attacking = -1;
            }
            if (rangth < attacklt && rangth > -attacklt){
                if (coltime)
                attack();
                //Debug.Log("dd");
            }

            else Rigid.velocity = new Vector2(speed*attacking, Rigid.velocity.y);
            
        }
    
    }
    void attack()
    {
        attackcol.SetActive(true);
        coltime = false;
        Invoke("colcol", 1);
    }
    public void andattack(){

    }
    
    void colcol(){
        coltime = true;
    }
}
