using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class damage : MonoBehaviour
{
    public float alphaspeed;
    TextMeshPro text;
    Color alpha;
    public int damagechk=0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = damagechk.ToString();
        Rigidbody2D Rigid = GetComponent<Rigidbody2D>();  
        alpha = text.color;
        Destroy(gameObject,1);
        //Rigid.velocity = new Vector2(Rigid.velocity.x, Rigid.velocity.y+4);
        Rigid.AddForce(new Vector2 (Random.Range(50,-50),200));
    }

    // Update is called once per frame
    void Update()
    {
        alpha.a = Mathf.Lerp(alpha.a,0,Time.deltaTime*alphaspeed);
        text.color = alpha;
    }
}
