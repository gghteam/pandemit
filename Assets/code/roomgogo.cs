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
        
        if(Input.GetMouseButtonDown(0)){
            if(hit.collider != null){
                target = hit.collider.gameObject;
                if (target.gameObject == this.gameObject)
                {
                    allgamemanager.GetComponent<AllgameManager>().myX=myx;
                    allgamemanager.GetComponent<AllgameManager>().myY=myy;
                    SceneManager.LoadScene("kimhyengjoo");
                }
            }
        }
    }
}
