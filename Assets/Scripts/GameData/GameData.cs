using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public Dictionary<int, float> _levelToHighscoreValue;
    public int _currentLevel;
}
