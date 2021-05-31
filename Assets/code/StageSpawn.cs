using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject NormalMaps;
    [SerializeField]
    private GameObject store;
    [SerializeField]
    private GameObject box;
    [SerializeField]
    private GameObject boss;
    void Start()
    {
        
        switch(gamemanager.instance.xy[gamemanager.instance.myX+10,gamemanager.instance.myY+10,0]){
            case 2:
            NormalMaps.transform.GetChild(gamemanager.instance.xy[gamemanager.instance.myX+10,gamemanager.instance.myY+10,2]).gameObject.SetActive(true);
            break;
            case 4:
            box.SetActive(true);
            break;
            case 3:
            store.SetActive(true);
            break;
            case 1:
            boss.SetActive(true);
            break;
        }
    }

    void Update()
    {
        
    }
}
