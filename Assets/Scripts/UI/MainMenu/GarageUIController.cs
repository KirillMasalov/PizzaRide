using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Garage;
using General;
using Statistics;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.MainMenu
{
    public class GarageUIController : MonoBehaviour
    {
        private const string NO_STATISTICS_VALUE_STRING = "--";
        [SerializeField] private GarageController garageController;
        [SerializeField] private TextMeshProUGUI coinsCountText;
        [SerializeField] private GameObject buyButton;
        [SerializeField] private GameObject choseButton;
        [SerializeField] private GameObject chosenLabel;
        [SerializeField] private GameObject noMoneyLabel;
        [SerializeField] private GameObject colorPicker;
            
        [Space(10)]
        [Header("Current motorcycle")]
        [SerializeField] private TextMeshProUGUI currentMotorcycleNameText;
        [SerializeField] private TextMeshProUGUI currentMotorcycleCostText;
        [SerializeField] private TextMeshProUGUI currentMotorcycleMaxSpeedText;
        
        [Space(10)]
        [Header("Statistics")]
        [SerializeField] private TextMeshProUGUI currentMotorcycleCoinsText;
        [SerializeField] private TextMeshProUGUI currentMotorcycleModifierText;
        [SerializeField] private TextMeshProUGUI currentMotorcycleTimeText;

        private Dictionary<string, StatisticsValues> motorcyclesStatistics;

        private void Start()
        {
            motorcyclesStatistics = new Dictionary<string, StatisticsValues>();
            garageController.openGarage.AddListener(OnOpenGarage);
            garageController.changeCurrentMotorcycle.AddListener(OnChangedMotorcycle);
            garageController.buyMotorcycle.AddListener(OnBuyMotorcycle);
        }

        private void OnChangedMotorcycle(MotorcycleData data)
        {
            var isOwned = MotoManagementController.Instance.PlayerHasMotorcycle(data.Name);
            var isChosen = MotoManagementController.Instance.CurrentMotorcycle == data;
            if (isOwned)
            {
                buyButton.SetActive(false);
                noMoneyLabel.SetActive(false);
                colorPicker.SetActive(true);
                if (isChosen)
                {
                    chosenLabel.SetActive(true);
                    choseButton.SetActive(false);
                }
                else
                {
                    chosenLabel.SetActive(false);
                    choseButton.SetActive(true);
                }
            }
            else
            {
                chosenLabel.SetActive(false);
                choseButton.SetActive(false);
                colorPicker.SetActive(false);
                
                if (GameController.Instance.CoinsCount >= data.Cost)
                {
                    buyButton.SetActive(true);
                    noMoneyLabel.SetActive(false);
                }
                else
                {
                    buyButton.SetActive(false);
                    noMoneyLabel.SetActive(true);
                }
            }
            

            currentMotorcycleNameText.text = data.Name;
            currentMotorcycleCostText.text = data.Cost.ToString();
            currentMotorcycleMaxSpeedText.text = data.MaxSpeed.ToString();

            if (!motorcyclesStatistics.ContainsKey(data.Name))
                motorcyclesStatistics[data.Name] = StatisticsController.Instance.GetValuesForMotorcycle(data);

            var currentStatistics = motorcyclesStatistics[data.Name];

            currentMotorcycleCoinsText.text = currentStatistics.MaxCoins.HasValue 
                ? currentStatistics.MaxCoins.Value.ToString() 
                : NO_STATISTICS_VALUE_STRING;

            currentMotorcycleModifierText.text = currentStatistics.MaxModifier.HasValue 
                ? currentStatistics.MaxModifier.Value.ToString("0.000") 
                : NO_STATISTICS_VALUE_STRING;

            currentMotorcycleTimeText.text = currentStatistics.BestTime.HasValue 
                ? currentStatistics.BestTime.Value.ToString("F") 
                : NO_STATISTICS_VALUE_STRING;
        }

        private void OnOpenGarage()
        {
            coinsCountText.text = GameController.Instance.CoinsCount.ToString();
        }

        private void OnBuyMotorcycle()
        {
            coinsCountText.text = GameController.Instance.CoinsCount.ToString();
            chosenLabel.SetActive(true);
            buyButton.SetActive(false);
            colorPicker.SetActive(true);
        }
    }
}