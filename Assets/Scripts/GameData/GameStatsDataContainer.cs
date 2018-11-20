using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameStatsDataContainer : MonoBehaviour
{
    public static GameStatsDataContainer _gameStatsInstance;
    private GameStatValues _gameStats;

    public float ReturnLevelHighscore(int level)
    {
        if (_gameStats._levelToHighscoreValue.ContainsKey(level))
        {
            return _gameStats._levelToHighscoreValue[level];
        }
        else
        {
            UpdateLevelHighscore(level, 0f);
            return 0f;
        }
    }

    public bool ReturnLevelComplete(int level)
    {
        if (_gameStats._levelToLevelComplete.ContainsKey(level))
        {
            return _gameStats._levelToLevelComplete[level];
        }
        else
        {
            UpdateLevelComplete(level, false);
            return false;
        }
    }


    public void UpdateLevelHighscore(int level, float newScore)
    {
        if (_gameStats._levelToHighscoreValue.ContainsKey(level))
        {
            if (newScore > _gameStats._levelToHighscoreValue[level]) {
                _gameStats._levelToHighscoreValue[level] = newScore;
            }
            else
            {
                return;
            }
        }
        else
        {
            _gameStats._levelToHighscoreValue.Add(level, newScore);
        }

        Save();
    }

    public void UpdateLevelComplete(int level, bool complete)
    {
        if (_gameStats._levelToLevelComplete.ContainsKey(level))
        {
            if (_gameStats._levelToLevelComplete[level] != complete)
            {
                _gameStats._levelToLevelComplete[level] = complete;
            }
            else
            {
                return;
            }
        }
        else
        {
            _gameStats._levelToLevelComplete.Add(level, complete);
        }

        Save();
    }

    private void Awake()
    {
        if (_gameStatsInstance == null)
        {
            DontDestroyOnLoad(this);
            _gameStatsInstance = this;
            Load();
        }
        else if (_gameStatsInstance != null)
        {
            Destroy(this);
        }
    }

    private void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(Application.persistentDataPath + "/ace_tunnel_data.dat", FileMode.Create);

        binaryFormatter.Serialize(fileStream, _gameStats);
        fileStream.Close();
    }

    private void Load ()
    {
        if (File.Exists(Application.persistentDataPath + "/ace_tunnel_data.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + "/ace_tunnel_data.dat", FileMode.Open);
            _gameStats = (GameStatValues)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }
        else
        {
            // create data for just one level
            _gameStats = new GameStatValues();
            _gameStats._levelToHighscoreValue = new System.Collections.Generic.Dictionary<int, float>();
            _gameStats._levelToHighscoreValue.Add(1, 0);

            _gameStats._levelToLevelComplete = new System.Collections.Generic.Dictionary<int, bool>();
            _gameStats._levelToLevelComplete.Add(1, false);

            Save();
        }
    }
}

