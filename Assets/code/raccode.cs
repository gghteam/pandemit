using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class raccode : MonoBehaviour
{
    public Text ScriptTxt;
    float second=0;

     void Update()
    {
        second+=Time.deltaTime;
        string str = (Mathf.Round(second*100)*0.0001).ToString()+"000000";
        //Debug.Log(str);
        ScriptTxt.text = "00:00:"+str[2]+str[3]+":"+str[4]+str[5];
    }
}
