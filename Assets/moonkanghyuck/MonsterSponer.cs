using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSponer : MonoBehaviour
{
    [SerializeField]
    List<GameObject> monsters = new List<GameObject> { }; // 사용하기 전에 크기와 몬스터프리펩을 넣어줘야함

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
            int random = Random.Range(0, monsters.Count);
            int randomper = Random.Range(0, random + 1);
            Instantiate(monsters[randomper], transforms[i].position, Quaternion.identity);
        }
    }
}
