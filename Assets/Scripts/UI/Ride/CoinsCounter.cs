using System;
using System.Collections;
using System.Collections.Generic;
using Ride;
using TMPro;
using UnityEngine;

namespace UI.Ride
{
    public class CoinsCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counter;

        private void Start()
        {
            RideController.Instance.coinsCountChanged.AddListener(RedrawCoinsCount);
        }

        private void RedrawCoinsCount(int newCount)
        {
            counter.text = newCount.ToString();
        }
    }
}
