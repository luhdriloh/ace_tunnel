using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSizeUpdater : MonoBehaviour
{
    public float _maxScalingToBeAdded;
    private float _xScaling, _yScaling;

	private void Start ()
    {
        _xScaling = transform.localScale.x;
        _yScaling = transform.localScale.y;
	}
	
	private void Update ()
    {
        float scalingAddition = AudioPeer._amplitudeBuffer * _maxScalingToBeAdded;
        transform.localScale = new Vector3(scalingAddition + _xScaling, scalingAddition + _yScaling, 0);
	}
}
