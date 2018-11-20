using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public Button _resumeButton;
    public Button _backToHomeScreenButton;
    
	private void Start ()
    {
        _resumeButton.onClick.AddListener(Resume);
        _backToHomeScreenButton.onClick.AddListener(BackToHomeScreen);
    }

    private void Resume()
    {
        GameController._controller.Pause();
    }

    private void BackToHomeScreen()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
