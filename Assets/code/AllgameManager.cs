using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllgameManager : MonoBehaviour
{
    private Animator roomani;
    public GameObject roompp;
    int[,] xy=new int[20,20];
    
    void Start()
    {
        
        DontDestroyOnLoad(this);
    }
    void maploding(){
        int myX=0,myY= 0;
        int roomX=0,roomY=0;
        int randomgogo = Random.Range(0,4);
        int randomroomgo = Random.Range(1,5);
        xy[10,10]=10;

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
        if(xy[roomX+10,roomY+10]==0){
            xy[roomX+10,roomY+10]=randomroomgo;
            GameObject room = Instantiate(roompp);
            roomani = room.GetComponent<roomgogo>().animator;
            room.GetComponent<roomgogo>().randomroom=randomroomgo;
            room.transform.position = new Vector3(room.transform.localScale.x*roomX,room.transform.localScale.y*roomY,0);
        }
        for(int i=0;i<20;i++){
            for(int j=0;j<20;j++){
                if(xy[i+10,j+10]!=0){
                    GameObject room = Instantiate(roompp);
                    room.GetComponent<roomgogo>().randomroom=randomroomgo;
                    room.transform.position = new Vector3(room.transform.localScale.x*roomX,room.transform.localScale.y*roomY,0);
                }
            }
        }
        
        

    }
    public void roguelike(){
        for(int i=0;i<4;i++){
            maploding();
        }
    }
    void Update()
    {

    }
}
