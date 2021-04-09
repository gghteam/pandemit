using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

    // 대화
    public TalkManager talkmanager;
    public GameObject scanObject;
    public GameObject textpanel;
    //public CutManager cutmanager;
    public Animator fadeanim;
    public Animator fadeanim2;
    public Text talkText;
    public bool isAction;
    public int talkIndex;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Talk(int id, bool isNpc)
    {
        string talkdata = talkmanager.GetTalk(id, talkIndex);
        if(talkdata == null) //대화가 끝날 때
        {
            
            fadeanim.SetBool("IsText?", false);
            fadeanim2.SetBool("IsText?", false);
            isAction = false;
            talkIndex = 0;
            return;
        }
        if (isNpc)
        {
            talkText.text = talkdata;
        }
        else
        {
            talkText.text = talkdata;
        }
        isAction = true;
    }
    public void Action(GameObject scanObj)
    {
            if (isAction)
            {
                talkIndex++;
            }
            else
            {
            //대화 시작
            fadeanim.SetBool("IsText?", true);
            fadeanim2.SetBool("IsText?", true);
            //대충 주인공 멈추는 스크립트 
        }

            scanObject = scanObj;
            Objdata objdata = scanObject.GetComponent<Objdata>();
            Talk(objdata.id,objdata.isNpc);
            textpanel.SetActive(isAction);
            
    }
}
