using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerroguelike : MonoBehaviour
{
    GameObject allgamemanager;
    public GameObject room;
    void Start()
    {
        allgamemanager = GameObject.Find("AllgameManager");
        transform.position=new Vector3(allgamemanager.GetComponent<AllgameManager>().myX*room.transform.localScale.x,
        allgamemanager.GetComponent<AllgameManager>().myY*room.transform.localScale.x,0);
    }

    void Update()
    {
        
    }
}
