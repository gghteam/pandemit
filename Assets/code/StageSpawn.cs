using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject NormalMaps;
    // Start is called before the first frame update
    void Start()
    {
        NormalMaps.transform.GetChild(gamemanager.instance.randoMap).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
