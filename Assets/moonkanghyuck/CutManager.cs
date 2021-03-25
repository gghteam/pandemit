using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutManager : MonoBehaviour
{
    public Cutscenefade[] fade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCunsecne()
    {
        fade[0].gameObject.SetActive(true);
        fade[1].gameObject.SetActive(true);
        fade[0].OnCutSence();
        fade[1].OnCutSence();
    }
    public void OffCunsecne()
    {
        fade[0].StartCoroutine(fade[0].OffCutSence());
        fade[1].StartCoroutine(fade[1].OffCutSence());
    }
}
