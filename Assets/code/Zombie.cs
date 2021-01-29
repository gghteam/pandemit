using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Player PlayerObj;
    public double rangth; //����� �÷��̾��� �Ÿ� üũ ����
    public int speed; //������ �ӵ�
    public double rangthf; //�����Ÿ�
    Rigidbody2D Rigid;
    // Start is called before the first frame update
    void Start() //sss
    {
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
                Rigid.velocity = new Vector2(speed, 0);
            }
            else if (PlayerObj.transform.position.x < Rigid.transform.position.x)
            {
                Rigid.velocity = new Vector2(-speed, 0);
            }
        }
    
    }
}
