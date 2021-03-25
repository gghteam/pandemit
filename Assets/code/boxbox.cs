using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxbox : MonoBehaviour
{
    public GameObject dil;
    private int boxHP=30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void boxhit(int damage){
        boxHP-=damage;  
        if (boxHP<0)
        {
            GameObject hello = Instantiate (dil);
            hello.transform.position=(transform.position);
            Destroy(gameObject);

        }
    }
}
