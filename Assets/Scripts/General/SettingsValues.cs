using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class SettingsValues
    {
        public float MainMusicValue { get; private set; }
        public float SFXValue { get; private set; }
        public float BackGroundMusicValue { get; private set; }

        public SettingsValues(float mainMusicValue, float sfxValue, float backgroundMusicValue)
        {
            MainMusicValue = mainMusicValue;
            SFXValue = sfxValue;
            BackGroundMusicValue = backgroundMusicValue;
        }
    }
}