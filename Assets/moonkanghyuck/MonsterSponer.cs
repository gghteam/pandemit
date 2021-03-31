using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSponer : MonoBehaviour
{
    [SerializeField]
    List<GameObject> monsters = new List<GameObject> { };

    [SerializeField]
    Transform[] transforms;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i < transforms.Length; i++)
        {
            int random = Random.Range(0, monsters.Count);
            Instantiate(monsters[random], transforms[i]);
            Debug.Log("¼ÒÈ¯");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
