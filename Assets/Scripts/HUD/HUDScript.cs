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
        _timeElapsed += Time.deltaTime;
        _timeElapsedText.text = Utils.ReturnTimeStringFromFloat(_timeElapsed);
	}
}
