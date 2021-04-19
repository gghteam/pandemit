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
    public int[,,] xy=new int[40,40,3];

    private static AllgameManager instance = null;//싱글톤

    //싱글톤 스크립트
    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static AllgameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    void Start()
    {
        xy[10,10,0]=10;
        DontDestroyOnLoad(this);
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
