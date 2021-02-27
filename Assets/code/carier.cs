using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carier : MonoBehaviour
{
    public GameObject tire;
    void Start(){
        Destroy(gameObject,4f);
        Invoke("gogo",2f);
    }
    void gogo(){
        GetComponent<Animator>().SetTrigger("New Trigger");
        tire.GetComponent<Animator>().SetTrigger("New Trigger");
    }

    
}
