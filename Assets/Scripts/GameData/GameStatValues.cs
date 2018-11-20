using System;
using System.Collections.Generic;

[Serializable]
public class GameStatValues
{
    public Dictionary<int, bool> _levelToLevelComplete;
    public Dictionary<int, float> _levelToHighscoreValue;
}
