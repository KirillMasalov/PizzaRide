using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.Ride.Panels
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI collectedCoinsText;
        [SerializeField] private TextMeshProUGUI timeBonusText;
        [SerializeField] private TextMeshProUGUI modifierText;
        [SerializeField] private TextMeshProUGUI totalCoinsText;

        public void SetTexts(int collectedCoins, int timeBonus, float modifier, int totalCoins)
        {
            collectedCoinsText.text = collectedCoins.ToString();
            timeBonusText.text = timeBonus.ToString();
            modifierText.text = modifier.ToString("0.000");
            totalCoinsText.text = totalCoins.ToString();
        }
    }
}