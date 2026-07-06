using UnityEngine;
using UnityEngine.Audio;

namespace Game.Library
{

    public class Audio
    {
        AudioMixer _audioMixer;

        private readonly string BGM = "BGM";
        private readonly string SE = "SE";

        float ConvertVolume2dB(float volume) => Mathf.Clamp(20f * Mathf.Log10(Mathf.Clamp(volume, 0f, 1f)), -80f, 0f);

        public void ChangeBGMVaolume(float value)
        {
            _audioMixer.SetFloat(BGM, ConvertVolume2dB(value));
        }

        public void ChangeSEVaolume(float value)
        {
            _audioMixer.SetFloat(SE, ConvertVolume2dB(value));
        }
    }
}