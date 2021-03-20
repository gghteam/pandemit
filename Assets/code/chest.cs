using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chest : MonoBehaviour
{
    public SpriteRenderer renderer;
    public GameObject reward;
    public Sprite Testsprite;
    public int isopen = 0;


    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    public void change()
    {
        renderer.sprite = Testsprite;
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
