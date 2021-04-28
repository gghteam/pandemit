using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIwashbar : MonoBehaviour
{
    [SerializeField]
    private float wash;
    [SerializeField]
    private float maxwash;

    Image hpimage;

    private void Start()
    {
        maxwash = gamemanager.instance.maxwash;
        wash = gamemanager.instance.wash;
        hpimage = GetComponent<Image>();
    }
    void Update()
    {
        hpimage.fillAmount = wash / maxwash;
    }
}
