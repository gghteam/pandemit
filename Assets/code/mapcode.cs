using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapcode : MonoBehaviour
{
    [SerializeField]
    public bool endending;
    private float MapendingPoint=13;
    public GameObject playercamera,player,faidin;
    void Start()
    {
        playercamera.GetComponent<playercamera>().maxPos.x = MapendingPoint;
    }

    void Update()
    {
        if ((!endending)&&player.transform.position.x>=MapendingPoint+playercamera.transform.localScale.x){
            endending = true;
        }
    }
}
