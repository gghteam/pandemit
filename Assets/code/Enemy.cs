using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float nuckback = 3f;

    protected GameObject player;
    HPbar healthbar;

    public bool gogo;

    public float HP;
    public float MAXHP;
    protected PoolManager poolManager;

    Rigidbody2D Rigid;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        healthbar = GetComponentInChildren<HPbar>();
        player = FindObjectOfType<PrototypeHero>().gameObject;
        HP = MAXHP;
        healthbar.sethealth(HP, MAXHP);
        gogo = true;
        Rigid = gameObject.GetComponent<Rigidbody2D>();
        poolManager = FindObjectOfType<PoolManager>();
    }

    public virtual void Hit(int damege) //데미지를 받음
    {
        if (HP > 0)
        {
            HP -= damege;
            healthbar.sethealth(HP, MAXHP);
            int angle = player.transform.position.x > Rigid.transform.position.x ? 1 : -1;
            Rigid.velocity = new Vector2(-nuckback * angle, Rigid.velocity.y);
            gogo = false;
        }
    }
}
