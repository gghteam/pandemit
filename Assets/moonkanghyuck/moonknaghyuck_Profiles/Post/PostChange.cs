using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PostChange : MonoBehaviour
{
    [SerializeField]
    private Volume postProcessVolume = null;

    [SerializeField]
    private VolumeProfile[] postProcessProfiles = null;

    [SerializeField]
    private int num = 0;

    private void Start()
    {
        postProcessVolume = GetComponent<Volume>();
    }

    private void Update()
    {
        postProcessVolume.profile = postProcessProfiles[num];
    }
    //public void ChangePostProcess(int num)
    //{
    //    postProcessVolume.profile = postProcessProfiles[num];
    //}
}
