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
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

