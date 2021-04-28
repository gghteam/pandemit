using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIhpbar : MonoBehaviour
{

    Image hpimage;

    private void Start()
    {
        hpimage = GetComponent<Image>();
    }
    void Update()
    {
        hpimage.fillAmount = gamemanager.instance.hp / gamemanager.instance.maxhp;
    }
}
