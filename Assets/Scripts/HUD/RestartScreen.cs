using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartScreen : MonoBehaviour
{
    public Button _restartButton;
    public Button _backToHomeScreenButton;

    private void Start()
    {
        _restartButton.onClick.AddListener(Restart);
        _backToHomeScreenButton.onClick.AddListener(BackToHomeScreen);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void BackToHomeScreen()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}

