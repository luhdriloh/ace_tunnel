using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectSceneManager : MonoBehaviour
{
    public Button _previousLevelButton;
    public Button _nextLevelButton;

    public List<GameObject> _levelObjects;
    public int _selectedLevel;

    private void Awake()
    {
        _selectedLevel = 0;
        TurnLevelOn();
    }

    private void Start()
    {
        _previousLevelButton.onClick.AddListener(PreviousLevelButtonPressed);
        _nextLevelButton.onClick.AddListener(NextLevelButtonPressed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousLevelButtonPressed();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextLevelButtonPressed();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            _levelObjects[_selectedLevel].GetComponent<LevelSelectButton>().PlayGame();
        }
    }

    private void NextLevelButtonPressed()
    {
        _selectedLevel = (_selectedLevel + 1) % _levelObjects.Count;
        TurnLevelOn();
    }

    private void PreviousLevelButtonPressed()
    {
        _selectedLevel = (_selectedLevel + _levelObjects.Count - 1) % _levelObjects.Count;
        TurnLevelOn();
    }

    private void TurnLevelOn()
    {
        for (int i = 0; i < _levelObjects.Count; i++)
        {
            if (i == _selectedLevel)
            {
                _levelObjects[i].gameObject.SetActive(true);
            }
            else
            {
                _levelObjects[i].gameObject.SetActive(false);
            }
        }
    }
}
