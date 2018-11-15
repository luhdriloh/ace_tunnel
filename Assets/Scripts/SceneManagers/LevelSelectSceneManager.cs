using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSelectSceneManager : MonoBehaviour
{
    public Button _levelOne;

    // Use this for initialization
    private void Start()
    {
        _levelOne.onClick.AddListener(PlayGame);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("GamePlayScene");
    }

}
