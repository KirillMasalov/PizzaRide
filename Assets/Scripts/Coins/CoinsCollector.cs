using System;
using System.Collections;
using System.Collections.Generic;
using Ride;
using UnityEngine;

namespace Coins
{
    public class CoinsCollector : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                var coin = other.transform.parent.GetComponent<Coin>();
                coin.RemoveFromSpawnPoint();
                audioSource.Play();
                CoinsSpawner.Instance.ReturnCoinToPool(coin);
                RideController.Instance.CoinsCount++;
                
            }
        }
    }
}
