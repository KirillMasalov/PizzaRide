using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coins
{
    public class CoinsSpawnPoint : MonoBehaviour
    {
        [SerializeField] private int coinsCount;
        [SerializeField] private float coinsGap;
        private HashSet<Coin> spawnedCoins = new HashSet<Coin>();

        public void SpawnCoins()
        {
            var spawnPosition = transform.position;
            for (var i = 0; i < coinsCount; i++)
            {
                var coin = CoinsSpawner.Instance.SpawnCoin(spawnPosition);
                coin.CurrentSpawnPoint = this;
                spawnedCoins.Add(coin);
                spawnPosition += transform.forward * coinsGap;
            }
        }

        public void RemoveCoin(Coin coin)
        {
            spawnedCoins.Remove(coin);
        }

        public void ReturnRemainingCoins()
        {
            foreach (var coin in spawnedCoins)
            {
                coin.CurrentSpawnPoint = null;
                CoinsSpawner.Instance.ReturnCoinToPool(coin);
            }
            spawnedCoins.Clear();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}
