using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapcode : MonoBehaviour
{
    [SerializeField]
    private float MapendingPoint=13;
    public GameObject playercamera,player,faidin;
    void Start()
    {
        playercamera.GetComponent<playercamera>().maxPos.x = MapendingPoint;
    }

    void Update()
    {
        if (player.transform.position.x>=MapendingPoint){
            Debug.Log("dddddd");
        }
    }
}
