using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraResolutionFix : MonoBehaviour
{
    void Awake()
    {
        Screen.SetResolution(1080, 1920, true);
    }
}
