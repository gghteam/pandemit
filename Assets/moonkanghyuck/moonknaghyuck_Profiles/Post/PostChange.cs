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

    public int Num { get; set; } = 0;

    private void Start()
    {
        postProcessVolume = GetComponent<Volume>();
    }

    private void Update()
    {
        postProcessVolume.profile = postProcessProfiles[Num];
    }
    //public void ChangePostProcess(int num)
    //{
    //    postProcessVolume.profile = postProcessProfiles[num];
    //}
}
