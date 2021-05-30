using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtenEscManager : MonoBehaviour
{
        public void Quit(){
        Application.Quit();
    }
    public void Continue(){
        gamemanager.instance.Continue();
    }
}
