using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSelectSceneManager : MonoBehaviour
{
    public int _levelSelected;
    public AudioClip _levelAudioClip;

    private Button _levelButton;

    // Use this for initialization
    private void Start()
    {
        _levelButton = GetComponent<Button>();
        _levelButton.onClick.AddListener(PlayGame);
    }

    private void PlayGame()
    {
        LevelSelect._levelSelect._levelSelected = _levelSelected;
        LevelSelect._levelSelect._levelAudioClip = _levelAudioClip;
        SceneManager.LoadScene("GamePlayScene");
    }
}
