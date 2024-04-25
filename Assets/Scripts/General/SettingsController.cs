using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class SettingsController : MonoBehaviour
    {
        public static SettingsController Instance { get; private set; }
        public SettingsValues CurrentValue { get; private set; }

        [Header("Sound")]
        [SerializeField] private Slider mainVolumeSlider;
        [SerializeField] private Slider backgroundSlider;
        [SerializeField] private Slider sfxSlider;

        private void Awake()
        {
            Instance = this;
        }

        public void Initialize()
        {
            CurrentValue = SavesManager.Instance.LoadSettingsValues();
            
            mainVolumeSlider.onValueChanged.AddListener(AudioController.Instance.SetMainMusicValue);
            sfxSlider.onValueChanged.AddListener(AudioController.Instance.SetSFXValue);
            backgroundSlider.onValueChanged.AddListener(AudioController.Instance.SetBackgroundMusicValue);
            
            mainVolumeSlider.value = CurrentValue.MainMusicValue;
            sfxSlider.value = CurrentValue.SFXValue;
            backgroundSlider.value = CurrentValue.BackGroundMusicValue;
            
            SetValuesInMixer(CurrentValue);
        }

        public void Cancel()
        {
            mainVolumeSlider.value = CurrentValue.MainMusicValue;
            sfxSlider.value = CurrentValue.SFXValue;
            backgroundSlider.value = CurrentValue.BackGroundMusicValue;
            
            SetValuesInMixer(CurrentValue);
        }

        public void Confirm()
        {
            CurrentValue = new SettingsValues(mainVolumeSlider.value, sfxSlider.value, backgroundSlider.value);
            SavesManager.Instance.SaveSettingsValues(CurrentValue);
        }

        private void SetValuesInMixer(SettingsValues values)
        {
            AudioController.Instance.SetMainMusicValue(values.MainMusicValue);
            AudioController.Instance.SetBackgroundMusicValue(values.BackGroundMusicValue);
            AudioController.Instance.SetSFXValue(values.SFXValue);
        }
    }
}