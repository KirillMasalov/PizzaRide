using System;
using System.Collections;
using System.Collections.Generic;
using General;
using Modifiers;
using MotorCycle;
using Rider;
using Statistics;
using UI.Ride;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Ride
{
    public class RideController : MonoBehaviour
    {
        public static RideController Instance { get; private set; }
        [field:SerializeField]public RideTimer RideTimer { get; private set; }

        [Header("Settings")]
        [SerializeField] private float coinsForRemainingSecond;
        [SerializeField] [Range(0,1f)] private float penaltyRatio;
        [SerializeField] [Range(0,0.5f)] private float deliverLowRate;
        
        [Space(10)]
        [Header("Links")]
        [SerializeField] private Transform rider;
        [SerializeField] private Transform motorcycleAnchor;
        [SerializeField] private MotorcycleMovementController movementController;
        [SerializeField] private AudioSource winAudio;

        public bool RideOver { get; private set; }
        public bool IsTutorial { get; private set; }
        public bool OrderDelivered { get; private set; }
        public bool Pause { get; private set; }

        private int coinsCount;
        public int CoinsCount
        {
            get => coinsCount;
            set
            {
                coinsCount = value;
                coinsCountChanged?.Invoke(value);
            }
        }

        public UnityEvent<int> coinsCountChanged;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            var motorcycleData = MotoManagementController.Instance.CurrentMotorcycle;
            var motorcycleTransform = Instantiate(motorcycleData.Prefab).transform;
            
            motorcycleTransform.SetParent(motorcycleAnchor.parent);
            motorcycleTransform.localPosition = motorcycleAnchor.localPosition;

            var mcConstraint = motorcycleTransform.GetComponent<MotorcycleConstraintsController>();
            var rdConstraint = rider.GetComponent<RiderConstraintsController>();
            mcConstraint.Initialize(rdConstraint);
            rdConstraint.Initialize(mcConstraint); 
            
            movementController.Initialize(motorcycleData);
        }
        

        public void SetPause(bool isPause)
        {
            Time.timeScale = isPause ? 0 : 1f;
            Pause = isPause;
            
            if(Pause)
                UIController.Instance.OpenPauseMenu();
            else
                UIController.Instance.ClosePauseMenu();
        }

        public void DeliverOrder()
        {
            if (!RideOver)
            {
                RideOver = true;
                
                winAudio.Play();
                var timeBonus = (int)(RideTimer.RemainingSeconds() * coinsForRemainingSecond);
                var earnCoins = (int)((coinsCount + timeBonus) * ModifiersController.Instance.CurrentValue);
                UIController.Instance.ShowWinPanel(coinsCount,
                    timeBonus, ModifiersController.Instance.CurrentValue, earnCoins);
                
                movementController.SetLowSpeed(deliverLowRate);
                movementController.ZeroSlant();
                GameController.Instance.ChangeCoins(earnCoins);
                ChangeStatistics();
            }
        }

        private void ChangeStatistics()
        {
            var currentMotorcycle = MotoManagementController.Instance.CurrentMotorcycle;
            var currentStatistics =
                StatisticsController.Instance.GetValuesForMotorcycle(currentMotorcycle);

            var newStatistics = new StatisticsValues();

            if (!currentStatistics.MaxCoins.HasValue || currentStatistics.MaxCoins.Value < coinsCount)
                newStatistics.MaxCoins = coinsCount;
            
            if (!currentStatistics.MaxModifier.HasValue || currentStatistics.MaxModifier.Value < ModifiersController.Instance.CurrentValue)
                newStatistics.MaxModifier = ModifiersController.Instance.CurrentValue;
            
            if (!currentStatistics.BestTime.HasValue || currentStatistics.BestTime.Value > RideTimer.TimeFromStart)
                newStatistics.BestTime = RideTimer.TimeFromStart;
            
            StatisticsController.Instance.SaveValuesForMotorcycle(currentMotorcycle, newStatistics);
        }

        public void StartTutorial()
        {
            IsTutorial = true;
        }
        
        public void EndTutorial()
        {
            IsTutorial = false;
        }

        public void EndRide()
        {
            if (!RideOver)
            {
                RideOver = true;
                var penalty = (int)(coinsCount * penaltyRatio);
                var earnCoins = coinsCount - penalty;
                
                UIController.Instance.ShowLosePanel(coinsCount, penalty, earnCoins);
                
                GameController.Instance.ChangeCoins(earnCoins);
            }
        }

        public void QuitFromRide()
        {
            SceneTransition.Instance.LoadMainMenu();
        }
    }
}