using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapcode : MonoBehaviour
{
    [SerializeField]
    public bool endending;
    [SerializeField]
    private float MapendingPoint=13;
    public GameObject playercamera,player,faidin;
    void Start()
    {
        faidin.SetActive(true);
        playercamera.GetComponent<playercamera>().maxPos.x = MapendingPoint;
    }

    void Update()
    {
        if ((!endending)&&player.transform.position.x-playercamera.transform.localScale.x>=MapendingPoint){
            faidin.transform.GetChild(1).GetComponent<fadein>().fadeoutbool=true;
            faidin.GetComponent<fadein>().fadeoutbool=true;
        }
    }
}
