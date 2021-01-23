using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BOTTOMFINDER : MonoBehaviour
{
    public static bool PasibleBottomJump;

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
            PasibleBottomJump=true;
    }
    void OnTriggerExit2D(Collider2D other) {
            PasibleBottomJump=false;
    }
    
    void Update()
    {

    }
}

