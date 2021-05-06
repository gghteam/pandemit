using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoctor_Potion : MonoBehaviour
{
    [SerializeField]
    private GameObject dil;
    [SerializeField]
    private playercamera playercamera;

    Rigidbody2D rigid;
    void Start()
    {
        playercamera = FindObjectOfType<playercamera>();
        rigid = GetComponent<Rigidbody2D>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") //공격해서 플레이어를 맞춤
        {
            if (!collision.GetComponent<PrototypeHero>().m_dodging)
            {
                collision.GetComponent<PrototypeHero>().damagedani();

                //데미지 출력
                GameObject hello = Instantiate(dil);
                hello.transform.position = (collision.transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0));
                hello.GetComponent<damage>().damagechk = 10;
                playercamera.GetComponent<playercamera>().startshake(0.4f, 0.2f);
                Destroy(gameObject);
            }
        }
        if(collision.gameObject.layer == 7)
        {
            Destroy(gameObject);
        }
    }
}
