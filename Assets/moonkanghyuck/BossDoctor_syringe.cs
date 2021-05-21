using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoctor_syringe : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float speed = 5f;
    private float timer = 3f;
    WaitForFixedUpdate wait = new WaitForFixedUpdate();
    private Vector2 dir;
    private float angle;
    private Quaternion angleAxis;
    private Material material;
    private Collider2D col;
    private void Start()
    {
        col = GetComponent<Collider2D>();
        material = GetComponent<SpriteRenderer>().material;
        player = FindObjectOfType<PrototypeHero>().gameObject;
        col.enabled = false;
        StartCoroutine(Attack());
    }


    private IEnumerator Attack()
    {
        float materialscale = 0;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            SetAngle();
            materialscale = Mathf.Lerp(materialscale, 1000, 0.05f * Time.deltaTime);
            material.SetFloat("_Causticspower", materialscale);
            yield return wait;
        }
        Vector2 target = player.transform.position;
        SetAngle();
        yield return new WaitForSeconds(0.5f);
        col.enabled = true;
        while (true)
        {
            //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            speed += 0.5f;
            yield return wait;
        }
        Destroy(gameObject);
    }

    private void SetAngle()
    {
        dir = player.transform.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, angleAxis, speed * Time.deltaTime);
    }
    private Vector2 Vec2Abs(Vector2 target)
    {
        Vector2 absvector;
        absvector.x = Mathf.Abs(target.x);
        absvector.y = Mathf.Abs(target.y);
        return absvector;
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
