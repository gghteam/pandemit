using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class roomgogo : MonoBehaviour
{
    public Animator animator;
    public int randomroom;
    void Start()
    {
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
                SceneManager.LoadScene("kimhyengjoo");
            }
        }
    }
}
