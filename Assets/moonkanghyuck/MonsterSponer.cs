using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSponer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> monsters = new List<GameObject> { }; // ?????? ???? ???? ???????????? ????????
    [SerializeField]
    private List<int> randompersent = new List<int> {20,20,60}; // ?????? ???? ???? ???? ?????? ?¡Æ? ????? ????????

    [SerializeField]
    Transform[] transforms; // ?????? ???? ??? ???????????

    // Start is called before the first frame update
    void Start() // ?????? ?? ????????? ???? ?????
    {
        if(gamemanager.instance.xy[gamemanager.instance.myX+10,gamemanager.instance.myY+10,1]!=0)
            return;
        for(int i = 0; i < transforms.Length;i++)
        {
            transforms[i] = transform.GetChild(i);
        }
       
        Spone();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spone() // ??? ???
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            int random = Randoms();
            Instantiate(monsters[random], transforms[i].position, Quaternion.identity);
        }
    }
    public int Randoms()
    {

        int total = 0;
        foreach (int p in randompersent)
        {
            total += p;
        }

        int rand = Random.Range(0, total);
        for (int i = 0; i < randompersent.Count; i++)
        {
            if (rand < randompersent[i])
            {
                return i;
            }
            else
            {
                rand -= randompersent[i];
            }
        }
        return 0;
    }
}
