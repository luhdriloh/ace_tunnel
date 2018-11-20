using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

[Serializable]
public class GameStatsData
{
    public Dictionary<int, float> _levelToHighscoreValue;
    public int _currentLevel;
}
