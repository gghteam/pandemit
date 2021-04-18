using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roguelikestart : MonoBehaviour
{
    gamemanager allgamemanager;
    // Start is called before the first frame update
    void Start()
    {
        allgamemanager=FindObjectOfType<gamemanager>();
        allgamemanager.GetComponent<gamemanager>().roguelike();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
