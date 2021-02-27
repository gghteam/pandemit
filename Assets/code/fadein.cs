using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class fadein : MonoBehaviour
{
    GameObject SplashObj;               //판넬오브젝트
    Image image;                            //판넬 이미지
    private bool checkbool = false;     //투명도 조절 논리형 변수
    void Awake()
    {
        SplashObj = this.gameObject;                         //스크립트 참조된 오브젝트

        image = gameObject.GetComponent<Image>();    //판넬오브젝트에 이미지 참조

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
        Color color = image.color;
        for (int i = 100; i >= 0; i--)

        {
            color.a -= Time.deltaTime * 0.01f;
            image.color = color; 
            if (image.color.a <= 0)
            {
                checkbool = true;                           
            }
        }
        yield return null;                                 
    }
}
