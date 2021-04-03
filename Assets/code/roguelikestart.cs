using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roguelikestart : MonoBehaviour
{
    GameObject allgamemanager;
    // Start is called before the first frame update
    void Start()
    {
        allgamemanager = GameObject.Find("AllgameManager");
        allgamemanager.GetComponent<AllgameManager>().roguelike();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
