using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerroguelike : MonoBehaviour
{
    gamemanager allgamemanager;
    public GameObject room;
    void Start()
    {
        allgamemanager=FindObjectOfType<gamemanager>();
        if(allgamemanager==null) return;
        transform.position=new Vector3(allgamemanager.GetComponent<gamemanager>().myX*room.transform.localScale.x,
        allgamemanager.GetComponent<gamemanager>().myY*room.transform.localScale.x,0);
    }

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.W))
        // if(Input.GetKeyDown(KeyCode.A))
        // if(Input.GetKeyDown(KeyCode.S))
        // if(Input.GetKeyDown(KeyCode.D))
        if(allgamemanager==null) return;
        transform.position=new Vector3(allgamemanager.GetComponent<gamemanager>().myX*room.transform.localScale.x,
        allgamemanager.GetComponent<gamemanager>().myY*room.transform.localScale.x,0);
    }
}
