using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public Text _timeElapsedText;
    private float _timeElapsed;

	void Start ()
    {
        _timeElapsed = 0f;
    }
	
	private void Update ()
    {
        UpdateTimeValue();
	}

    private void UpdateTimeValue ()
    {
        _timeElapsed += Time.deltaTime;
        int min = (int)_timeElapsed / 60;
        int seconds = (int)_timeElapsed % 60;
        int centiSeconds = (int)(100 * (_timeElapsed - Mathf.Floor(_timeElapsed)));

        string minutesField = "0" + min.ToString();
        string secondsField = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
        string centiSecondsField = centiSeconds < 10 ? "0" + centiSeconds.ToString() : centiSeconds.ToString();
        _timeElapsedText.text = minutesField + ":" + secondsField + ":" + centiSecondsField;
    }
}
