using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_Bar : MonoBehaviour
{
    public Player Player;
    public Image hpbar;
    public float HPba;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HPba = Player.ply_HP;
        hpbar.fillAmount = HPba / 100f;
    }
}
