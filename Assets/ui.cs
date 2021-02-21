using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ui : MonoBehaviour
{
    public GameObject player;
    public GameObject startmap;
    public GameObject startui;
    public GameObject startbutton;
    public GameObject background;

    public Camera camera1;
    public Camera camera2;

    
    void Start()
    {
        camera2.enabled = false;
        camera1.enabled = true;
    }

    void Update()
    {
        
    }
    public void Startgame()
    {
        background.SetActive(true);
        startmap.SetActive(true);
        player.SetActive(true);
        startui.SetActive(false);
        startbutton.SetActive(false);
        camera2.enabled = true;
        camera1.enabled = false;
        
    }
}
