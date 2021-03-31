using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class raccode : MonoBehaviour
{
    public Text ScriptTxt;
    float second=0.5f;
    
     void Update()
    {
        second+=Time.deltaTime;
        if(second>10&&(int)second%60==0){
            second+=40;
        }
        string str = (Mathf.Round(second*100)*0.000001).ToString()+"000000";
        //Debug.Log(str);
        ScriptTxt.text = "00:"+str[2]+str[3]+":"+str[4]+str[5]+":"+str[6]+str[7];
        
    }
}
