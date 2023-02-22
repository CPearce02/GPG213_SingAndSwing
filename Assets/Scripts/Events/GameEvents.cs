using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class GameEvents 
{
    public delegate void ScreenShake(Strength str, float lengthInSeconds = 0.2f);
    
    public static ScreenShake OnScreenShakeEvent;
}
