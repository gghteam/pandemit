using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoctor_ReverseFog : MonoBehaviour
{
    //풀매니저
    private PoolManager poolManager;
    private PostChange postChange;
    private playercamera playercamera;


    private void Awake()
    {
        poolManager = FindObjectOfType<PoolManager>();
        postChange = FindObjectOfType<PostChange>(true);
        playercamera = FindObjectOfType<playercamera>();

    }

    private void OnEnable()
    {
        Invoke("DestroyGameObject", 5f);
    }
    protected void DestroyGameObject()
    {
        transform.SetParent(poolManager.transform);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            postChange.Num = 3;
            playercamera.OnChangeCull(-1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            postChange.Num = 0;
            playercamera.OnChangeCull(1);
        }
    }
}
