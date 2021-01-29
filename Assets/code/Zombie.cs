using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Player PlayerObj;
    public double rangth; //좀비와 플레이어의 거리 체크 변수
    public int speed; //좀비의 속도
    public double rangthf; //일정거리
    Rigidbody2D Rigid;
    // Start is called before the first frame update
    void Start() //sss
    {
        Rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rangth = Mathf.Abs(PlayerObj.transform.position.x) - Mathf.Abs(Rigid.transform.position.x); //좀비와 플레이어의 거리
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
