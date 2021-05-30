using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoctor_syringe : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    protected float speed = 5f;
    private Vector2 dir;
    private float angle;
    private Quaternion angleAxis;
    protected Material material;
    protected Collider2D col;
    protected float materialscale = 0f;

    //Å¸ÀÌ¸Óµé
    protected float angletimer = 3f;
    protected float targettimer = 0f;

    //Ç®¸Å´ÏÀú
    private PoolManager poolManager;
    private void Start()
    {
        poolManager = FindObjectOfType<PoolManager>();
        material = GetComponent<SpriteRenderer>().material;
        player = FindObjectOfType<PrototypeHero>().gameObject;
    }
    protected virtual void OnEnable()
    {
        if(col == null)
            col = GetComponent<Collider2D>();
        col.enabled = false;
        angletimer = 3f;
        targettimer = 0f;
        materialscale = 0f;
        speed = 5f;
    }
    public virtual void Update()
    {
        MaterialSet();
    }
    protected virtual void MaterialSet()
    {
        if (angletimer > 0)
        {
            angletimer -= Time.deltaTime;
            SetAngle();
            //»ý¼ºµÉ ¶§ ½¦ÀÌ´õ     
            materialscale = Mathf.Lerp(materialscale, 1000, 0.05f * Time.deltaTime);
            material.SetFloat("_Causticspower", materialscale);
        }
        else if (targettimer < 0.5f)
        {
            targettimer += Time.deltaTime;
            col.enabled = true;
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            speed += 0.5f;
        }
    }

    protected void SetAngle()
    {
        dir = player.transform.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, angleAxis, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") //ï¿½ï¿½ï¿½ï¿½ï¿½Ø¼ï¿½ ï¿½Ã·ï¿½ï¿½Ì¾î¸¦ ï¿½ï¿½ï¿½ï¿½
        {
            if (!collision.GetComponent<PrototypeHero>().m_dodging)
            {
                collision.GetComponent<PrototypeHero>().damagedani();

                //ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿?
                DestroyGameObject();
            }
        }
        if (collision.gameObject.layer == 7)
        {
            DestroyGameObject();
        }
    }

    private void DestroyGameObject()
    {
        transform.SetParent(poolManager.transform);
        gameObject.SetActive(false);
    }
}
