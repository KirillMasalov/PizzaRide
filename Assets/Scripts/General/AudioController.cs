using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

namespace General
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController Instance;
        
        [SerializeField] private AudioMixerGroup mixer;
        [SerializeField] private float zeroThreshold = 0.01f;
        [SerializeField] private string mainMusicVarName;
        [SerializeField] private string backgroundMusicVarName;
        [SerializeField] private string sfxVarName;
        
        [Space(10)]
        [Header("MinValues")]
        [SerializeField] private float mainMusicMinValue;
        [SerializeField] private float backgroundMusicMinValue;
        [SerializeField] private float sfxMinvalue;
        
        [Space(10)]
        [Header("MaxValues")]
        [SerializeField] private float mainMusicMaxValue;
        [SerializeField] private float backgroundMusicMaxValue;
        [SerializeField] private float sfxMaxvalue;

        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
                Destroy(this);
        }

        public void SetMainMusicValue(float value)
        {
            var calculatedValue = (value < zeroThreshold) ? -80
                : mainMusicMinValue + value
                * (mainMusicMaxValue - mainMusicMinValue);
            mixer.audioMixer.SetFloat(mainMusicVarName, calculatedValue);
        }

        public void SetBackgroundMusicValue(float value)
        {
            var calculatedValue = (value < zeroThreshold) ? -80
                : backgroundMusicMinValue + value
                * (backgroundMusicMaxValue - backgroundMusicMinValue);
            mixer.audioMixer.SetFloat(backgroundMusicVarName,  calculatedValue);
        }

        public void SetSFXValue(float value)
        {
            var calculatedValue = (value < zeroThreshold) ? -80
                : sfxMinvalue + value
                * (sfxMaxvalue - sfxMinvalue);
            mixer.audioMixer.SetFloat(sfxVarName, calculatedValue);
        }
    }
}