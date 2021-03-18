using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset;
    // Start is called before the first frame update
    
    public void sethealth(float health, float maxhealth){
        slider.gameObject.SetActive(health<maxhealth);
        slider.value = health;
        slider.maxValue = maxhealth;
        CancelInvoke();
        Invoke("nono",2);
        if (health<0)
            slider.gameObject.SetActive(false);
    }
    void nono(){
        slider.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        

        //Debug.Log(transform.parent.position);
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
