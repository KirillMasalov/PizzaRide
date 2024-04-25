using System.Collections;
using System.Collections.Generic;
using Coins;
using UnityEngine;

namespace LevelCreature
{
    public class LevelPartCoinsSpawn : MonoBehaviour
    {
        [SerializeField] [Range(0, 1f)] private float spawnChance = 0.5f;
        [SerializeField] private List<CoinsSpawnPoint> spawnPoints;

        public void SpawnAllCoins()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                if (Random.Range(0, 1f) < spawnChance)
                {
                    spawnPoint.SpawnCoins();
                }
            }
        }

        public void ReturnCoinsToPool()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                spawnPoint.ReturnRemainingCoins();
            }
        }
    }
}
