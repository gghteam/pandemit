using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WALLFINDER : MonoBehaviour
{
    public static bool PasibleWallJump;
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
            PasibleWallJump=true;
    }
    void OnTriggerExit2D(Collider2D other) {
            PasibleWallJump=false;

    }
    

    void Update()
    {

    }
}