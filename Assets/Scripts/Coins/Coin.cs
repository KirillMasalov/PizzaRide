using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coins
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        public CoinsSpawnPoint CurrentSpawnPoint { get; set; }

        public void RemoveFromSpawnPoint()
        {
            CurrentSpawnPoint?.RemoveCoin(this);
        }

        private void FixedUpdate()
        {
            transform.Rotate(Vector3.up, rotationSpeed);
        }
    }
}
