using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Depthも持つ必要がある。
public class _TapEventManager
{
    public float timeStamp;
    public Vector3 tapPosition;
    public float depth;
    public _TapEventManager()
    {
        tapPosition = Input.mousePosition;
        DateTime now = DateTime.Now;
        timeStamp = ConvertDatetimeToFloat(now);
        depth = 0f;
    }
    public _TapEventManager(float depth, Vector3 tapPosition)
    {
        DateTime now = DateTime.Now;
        timeStamp = ConvertDatetimeToFloat(now);

        tapPosition = tapPosition;
        depth = depth;
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