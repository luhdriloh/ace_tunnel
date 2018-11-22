using UnityEngine;

public class ColorUpdateScript : MonoBehaviour
{
    private float _lowestColorValue;

    private Camera _camera;
    private float _h;
    private float _h2;
    private float _s;
    private float _v;

	private void Start ()
    {
        _camera = Camera.main;
        Color.RGBToHSV(LevelSelectData._levelSelect._levelColor, out _h, out _s, out _v);
        _lowestColorValue = _v * 100;
	}
	
	private void Update ()
    {
        _v = (AudioPeer._clampedFrequncyBandsBuffer[1] * (GameConstants.highestColorvalue - _lowestColorValue) + _lowestColorValue) / 100;
        _camera.backgroundColor = Color.HSVToRGB(_h, _s, _v);
	}
}
