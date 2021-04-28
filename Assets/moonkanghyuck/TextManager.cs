using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    // 대화
    [SerializeField]
    private TextManager talkmanager = TextManager.Instance;

    public GameObject scanObject;
    public GameObject textpanel;

    [SerializeField]
    private Animator fadeanim;
    [SerializeField]
    private Animator fadeanim2;
    [SerializeField]
    private Animator UIHpbar;
    [SerializeField]
    private Animator UIWashbar;
    [SerializeField]
    private Animator UIHpback;
    [SerializeField]
    private Animator UIWashback;

    [SerializeField]
    private TextEffect talkText;
    public bool isAction;
    public int talkIndex;

    private void Start()
    {
        //UIHpbar = GameObject.Find("Canvas").transform.Find("UIhpbar").GetComponent<Animator>();
        //UIWashbar = GameObject.Find("Canvas").transform.Find("UIwashbar").GetComponent<Animator>();
    }
    //텍스트 출력
    public void Talk(int id, bool isNpc)
    {
        string talkdata = talkmanager.GetTalk(id, talkIndex);

        if(talkdata == null) //대화가 끝날 때
        {
            
            fadeanim.SetBool("IsText?", false);
            fadeanim2.SetBool("IsText?", false);
            UIHpbar.SetBool("IsText?", false);
            UIWashbar.SetBool("IsText?", false);
            UIHpback.SetBool("IsText?", false);
            UIWashback.SetBool("IsText?", false);
            isAction = false;
            talkIndex = 0;
            return;
        }
        if (isNpc)
        {
            talkText.SetMsg(talkdata);
        }
        else
        {
            talkText.SetMsg(talkdata);
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
            UIHpbar.SetBool("IsText?", true);
            UIWashbar.SetBool("IsText?", true);
            UIHpback.SetBool("IsText?", true);
            UIWashback.SetBool("IsText?", true);
            //대충 주인공 멈추는 스크립트 
        }

            scanObject = scanObj;
            Objdata objdata = scanObject.GetComponent<Objdata>();
            Talk(objdata.id,objdata.isNpc);
            textpanel.SetActive(isAction);
            
    }
}
