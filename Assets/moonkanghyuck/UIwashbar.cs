using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIwashbar : MonoBehaviour
{

    Image washimage;

    private void Start()
    {
        washimage = GetComponent<Image>();
    }
    void Update()
    {
        washimage.fillAmount = gamemanager.instance.wash / gamemanager.instance.maxwash;
    }
}
