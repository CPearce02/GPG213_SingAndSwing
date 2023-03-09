using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set4by3 : MonoBehaviour
{
    void Awake()
    {
        Screen.SetResolution((Screen.height/3) * 4, Screen.height, FullScreenMode.ExclusiveFullScreen);
    }
}
