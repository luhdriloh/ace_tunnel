using UnityEngine;

public class AudioPeer : MonoBehaviour
{
    public static float[] _samplesLeft = new float[512];
    public static float[] _samplesRight = new float[512];
    public static float[] _frequencyBands = new float[8];
    public static float[] _frequencyBandBuffer = new float[8];
    private float[] _bufferDecrease = new float[8];

    public float highestFrequencyValue;
    public float[] _maxFrequencies = new float[8];
    public static float[] _clampedFrequencyBands = new float[8];
    public static float[] _clampedFrequncyBandsBuffer = new float[8];

    public static float _amplitude, _amplitudeBuffer, _highestAmplitude;

    public static float _playbackProgressSeconds;
    private int _playbackProgress;
    private AudioSource _audioSource;

    public void StopMusic()
    {
        _playbackProgressSeconds = _audioSource.time;
        _playbackProgress = _audioSource.timeSamples;
        _audioSource.Stop();
    }

    public void StartMusic()
    {
        _audioSource.timeSamples = _playbackProgress;
        _audioSource.Play();
    }

    public bool IsMusicPlaying()
    {
        return _audioSource.isPlaying;
    }

    private void Start()
    {
        InitializeValues();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = LevelSelectData._levelSelect._levelAudioClip;
        AudioProfile(highestFrequencyValue);

        _audioSource.Play();
    }

    private void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    /// <summary>
    /// Initializes member values, for use when the scene gets reloaded
    /// </summary>
    private void InitializeValues()
    {
        _samplesLeft = new float[512];
        _samplesRight = new float[512];
        _frequencyBands = new float[8];
        _frequencyBandBuffer = new float[8];
        _bufferDecrease = new float[8];

        _maxFrequencies = new float[8];
        _clampedFrequencyBands = new float[8];
        _clampedFrequncyBandsBuffer = new float[8];

        _amplitude = 0f;
        _amplitudeBuffer = 0f;
        _highestAmplitude = 0f;
        _playbackProgressSeconds = 0f;
}

    private void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samplesLeft, 0, FFTWindow.Blackman);
        _audioSource.GetSpectrumData(_samplesRight, 1, FFTWindow.Blackman);
    }

    private void AudioProfile(float highestFrequency)
    {
        for (int i = 0; i < 8; i++)
        {
            _maxFrequencies[i] = highestFrequency;
        }
    }

    private void BandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_frequencyBands[i] > _frequencyBandBuffer[i])
            {
                _frequencyBandBuffer[i] = _frequencyBands[i];
                _bufferDecrease[i] = .0005f;
            }
            else if (_frequencyBands[i] < _frequencyBandBuffer[i])
            {
                float newValue = _frequencyBandBuffer[i] - _bufferDecrease[i];
                newValue = newValue < 0 ? 0 : newValue;

                _frequencyBandBuffer[i] = newValue;
                _bufferDecrease[i] *= 1.2f;
            }
        }
    }

    private void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_frequencyBands[i] > _maxFrequencies[i])
            {
                _maxFrequencies[i] = _frequencyBands[i];
            }

            _clampedFrequencyBands[i] = _frequencyBands[i] / _maxFrequencies[i];
            _clampedFrequncyBandsBuffer[i] = _frequencyBandBuffer[i] / _maxFrequencies[i];
        }
    }

    private void GetAmplitude()
    {
        float currentAmplitude = 0f;
        float currentAmplitudeBuffer = 0f;

        for (int i = 0; i < 8; i++)
        {
            currentAmplitude += _clampedFrequencyBands[i];
            currentAmplitudeBuffer += _clampedFrequncyBandsBuffer[i];
        }

        if (currentAmplitude > _highestAmplitude)
        {
            _highestAmplitude = currentAmplitude;
        }

        _amplitude = currentAmplitude / _highestAmplitude;
        _amplitudeBuffer = currentAmplitudeBuffer / _highestAmplitude;
    }

    private void MakeFrequencyBands()
    {
        /*
         * 20 - 60 hertz
         * 60 - 250 hertz
         * 250 - 500 hertz
         * 500 - 2000 hertz
         * 2000 - 4000 hertz
         * 4000 - 6000 hertz
         * 6000 - 20000 hertz
         */

        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            int samplesToRead = (int)Mathf.Pow(2, i + 1);
            float average = 0;

            for (int j = 0; j < samplesToRead; j++)
            {
                average += _samplesLeft[count] + _samplesRight[count];
                count++;
            }

            average /= samplesToRead;

            // find the frequency jump and the new band value average
            _frequencyBands[i] = average;
        }
    }
}
