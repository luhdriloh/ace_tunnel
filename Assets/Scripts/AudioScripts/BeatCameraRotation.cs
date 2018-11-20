using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCameraRotation : MonoBehaviour
{
    public float _maxRotationSpeedInAngles;
    private System.Random _random;
    private float _timeToMoveInDirection;
    private float _timePassed;

    private void Start()
    {
        _random = new System.Random(LevelSelectData._levelSelect._levelSeed);
        _timeToMoveInDirection = (float)_random.NextDouble() * 9 + 6;
        transform.Rotate(Vector3.zero);
    }

    // Update is called once per frame
    private void Update ()
    {
        _timePassed += Time.deltaTime;
        if (_timePassed > _timeToMoveInDirection)
        {
            _timePassed = 0f;
            _timeToMoveInDirection = (float)_random.NextDouble() * 9 + 6;
            _maxRotationSpeedInAngles *= -1;
        }

        float rotationSpeed = AudioPeer._frequencyBandBuffer[5] * _maxRotationSpeedInAngles * 500;
        rotationSpeed = (Mathf.Abs(rotationSpeed) < 110) ? rotationSpeed : rotationSpeed < 0 ? -110 : 110;
        transform.Rotate(new Vector3(0f, 0f, rotationSpeed * Time.deltaTime));
	}
}
