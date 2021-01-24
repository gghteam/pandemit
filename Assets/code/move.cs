using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public bool PasibleJump;
    void OnCollisionStay2D(Collision2D col) {
        PasibleJump=true;

    }
    void OnCollisionExit2D(Collision2D col) {
        PasibleJump=false;
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void FixedUpdate()   {
        if (Input.GetKey (KeyCode.A)){
            if (BOTTOMFINDER.PasibleBottomJump==false){
                if (Input.GetKey (KeyCode.Space)&&PasibleJump == true){
                    if(WALLFINDER.PasibleWallJump==true){
                        GetComponent<Rigidbody2D>().AddForce(Vector3.right*200f);
                        GetComponent<Rigidbody2D>().AddForce(Vector3.up*400f);
                    }
                }
            }
            //Debug.Log (jun);
            //Debug.Log (this.gameObject.transform.position.x);
            GetComponent<Rigidbody2D>().AddForce(Vector3.left*16f);
        }
        if (Input.GetKey (KeyCode.D)){ 
            if (BOTTOMFINDER.PasibleBottomJump==false){
                if (Input.GetKey (KeyCode.Space)&&PasibleJump == true){
                    if(WALLFINDER.PasibleWallJump==true){
                        GetComponent<Rigidbody2D>().AddForce(Vector3.left*200f);
                        GetComponent<Rigidbody2D>().AddForce(Vector3.up*400f);
                    }
                }
            }
            GetComponent<Rigidbody2D>().AddForce(Vector3.right*16f);
        }

            
        if (Input.GetKey (KeyCode.Space) &&BOTTOMFINDER.PasibleBottomJump==true &&PasibleJump == true ){
                GetComponent<Rigidbody2D>().AddForce(Vector3.up*500f);
        }

        if (Input.GetKeyDown (KeyCode.LeftControl)) {
            Debug.Log("앉기");
        }
        

        if(gameObject.transform.position[1]<-9) {
            transform.position = new Vector3(gameObject.transform.position[0],9,gameObject.transform.position[2]);
        }
        
        
        
        if (Input.GetKeyDown (KeyCode.C)){
            Instantiate(gameObject);
            GetComponent<SpriteRenderer>().color = Color.gray;

        }

        Vector3 worldpos = Camera.main.WorldToViewportPoint(this.transform.position);

        if (worldpos.x < 0f) worldpos.x = 0f;
        if (worldpos.y < 0f) worldpos.y = 0f;
        if (worldpos.x > 1f) worldpos.x = 1f;
        if (worldpos.y > 1f) worldpos.y = 1f;

        this.transform.position = Camera.main.ViewportToWorldPoint(worldpos);
        
    }
}
