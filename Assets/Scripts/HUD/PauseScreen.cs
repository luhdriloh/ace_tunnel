using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public Button _resumeButton;
    public Button _backToHomeScreenButton;
    
	private void Start ()
    {
        _resumeButton.onClick.AddListener(Resume);
	}

    private void Resume()
    {
        GameController._controller.Pause();
    }
}
