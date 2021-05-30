using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoctor_BoundingSyringer : BossDoctor_syringe
{
    protected override void OnEnable()
    {
        base.OnEnable();
        speed = 3f;
    }
    private new void Update()
    {
        MaterialSet();
    }
    protected new void MaterialSet()
    {
        if (angletimer > 0)
        {
            angletimer -= Time.deltaTime;
            SetAngle();
            //생성될 때 쉐이더     
            materialscale = Mathf.Lerp(materialscale, 20, 0.1f * Time.deltaTime);
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
            speed += 0.1f;
        }
    }
}
