using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class 대사 : MonoBehaviour
{
    public Text m_TypingText; 
    public string m_Message;     
    public float m_Speed; 

    // Start is called before the first frame update 
    void Start() 
    { 
        Destroy(gameObject,4.5f);
        m_Message = @"여기서부턴 데려다줄수 없을것 
        같구나.. 조심하는게 좋을꺼야..."; 

        StartCoroutine(Typing(m_TypingText, m_Message, m_Speed)); 
    } 

    IEnumerator Typing(Text typingText, string message, float speed) 
    { 
        for (int i = 0; i < message.Length; i++) 
        { 
            typingText.text = message.Substring(0, i + 1); 
            yield return new WaitForSeconds(speed); 
        } 
    } 
}
