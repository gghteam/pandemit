using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chest : MonoBehaviour
{
    Animator anim;
    public GameObject reward;
    public int isopen = 0;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void change()
    {
        anim.SetTrigger("open?"); // ���� ������ �ִϸ��̼�
    }

    public void reward_event()
    {
        reward.SetActive(true);
    }

    public void button_out()
    {
        reward.SetActive(false);
    }
}
