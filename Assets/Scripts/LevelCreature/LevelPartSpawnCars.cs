using System;
using System.Collections;
using System.Collections.Generic;
using Cars;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelCreature
{
    public class LevelPartSpawnCars : MonoBehaviour
    {
        [SerializeField] [Range(0, 1f)] private float spawnChance;
        [SerializeField] private List<CarSpawnPoint> spawnPoints;

        public void SpawnAllCars()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                if(Random.Range(0,1f) <= spawnChance)
                    CarSpawner.Instance.SpawnCar(spawnPoint);
            }
        }
        
        
    }
}
