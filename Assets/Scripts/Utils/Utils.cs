using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static string ReturnTimeStringFromFloat(float timeValue)
    {
        int min = (int)timeValue / 60;
        int seconds = (int)timeValue % 60;
        int centiSeconds = (int)(100 * (timeValue - Mathf.Floor(timeValue)));

        string minutesField = "0" + min.ToString();
        string secondsField = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
        string centiSecondsField = centiSeconds < 10 ? "0" + centiSeconds.ToString() : centiSeconds.ToString();

        return minutesField + ":" + secondsField + ":" + centiSecondsField;
    }
}
