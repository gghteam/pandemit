using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSponer : MonoBehaviour
{
    [SerializeField]
    List<GameObject> monsters = new List<GameObject> { }; // ����ϱ� ���� ũ��� ������������ �־������

    [SerializeField]
    Transform[] transforms; // ����ϱ� ���� ũ�� �����������

    // Start is called before the first frame update
    void Start() // ������ �� Ʈ�������� ã�Ƽ� �����
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

    void Spone() // ��ȯ �Լ�
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            int random = Random.Range(0, monsters.Count);
            int randomper = Random.Range(0, random + 1);
            Instantiate(monsters[randomper], transforms[i].position, Quaternion.identity);
        }
    }
}
