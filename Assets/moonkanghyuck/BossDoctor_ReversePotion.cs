using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoctor_ReversePotion : BossDoctor_Potion
{
    [SerializeField]
    private GameObject reverseFog;
    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)// Wall
        {
            DestroyGameObject();
        }
    }
    protected override void DestroyGameObject()
    {
        GetFog();
        transform.SetParent(poolManager.transform);
        gameObject.SetActive(false);
    }
    private void GetFog()
    {
        var syringerson = poolManager.transform.GetComponentInChildren<BossDoctor_ReverseFog>(true);
        if (syringerson == null)
        {
            Instantiate(reverseFog, transform.position, Quaternion.identity);
        }
        else
        {
            syringerson.transform.position = transform.position;
            syringerson.transform.SetParent(null);
            syringerson.gameObject.SetActive(true);
        }
    }
}
