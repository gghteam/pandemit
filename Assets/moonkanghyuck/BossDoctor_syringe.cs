using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoctor_syringe : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float speed = 5f;
    WaitForFixedUpdate wait = new WaitForFixedUpdate();
    private Vector2 dir;
    private float angle;
    private Quaternion angleAxis;
    private Material material;
    private Collider2D col;

    //타이머들
    private float angletimer = 3f;
    private float targettimer = 0f;
    private void Start()
    {
        col = GetComponent<Collider2D>();
        material = GetComponent<SpriteRenderer>().material;
        player = FindObjectOfType<PrototypeHero>().gameObject;
    }
    private void OnEnable()
    {
        col.enabled = false;
    }
    private void Update()
    {
        if(angletimer > 0)
        {
            angletimer -= Time.deltaTime;
            SetAngle();
            //생성될 때 비주얼 
            float materialscale = 0;
            materialscale = Mathf.Lerp(materialscale, 1000, 0.05f * Time.deltaTime);
            material.SetFloat("_Causticspower", materialscale);
        }
        else if(targettimer < 0.5f)
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

    private void SetAngle()
    {
        dir = player.transform.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, angleAxis, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") //공격해서 플레이어를 맞춤
        {
            if (!collision.GetComponent<PrototypeHero>().m_dodging)
            {
                collision.GetComponent<PrototypeHero>().damagedani();

                //데미지 출력
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("asd");
            Destroy(gameObject);
        }
    }
}
