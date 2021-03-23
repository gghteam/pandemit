using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscenefade : MonoBehaviour
{
    Vector3 velo = Vector3.zero;
    [SerializeField]
    private bool dir; //트루면 아래로 구라면 위로
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, dir == true ? 15:264,transform.position.z),ref velo, 0, 10000);
    }

    public void OnCutSence()
    {
        image.color = new Color(0f,0f,0f,1f);
        transform.position = new Vector3(transform.position.x, dir == true ? -20 : 300, transform.position.z);
    }

    public IEnumerator OffCutSence()
    {
        for(float alpha=1; alpha>=0;alpha-=0.1f)
        {
            image.color = new Color(0f, 0f, 0f, alpha);
            yield return new WaitForSeconds(0.05f);
        }
        gameObject.SetActive(false);
        yield return null;
    }

}
