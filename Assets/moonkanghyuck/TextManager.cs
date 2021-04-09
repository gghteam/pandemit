using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

    // ��ȭ
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
        if(talkdata == null) //��ȭ�� ���� ��
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
            //��ȭ ����
            fadeanim.SetBool("IsText?", true);
            fadeanim2.SetBool("IsText?", true);
            //���� ���ΰ� ���ߴ� ��ũ��Ʈ 
        }

            scanObject = scanObj;
            Objdata objdata = scanObject.GetComponent<Objdata>();
            Talk(objdata.id,objdata.isNpc);
            textpanel.SetActive(isAction);
            
    }
}
