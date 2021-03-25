using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkdata;

    // Start is called before the first frame update
    void Awake()
    {
        talkdata = new Dictionary<int, string[]>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        talkdata.Add(1000, new string[] {"�鰳 ����!", "�鰳��.."});
        talkdata.Add(1001, new string[] { "�׽�Ʈ�� ǥ���� �Դϴ�" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if(talkIndex == talkdata[id].Length)
        {
            return null;
        }
        else return talkdata[id][talkIndex];
    }
}
