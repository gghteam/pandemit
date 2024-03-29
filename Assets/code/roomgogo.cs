using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class roomgogo : MonoBehaviour
{
    private GameObject target;
    public Animator animator;
    public int randomroom;
    public int myx=0,myy=0;
    void Start()
    {

        animator = GetComponent<Animator>();
        if(gamemanager.instance.xy[myx+10,myy+10,1]!=0){
            transform.GetChild(gamemanager.instance.xy[myx+10,myy+10,1]-1).gameObject.SetActive(true);
        }
    }

    void Update()
    {
        
        animator.SetInteger("New Int", randomroom);
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
        
        gameObject.GetComponent<SpriteRenderer>().color=new Color(0.5f,0.5f,0.5f);
        if ((Mathf.Abs(myx - gamemanager.instance.myX)==1&&Mathf.Abs(myy - gamemanager.instance.myY)==0)||
        (Mathf.Abs(myy - gamemanager.instance.myY)==1&&Mathf.Abs(myx - gamemanager.instance.myX)==0)){

            if(hit.collider != null){
                target = hit.collider.gameObject;
                if (target.gameObject == this.gameObject)
                {
                    gameObject.GetComponent<SpriteRenderer>().color=new Color(0.5f,0.5f,0.5f);
                    if(Input.GetMouseButtonDown(0)){
                        gameObject.GetComponent<SpriteRenderer>().color=new Color(1,1,1);
                        gamemanager.instance.myX=myx;
                        gamemanager.instance.myY=myy;
                        StartCoroutine(gogogo());
                    }
                }
                else gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
            }
            else gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
        }
    }
    IEnumerator gogogo(){
        gamemanager.instance.randoMap = Random.Range(0,gamemanager.instance.roomCnt);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("kimhyengjoo");
        
    }
}
