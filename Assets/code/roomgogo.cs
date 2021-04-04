using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class roomgogo : MonoBehaviour
{
    private GameObject target;
    GameObject allgamemanager;
    public Animator animator;
    public int randomroom;
    public int myx=0,myy=0;
    void Start()
    {
        allgamemanager = GameObject.Find("AllgameManager");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        animator.SetInteger("New Int", randomroom);
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
        
        
        if (Mathf.RoundToInt(myx - allgamemanager.GetComponent<AllgameManager>().myX)==1||
        Mathf.RoundToInt(myy - allgamemanager.GetComponent<AllgameManager>().myY)==1){
            if (!(Mathf.RoundToInt(myx - allgamemanager.GetComponent<AllgameManager>().myX)==1&&
            Mathf.RoundToInt(myy - allgamemanager.GetComponent<AllgameManager>().myY)==1)){
        
                if(hit.collider != null){
                    target = hit.collider.gameObject;
                    if (target.gameObject == this.gameObject)
                    {
                        gameObject.GetComponent<SpriteRenderer>().color=new Color(0.5f,0.5f,0.5f);
                        if(Input.GetMouseButtonDown(0)){
                            gameObject.GetComponent<SpriteRenderer>().color=new Color(1,1,1);
                            allgamemanager.GetComponent<AllgameManager>().myX=myx;
                            allgamemanager.GetComponent<AllgameManager>().myY=myy;
                            SceneManager.LoadScene("kimhyengjoo");
                        }
                    }
                }
                else
                        gameObject.GetComponent<SpriteRenderer>().color=new Color(1,1,1);
             }
        }
    }
}
