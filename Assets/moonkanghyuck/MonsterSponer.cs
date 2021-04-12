using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSponer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> monsters = new List<GameObject> { }; // 사용하기 전에 크기와 몬스터프리펩을 넣어줘야함
    [SerializeField]
    private List<int> randompersent = new List<int> {20,20,60}; // 사용하기 전에 크기와 몬스터 순서에 맞게 확률을 넣어줘야함

    [SerializeField]
    Transform[] transforms; // 사용하기 전에 크기 설정해줘야함

    // Start is called before the first frame update
    void Start() // 시작할 때 트랜스폼을 찾아서 사용함
    {
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

    void Spone() // 소환 함수
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
