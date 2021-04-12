using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    public float CharPerSeconds;
    public GameObject EndCursor;
    public string targetMsg;
    public Text msgText;
    public int index;

    public void SetMsg(string msg)
    {
        msgText.text = "";
        targetMsg = msg;
        EffectStart();
    }

    void EffectStart()
    {
        index = 0;
        msgText.text += targetMsg[index];
        //EndCursor.SetActive(false);
        Invoke("Effecting", 1 / CharPerSeconds);
    }
    
    void Effecting()
    {
        if(msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        index++;
        msgText.text += targetMsg[index];
        Invoke("Effecting", 1 / CharPerSeconds);
        Debug.Log(1 / CharPerSeconds);
    }
    
    void EffectEnd()
    {
        //EndCursor.SetActive(true);
    }
}
