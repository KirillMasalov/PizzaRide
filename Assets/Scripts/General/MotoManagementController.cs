using System;
using System.Collections;
using System.Collections.Generic;
using Garage;
using UnityEngine;

namespace General
{
    public class MotoManagementController : MonoBehaviour
    {
        public static MotoManagementController Instance { get; private set; }
        
        private HashSet<string> ownedMotorcycles = new HashSet<string>(){"vespa"};

        [field: SerializeField] public MotorcycleData CurrentMotorcycle { get; private set; }

        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                LoadData();
            }
            else
            {
                Destroy(this);
            }
        }

        private void LoadData()
        {
            LoadOwnedMotorcycles();
            LoadChosenMotorcycle();
        }

        private void LoadOwnedMotorcycles()
        {
            var motorcyclesList = SavesManager.Instance.LoadOwnedMotorcycles();
            if (motorcyclesList is not null)
                ownedMotorcycles = new HashSet<string>(motorcyclesList);
        }

        private void LoadChosenMotorcycle()
        {
            var chosenMotorcycleName = SavesManager.Instance.LoadChosenMotorcycleName();
            if (chosenMotorcycleName is not null)
            {
                var motorcycleData = GarageController.GetMotoDataByLowerName(chosenMotorcycleName);

                if (motorcycleData is not null)
                    CurrentMotorcycle = motorcycleData;
            }
            else
                SavesManager.Instance.SaveChosenMotorcycle(CurrentMotorcycle.Name.ToLower());
        }

        public bool PlayerHasMotorcycle(string motorcycleName)
        {
            return ownedMotorcycles.Contains(motorcycleName.ToLower());
        }

        public void SetCurrentMotorcycle(MotorcycleData data)
        {
            CurrentMotorcycle = data;
            SavesManager.Instance.SaveChosenMotorcycle(data.Name.ToLower());
        }

        public bool BuyMotorcycle(MotorcycleData data)
        {
            if (GameController.Instance.CoinsCount >= data.Cost)
            {
                ownedMotorcycles.Add(data.Name.ToLower());
                GameController.Instance.ChangeCoins(-data.Cost);
                SavesManager.Instance.SaveOwnedMotorcycles(ownedMotorcycles);
                SavesManager.Instance.SaveChosenMotorcycle(data.Name.ToLower());
                CurrentMotorcycle = data;
                return true;
            }
            return false;
        }
    }
}