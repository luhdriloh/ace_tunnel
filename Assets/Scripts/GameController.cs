using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController controller;

    private bool isPaused;
    private bool gameover;

    private void Awake()
    {
        if (controller == null)
        {
            controller = this;
        }
        else if (controller != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
        gameover = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameover == false)
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (gameover == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void Pause()
    {
        if (isPaused == true)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void GameOver()
    {
        gameover = true;
        Time.timeScale = 0;
    }
}
