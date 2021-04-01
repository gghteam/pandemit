using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class roomgogo : MonoBehaviour
{
    public int myXX;
    public int myYY;
    GameObject systemmanager;
    public Animator animator;
    public int randomroom;
    void Start()
    {
        systemmanager = GameObject.Find("AllgameManager");
        animator = GetComponent<Animator>();

        
    }

    void Update()
    {
        animator.SetInteger("New Int", randomroom);
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
        if(Input.GetMouseButtonDown(0)){
            if (hit.collider != null)
            {
                systemmanager.GetComponent<AllgameManager>().myX = myXX;
                systemmanager.GetComponent<AllgameManager>().myY = myYY;
                SceneManager.LoadScene("kimhyengjoo");
            }
        }
    }
}
