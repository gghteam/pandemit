using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PostChange : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume postProcessVolume = null;

    [SerializeField]
    private PostProcessProfile[] postProcessProfiles = null;

    [SerializeField]
    private int num = 0;

    private void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
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
