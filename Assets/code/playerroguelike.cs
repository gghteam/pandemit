using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerroguelike : MonoBehaviour
{
    public GameObject room;
    void Start()
    {
        transform.position=new Vector3(gamemanager.instance.myX*room.transform.localScale.x,
        gamemanager.instance.myY*room.transform.localScale.x,0);
    }

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.W))
        // if(Input.GetKeyDown(KeyCode.A))
        // if(Input.GetKeyDown(KeyCode.S))
        // if(Input.GetKeyDown(KeyCode.D))
        transform.position=new Vector3(gamemanager.instance.myX*room.transform.localScale.x,
        gamemanager.instance.myY*room.transform.localScale.x,0);
    }
}
