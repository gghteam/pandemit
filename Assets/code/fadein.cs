using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class fadein : MonoBehaviour
{
    GameObject allgamemanager;
    GameObject stagemanager;
    public bool fadeoutbool,slowout;
    GameObject SplashObj;              
    Image image;
    public Text text; 
    public float delay=2.0f,speed=0.01f;                     
    private bool checkbool = false;    
    void Awake()
    {
        stagemanager=GameObject.Find("stagemanager");
        allgamemanager=GameObject.Find("AllgameManager");

        SplashObj = this.gameObject;                        

        image = gameObject.GetComponent<Image>();

    }
    

    void Update()

    {
        if(stagemanager.GetComponent<mapcode>().endending)
            fadeoutbool=true;

        StartCoroutine("MainSplash");

        if (checkbool)                                           
        {
            //Destroy(this.gameObject);                       
        }
    }



    IEnumerator MainSplash()
    {
        if(fadeoutbool&&slowout)
            yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(delay);
        Color color = image.color;
        for (int i = 100; i >= 0; i--)

        {
            if(fadeoutbool){
                color.a += Time.deltaTime * speed;
            }
            else
                color.a -= Time.deltaTime * speed;
            image.color = color; 
            if (text!=null)
                text.color = color;
            if (!fadeoutbool&&image.color.a <= 0)
            {
                color.a=0;
                checkbool = true;               
            }
            if (fadeoutbool&&image.color.a >= 1)
            {
                color.a=1;
                checkbool = true;
                if (slowout){
                    SceneManager.LoadScene("Roguelike");
                    yield return null;
                }


            }
        }
        
        yield return null;                                 
    }
    
}
