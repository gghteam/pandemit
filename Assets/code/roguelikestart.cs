using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roguelikestart : MonoBehaviour
{
    void Start()
    {
        gamemanager.instance.roguelike();
    }
}
