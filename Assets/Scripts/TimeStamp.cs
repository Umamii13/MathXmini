using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TimeStamp : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Update()
    {
        DateTimeOffset.Now.ToUnixTimeMilliseconds();
        long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        text.text = "" + time;
        //DateTimeOffset.Now.ToUnixTimeSeconds()(.NET Framework 4.6 +/.NET Core), older versions: var epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
    }
}
