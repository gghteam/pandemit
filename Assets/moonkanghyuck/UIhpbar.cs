using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIhpbar : MonoBehaviour
{
    [SerializeField]
    private float hp;
    [SerializeField]
    private float maxhp;

    Image hpimage;

    private void Start()
    {
        maxhp = gamemanager.instance.maxhp;
        hp = gamemanager.instance.hp;
        hpimage = GetComponent<Image>();
    }
    void Update()
    {
        hpimage.fillAmount = hp / maxhp;
    }
}
