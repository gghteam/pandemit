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
        talkdata.Add(1000, new string[] {"들개 조심!", "들개라.."});
        talkdata.Add(1001, new string[] { "테스트용 표지판 입니다" });
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
