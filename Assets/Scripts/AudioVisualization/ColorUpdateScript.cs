using UnityEngine;

public class ColorUpdateScript : MonoBehaviour
{
    public Color _baseColor;
    public float _lowestColorValue, _highestColorValue;

    private Camera _camera;
    private float _h;
    private float _s;
    private float _v;

	private void Start ()
    {
        _camera = Camera.main;
        Color.RGBToHSV(_baseColor, out _h, out _s, out _v);
	}
	
	private void Update ()
    {
        // lets find a new _v value

        _v = (AudioPeer._clampedFrequncyBandsBuffer[1] * (_highestColorValue - _lowestColorValue) + _lowestColorValue) / 100;
        _camera.backgroundColor = Color.HSVToRGB(_h, _s, _v);
	}
}
