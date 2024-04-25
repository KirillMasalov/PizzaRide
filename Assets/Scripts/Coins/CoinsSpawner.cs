using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coins
{
    public class CoinsSpawner : MonoBehaviour
    {
        public static CoinsSpawner Instance { get; private set; }

        [SerializeField] private int coinsPoolSize;
        [SerializeField] private GameObject coinPrefab;
        private Queue<Coin> coinsPool;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            coinsPool = new Queue<Coin>();
            for (var i = 0; i < coinsPoolSize; i++)
            {
                CreateNewCoin();
            }
        }

        public Coin SpawnCoin(Vector3 pos)
        {
            if(coinsPool.Count <= 0)
                CreateNewCoin();

            var newCoin = coinsPool.Dequeue();
            var newCoinTransform = newCoin.transform;
            newCoinTransform.gameObject.SetActive(true);

            newCoinTransform.position = pos;
            return newCoin;
        }

        private void CreateNewCoin()
        {
            var newCoinTransform = Instantiate(coinPrefab).transform;
            var coin = newCoinTransform.GetComponent<Coin>();

            if (coin is null)
                throw new NullReferenceException("CoinsSpawner | Coin prefab doesn't contain Coin component");
            newCoinTransform.parent = transform;
            coinsPool.Enqueue(coin);

            newCoinTransform.gameObject.SetActive(false);
        }

        public void ReturnCoinToPool(Coin coin)
        {
            coinsPool.Enqueue(coin);
            coin.gameObject.SetActive(false);
        }
    }
}
