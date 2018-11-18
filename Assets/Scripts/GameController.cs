using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController _controller;

    private bool _isPaused;
    private bool _gameover;
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_gameover == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void Pause()
    {
        if (_isPaused == true)
        {
            Time.timeScale = 1;
            _isPaused = false;
            _audioPeer.StartMusic();
        }
        else
        {
            Time.timeScale = 0;
            _isPaused = true;
            _audioPeer.StopMusic();
        }
    }

    public void GameOver()
    {
        _gameover = true;
        Time.timeScale = 0;
    }
}
