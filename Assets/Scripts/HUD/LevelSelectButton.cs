using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSelectButton : MonoBehaviour
{
    public Text _highscoreValue;
    public GameObject _levelCompletionStars;
    public AudioClip _levelAudioClip;
    public Color _levelColor;
    public int _levelSelected;
    public int _levelSeed;

    public float _minDirectionTravelTime;
    public float _maxDirectionTravelTime;

    public float _minRestTravelTime;
    public float _maxRestTravelTime;

    public float _tunnelVelocity;

    private Button _levelButton;

    // Use this for initialization
    private void Start()
    {
        _levelButton = GetComponent<Button>();
        _levelButton.onClick.AddListener(PlayGame);

        bool levelComplete = GameStatsDataContainer._gameStatsInstance.ReturnLevelComplete(_levelSelected);
        if (levelComplete)
        {
            _levelCompletionStars.SetActive(true);
        }

        float highscoreValue = GameStatsDataContainer._gameStatsInstance.ReturnLevelHighscore(_levelSelected);
        _highscoreValue.text = Utils.ReturnTimeStringFromFloat(highscoreValue);
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
        LevelSelectData._levelSelect._tunnelVelocity = _tunnelVelocity;

        SceneManager.LoadScene("GamePlayScene");
    }
}
