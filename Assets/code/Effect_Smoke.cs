using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Smoke : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        new Vector3(transform.position.x, transform.position.y, 3);
        Destroy(gameObject, 0.5f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}