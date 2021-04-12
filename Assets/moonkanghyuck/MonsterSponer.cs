using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSponer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> monsters = new List<GameObject> { }; // ����ϱ� ���� ũ��� ������������ �־������
    [SerializeField]
    private List<int> randompersent = new List<int> {20,20,60}; // ����ϱ� ���� ũ��� ���� ������ �°� Ȯ���� �־������

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
