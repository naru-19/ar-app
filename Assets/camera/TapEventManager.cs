using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Depthも持つ必要がある。
public class TapEventManager
{
    public float timeStamp;
    public Vector3 tapPosition;
    public TapEventManager()
    {
        tapPosition = Input.mousePosition;
        DateTime now = DateTime.Now;
        timeStamp = ConvertDatetimeToFloat(now);
    }

    public float ConvertDatetimeToFloat(DateTime datetime)
    {
        DateTime now = DateTime.Now;
        // ちょうど月が変わるタイミングの0時だけバグる。
        return now.Day * 24 * 60 * 60 * 1000 +
                now.Hour * 60 * 60 * 1000 +
                now.Minute * 60 * 1000 +
                now.Second * 1000 +
                now.Millisecond;
    }
}