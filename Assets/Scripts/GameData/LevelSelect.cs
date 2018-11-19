using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public static LevelSelect _levelSelect;

    public int _levelSelected;
    public AudioClip _levelAudioClip;

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
