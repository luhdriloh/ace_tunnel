using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartMenuSceneManagement : MonoBehaviour
{
    public Button _playButton;
    public AudioClip _levelAudioClip;
    public Color _levelColor;

    private void Start ()
    {
        _playButton.onClick.AddListener(PlayButtonClicked);
    }

    private void OnEnable()
    {
        LevelSelectData._levelSelect._levelAudioClip = _levelAudioClip;
        LevelSelectData._levelSelect._levelColor = _levelColor;
    }

    private void PlayButtonClicked()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }
}
