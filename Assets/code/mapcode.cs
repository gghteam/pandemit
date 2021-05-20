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
    public GameObject playercamera,player,faidin,NormalMaps;
    void Start()
    {
        playercamera.GetComponent<playercamera>().maxPos.x = MapendingPoint;
        NormalMaps.transform.GetChild(gamemanager.instance.randoMap).gameObject.SetActive(true);
    }

    void Update()
    {
        if ((!endending)&&player.transform.position.x>=MapendingPoint+playercamera.transform.localScale.x){
            endending = true;
        }
    }
}
