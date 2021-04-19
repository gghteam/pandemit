using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    // ��ȭ
    [SerializeField]
    private TextManager talkmanager = TextManager.Instance;

    public GameObject scanObject;
    public GameObject textpanel;

    [SerializeField]
    private Animator fadeanim;
    [SerializeField]
    private Animator fadeanim2;
    [SerializeField]
    private TextEffect talkText;
    public bool isAction;
    public int talkIndex;

    //�ؽ�Ʈ ���
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
