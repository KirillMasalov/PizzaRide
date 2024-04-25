using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Statistics
{
    public class StatisticsValues
    {
        public int? MaxCoins { get; set; }
        public float? MaxModifier { get; set; }
        public float? BestTime { get; set; }

        public StatisticsValues() { }

        public StatisticsValues(int? maxCoins, float? maxModifier, float? bestTime)
        {
            MaxCoins = maxCoins;
            MaxModifier = maxModifier;
            BestTime = bestTime;
        }
    }
}