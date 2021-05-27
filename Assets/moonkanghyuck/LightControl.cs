using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class LightControl : MonoBehaviour
{
    private Light2D groballight;
    private Light2D pointlight;

    private void Start()
    {
        groballight = FindObjectOfType<Camera>().GetComponentInChildren<Light2D>();
        pointlight = GetComponent<Light2D>();
    }

    public void Globalzero()
    {
        groballight.intensity = 0;
        pointlight.intensity = 1;
    }

    public void Globalone()
    {
        groballight.intensity = 1;
        pointlight.intensity = 0;
    }
}
