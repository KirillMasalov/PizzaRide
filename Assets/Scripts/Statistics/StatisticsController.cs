using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;

namespace Statistics
{
    public class StatisticsController : MonoBehaviour
    {
        public static StatisticsController Instance { get; private set; }

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

        public StatisticsValues GetValuesForMotorcycle(MotorcycleData data)
        {
            var maxCoins = SavesManager.Instance.LoadMaxCoinsForChosenMotorcycle(data.GetMaxCoinsSaveString);
            var maxModifier = SavesManager.Instance.LoadMaxModifierForChosenMotorcycle(data.GetMaxModifierSaveString);
            var bestTime = SavesManager.Instance.LoadBestTimeForChosenMotorcycle(data.GetBestTimeSaveString);

            return new StatisticsValues(maxCoins, maxModifier, bestTime);
        }

        public void SaveValuesForMotorcycle(MotorcycleData data, StatisticsValues values)
        {
            if(values.MaxCoins.HasValue)
                SavesManager.Instance.SaveMaxCoinsForChosenMotorcycle(data.GetMaxCoinsSaveString, values.MaxCoins.Value);
            if(values.MaxModifier.HasValue)
                SavesManager.Instance.SaveMaxModifierForChosenMotorcycle(data.GetMaxModifierSaveString, values.MaxModifier.Value);
            if(values.BestTime.HasValue)
                SavesManager.Instance.SaveBestTimeForChosenMotorcycle(data.GetBestTimeSaveString, values.BestTime.Value);
        }
    }
}