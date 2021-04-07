using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllgameManager : MonoBehaviour
{
    [SerializeField]
    public int myX=0,myY= 0;
    private Animator roomani;
    [SerializeField]
    private int Progress=0;
    public GameObject roompp;
    [SerializeField]
    private int[,] xy=new int[40,40];
    
    
    void Start()
    {
        xy[10,10]=10;
        DontDestroyOnLoad(this);
    }
    void maploding(){
        
        int randomgogo = Random.Range(0,4);
        int randomroomgo = Random.Range(1,5);
        int roomX=0,roomY=0;

        switch(randomgogo){
            case 0:
            roomX = myX+1;
            roomY = myY;
            break;
            case 1:
            roomX = myX-1;
            roomY = myY;
            break;
            case 2:
            roomY = myY+1;
            roomX = myX;
            break;
            case 3:
            roomY = myY-1;
            roomX = myX;
            break;
        }
        if(xy[roomX+10,roomY+10]==0){
            xy[roomX+10,roomY+10]=randomroomgo;
        }   
        
        
        

    }
    public void roguelike(){
        Progress++;
        maploding();maploding();maploding();maploding();
        for(int i=0;i<20;i++){
            for(int j=0;j<20;j++){
                if(xy[i,j]!=0){
                    GameObject room = Instantiate(roompp);
                    room.GetComponent<roomgogo>().myx=i-10;
                    room.GetComponent<roomgogo>().myy=j-10;
                    room.GetComponent<roomgogo>().randomroom=xy[i,j];
                    room.transform.position = new Vector3(room.transform.localScale.x*(i-10),room.transform.localScale.y*(j -10),0);
                }
            }
        }
    }
    void Update()
    {

    }
}
