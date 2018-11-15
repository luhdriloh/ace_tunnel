using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartMenuSceneManagement : MonoBehaviour
{
    public Button _playButton;
    public Button _optionsButton;


    // Use this for initialization
    private void Start ()
    {
        _playButton.onClick.AddListener(PlayButtonClicked);
        _optionsButton.onClick.AddListener(OptionsButtonClicked);
    }

    private void PlayButtonClicked()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }

    private void OptionsButtonClicked()
    {
        SceneManager.LoadScene("GameOptionsScene");
    }
}
