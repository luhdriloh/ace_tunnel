using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSelectButton : MonoBehaviour
{
    public AudioClip _levelAudioClip;
    public Color _levelColor;
    public int _levelSelected;
    public int _levelSeed;

    public float _minDirectionTravelTime;
    public float _maxDirectionTravelTime;

    public float _minRestTravelTime;
    public float _maxRestTravelTime;

    private Button _levelButton;

    // Use this for initialization
    private void Start()
    {
        _levelButton = GetComponent<Button>();
        _levelButton.onClick.AddListener(PlayGame);
    }

    private void PlayGame()
    {
        LevelSelectData._levelSelect._levelSelected = _levelSelected;
        LevelSelectData._levelSelect._levelAudioClip = _levelAudioClip;
        LevelSelectData._levelSelect._levelColor = _levelColor;
        LevelSelectData._levelSelect._levelSeed = _levelSeed;
        LevelSelectData._levelSelect._minRestTravelTime = _minRestTravelTime;
        LevelSelectData._levelSelect._maxRestTravelTime = _maxRestTravelTime;
        LevelSelectData._levelSelect._minDirectionTravelTime = _minDirectionTravelTime;
        LevelSelectData._levelSelect._maxDirectionTravelTime = _maxDirectionTravelTime;

        SceneManager.LoadScene("GamePlayScene");
    }
}
