using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllgameManager : MonoBehaviour
{
    private Animator roomani;
    public GameObject roompp;

    int[,] xy=new int[20,20];
    int myX=0,myY= 0;
    int roomX=0,roomY=0;
    void Start()
    {
        
        DontDestroyOnLoad(this);
        maploding();
    }
    void maploding(){
        int randomgogo = Random.Range(0,4);
        int randomroomgo = Random.Range(0,4);

        switch(randomgogo){
            case 0:
            roomX = myX+1;
            break;
            case 1:
            roomX = myX-1;
            break;
            case 2:
            roomY = myY+1;
            break;
            case 3:
            roomY = myY-1;
            break;
        }
        xy[roomX+10,roomY+10]=randomroomgo;
        GameObject room = Instantiate(roompp);
        roomani = room.GetComponent<roomgogo>().animator;
        roomani.SetInteger("Roguelike", randomroomgo);
        room.transform.position = new Vector3(room.transform.localScale.x*roomX,room.transform.localScale.y*roomY,0);
        

    }
    void Update()
    {

    }
}
