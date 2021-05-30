using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class room{
    bool isClear;
    int x,y;
    
}

public class gamemanager : MonoBehaviour
{

    private static gamemanager _instance;
    public static gamemanager instance
    {
        get{
            if(_instance == null){
                _instance = FindObjectOfType<gamemanager>();
                if(_instance == null){
                    _instance = new GameObject("GameManager").AddComponent<gamemanager>();
                }
            }
            return _instance;
        }
    }

    [SerializeField]
    public int myX=0,myY= 0,randoMap;
    private Animator roomani;
    [SerializeField]
    private int Progress=0;
    public GameObject roompp,ESC;
    public int roomCnt=7;
    public int[,,] xy=new int[40,40,3];

    public float hp = 100;
    public float maxhp = 100;
    public float wash = 100;
    public float maxwash = 100,timeSpeed=0f;
    private bool ESCON;

    void Start()
    {
        ESC = FindObjectOfType<ButtenEscManager>().transform.GetChild(0).gameObject;
        randoMap = Random.Range(0,roomCnt);
        xy[10,10,0]=10;
        DontDestroyOnLoad(this);
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Continue();
        }
    }
    void maploding(){
        
        int randomgogo = Random.Range(0,4);
        int randomroomgo = Random.Range(1,101);
        if(randomroomgo<7){
            if(Progress>7)
                randomroomgo=1;
            else
                randomroomgo=2;
        }else if(randomroomgo<30){
            randomroomgo=4;
        }else if(randomroomgo<40){
            randomroomgo=3;
        }else if(randomroomgo<100){
            randomroomgo=2;
        }else{
            randomroomgo=2;
        }
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
        if(xy[roomX+10,roomY+10,0]==0){
            xy[roomX+10,roomY+10,0]=randomroomgo;
        }
    }
    public void Continue(){
        
        if(!ESCON){
            timeSpeed = Time.timeScale;
            ESC.SetActive(true);
            ESCON=true;
            Time.timeScale = 0f;
        }
        else {
            ESC.SetActive(false);
            ESCON=false;
            Time.timeScale = timeSpeed;
        }
    }
    public void roguelike(){
        Progress++;
        xy[myX+10,myY+10,1]=Random.Range(1,5);
        if(xy[myX+10,myY+10,0]!=1){
            maploding();maploding();maploding();maploding();
        }
        for(int i=0;i<20;i++){
            for(int j=0;j<20;j++){
                if(xy[i,j,0]!=0){
                    GameObject room = Instantiate(roompp);
                    room.GetComponent<roomgogo>().myx=i-10;
                    room.GetComponent<roomgogo>().myy=j-10;
                    room.GetComponent<roomgogo>().randomroom=xy[i,j,0];
                    room.transform.position = new Vector3(room.transform.localScale.x*(i-10),room.transform.localScale.y*(j -10),0);
                }
            }
        }
    }
}
