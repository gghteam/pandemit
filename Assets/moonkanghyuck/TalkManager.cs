using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    private static TextManager instance = null;
    Dictionary<int, string[]> talkdata;
    public Sheet1 sheet1;

    // Start is called before the first frame update
    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        talkdata = new Dictionary<int, string[]>();
        GenerateData();
    }
    public static TextManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    // Update is called once per frame
    void GenerateData()
    {
        for(int i =1;sheet1.dataArray[i].Stringnumber != '0'; i++)
        {
            //Debug.Log("�ѹ�:" + sheet1.dataArray[i].Stringnumber + " �ؽ�Ʈ " + sheet1.dataArray[i].Talk1[0]);
            //for(int j = 0; sheet1.dataArray[i].Talk1[j] != null; j++)
            //{
                talkdata.Add(sheet1.dataArray[i].Stringnumber, sheet1.dataArray[i].Talk1);
            //}
        }
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
