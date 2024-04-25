using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.Ride.Panels
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI collectedCoinsText;
        [SerializeField] private TextMeshProUGUI penaltyText;
        [SerializeField] private TextMeshProUGUI totalCoinsText;

        public void SetTexts(int collectedCoins, int penalty, int totalCoins)
        {
            collectedCoinsText.text = collectedCoins.ToString();
            penaltyText.text = penalty.ToString();
            totalCoinsText.text = totalCoins.ToString();
        }
    }
}