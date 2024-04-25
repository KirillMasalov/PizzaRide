using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace General
{
    public class SavesManager : MonoBehaviour
    {
        private const string COINSCOUNT_KEY = "CoinsCount";
        private const string OWNED_MOTORCYCLES_KEY = "OwnedMotorCycles";
        private const string CHOSEN_MOTORCYCLE_KEY = "ChosenMotorcycle";
        private const string WAS_TUTORIAL_KEY = "WAS_TUTORIAL";
        
        private const string MAINMUSIC_KEY = "MainMusicValue";
        private const string SFX_KEY = "SFXValue";
        private const string BGMUSIC_KEY = "BackgroundMusicValue";
        
        public static SavesManager Instance;
        
        //cache
        private string ownedMotorcycles;
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

        public bool WasTutorial()
        {
            return PlayerPrefs.HasKey(WAS_TUTORIAL_KEY);
        }

        public void SetWasTutorial()
        {
            PlayerPrefs.SetInt(WAS_TUTORIAL_KEY, 1);
        }

        public void SaveCoins(int coinsCount)
        {
            PlayerPrefs.SetInt(COINSCOUNT_KEY, coinsCount);
        }

        public int LoadCoins()
        {
            var result = 0;
            if (PlayerPrefs.HasKey(COINSCOUNT_KEY))
                result = PlayerPrefs.GetInt(COINSCOUNT_KEY);

            return result;
        }

        public void SaveSettingsValues(SettingsValues values)
        {
            PlayerPrefs.SetFloat(MAINMUSIC_KEY, values.MainMusicValue);
            PlayerPrefs.SetFloat(SFX_KEY, values.SFXValue);
            PlayerPrefs.SetFloat(BGMUSIC_KEY, values.BackGroundMusicValue);
        }

        public SettingsValues LoadSettingsValues()
        {
            var mainMusic = PlayerPrefsExtensions.GetFloatOrDefault(MAINMUSIC_KEY, 1);
            var sfxMusic = PlayerPrefsExtensions.GetFloatOrDefault(SFX_KEY, 1);
            var bgMusic = PlayerPrefsExtensions.GetFloatOrDefault(BGMUSIC_KEY, 1);

            return new SettingsValues(mainMusic, sfxMusic, bgMusic);
        }

        public List<string> LoadOwnedMotorcycles()
        {
            if (PlayerPrefs.HasKey(OWNED_MOTORCYCLES_KEY))
            {
                ownedMotorcycles ??= PlayerPrefs.GetString(OWNED_MOTORCYCLES_KEY);
                return ownedMotorcycles.Split(";").ToList();
            }

            return null;
        }

        public void SaveOwnedMotorcycles(IEnumerable<string> motorcyclesNames)
        {
            ownedMotorcycles = string.Join(';',motorcyclesNames.Select(n => n.ToLower()));
            PlayerPrefs.SetString(OWNED_MOTORCYCLES_KEY, ownedMotorcycles);
        }

        public void SaveChosenMotorcycle(string motorcycleName)
        {
            PlayerPrefs.SetString(CHOSEN_MOTORCYCLE_KEY, motorcycleName.ToLower());
        }

        public string LoadChosenMotorcycleName()
        {
            return PlayerPrefs.HasKey(CHOSEN_MOTORCYCLE_KEY) ? PlayerPrefs.GetString(CHOSEN_MOTORCYCLE_KEY) : null;
        }

        public int? LoadMaxCoinsForChosenMotorcycle(string key)
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : null;
        }
        
        public float? LoadMaxModifierForChosenMotorcycle(string key)
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : null;
        }
        
        public float? LoadBestTimeForChosenMotorcycle(string key)
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : null;
        }
        
        
        public void SaveMaxCoinsForChosenMotorcycle(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
        
        public void SaveMaxModifierForChosenMotorcycle(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
        
        public void SaveBestTimeForChosenMotorcycle(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
    }
}