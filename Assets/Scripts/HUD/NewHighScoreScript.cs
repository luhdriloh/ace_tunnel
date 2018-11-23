using UnityEngine;
using UnityEngine.UI;

public class NewHighScoreScript : MonoBehaviour
{
    public Text _oldHighScore;
    public Text _newHighScore;

    public void SetScores(float oldScore, float newScore)
    {
        _oldHighScore.text = Utils.ReturnTimeStringFromFloat(oldScore);
        _newHighScore.text = Utils.ReturnTimeStringFromFloat(newScore);
    }
}
