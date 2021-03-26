using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class fadein : MonoBehaviour
{
    GameObject SplashObj;              
    Image image;
    public Text text; 
    public float delay=2.0f,speed=0.01f;                     
    private bool checkbool = false;    
    void Awake()
    {
        SplashObj = this.gameObject;                        

        image = gameObject.GetComponent<Image>();

    }



    void Update()

    {

        StartCoroutine("MainSplash");                       

        if (checkbool)                                           
        {
            Destroy(this.gameObject);                       
        }
    }



    IEnumerator MainSplash()
    {
        yield return new WaitForSeconds(delay);
        Color color = image.color;
        for (int i = 100; i >= 0; i--)

        {
            color.a -= Time.deltaTime * speed;
            image.color = color; 
            if (text!=null)
                text.color = color;
            if (image.color.a <= 0)
            {
                checkbool = true;               
            }
        }
        
        yield return null;                                 
    }
}
