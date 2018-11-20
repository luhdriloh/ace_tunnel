using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectData : MonoBehaviour
{
    public static LevelSelectData _levelSelect;

    public AudioClip _levelAudioClip;
    public Color _levelColor;
    public int _levelSelected;
    public int _levelSeed;

    public float _minDirectionTravelTime;
    public float _maxDirectionTravelTime;

    public float _minRestTravelTime;
    public float _maxRestTravelTime;

    private void Awake()
    {
        if (_levelSelect == null)
        {
            DontDestroyOnLoad(this);
            _levelSelect = this;
        }
        else if (_levelSelect != this)
        {
            Destroy(this);
        }
    }
}
