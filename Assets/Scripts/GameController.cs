using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController _controller;
    public GameObject _pauseScreen;
    public GameObject _restartScreen;
    public GameObject _tunnelSpawner;

    private bool _isPaused;
    private bool _gameover;
    public bool _levelWon;
    private AudioPeer _audioPeer;

    private void Awake()
    {
        if (_controller == null)
        {
            _controller = this;
        }
        else if (_controller != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _audioPeer = GetComponentInChildren<AudioPeer>();
        Time.timeScale = 1;
        _isPaused = false;
        _gameover = false;
        _levelWon = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_gameover == false)
            {
                Pause();
            }
        }

        if (!_audioPeer.IsMusicPlaying() && !_gameover && !_isPaused)
        {
            LevelComplete();
        }
    }

    public void Pause()
    {
        if (_isPaused == true)
        {
            Time.timeScale = 1;
            _isPaused = false;
            _audioPeer.StartMusic();
            _pauseScreen.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            _isPaused = true;
            _audioPeer.StopMusic();
            _pauseScreen.SetActive(true);
        }
    }

    public void GameOver()
    {
        _gameover = true;
        GetComponentInChildren<TunnelSpawner>().UpdateCollidersAndTunnels();
        Time.timeScale = 0;
        _isPaused = true;
        _audioPeer.StopMusic();
        _restartScreen.SetActive(true);

        int level = LevelSelectData._levelSelect._levelSelected;
        GameStatsDataContainer._gameStatsInstance.UpdateLevelHighscore(level, AudioPeer._playbackProgressSeconds);
    }

    public void LevelComplete()
    {
        _tunnelSpawner.SetActive(false);
        _levelWon = true;
        Time.timeScale = 0;

        int level = LevelSelectData._levelSelect._levelSelected;
        float songLength = LevelSelectData._levelSelect._levelAudioClip.length;
        GameStatsDataContainer._gameStatsInstance.UpdateLevelComplete(level, true);
        GameStatsDataContainer._gameStatsInstance.UpdateLevelHighscore(level, songLength);

        SceneManager.LoadScene("LevelSelectScene");
    }
}
