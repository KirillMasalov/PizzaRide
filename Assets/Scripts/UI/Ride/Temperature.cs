using System;
using System.Collections;
using System.Collections.Generic;
using Ride;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Ride
{
    public class Temperature : MonoBehaviour
    {
        [FormerlySerializedAs("timer")]
        [Header("Links")]
        [SerializeField] private RideTimer rideTimer;
        [SerializeField] private RectTransform fillBox;
        [SerializeField] private RectTransform fillCircle;

        [Space(10)] 
        [Header("Settings")] 
        [SerializeField] [Range(0, 1f)] private float updateFrequency; 
        [SerializeField] private List<float> thresholds;
        [SerializeField] private List<Color> colors;

        private int currentThresholdIndex = 0;
        private Image boxImage;
        private Image circleImage;
        private float previousRatio = 0;
        
        private void Awake()
        {
            thresholds.Sort();
            boxImage = fillBox.GetComponent<Image>();
            circleImage = fillCircle.GetComponent<Image>();
        }

        private void Update()
        {
            var ratio = rideTimer.TimeFromStart / rideTimer.LimitSeconds;
            if (ratio - previousRatio >= updateFrequency)
            {
                previousRatio = ratio;
                fillBox.localScale = new Vector3(1, Mathf.Max(1 - ratio, 0), 1f);
                CheckThreshold();
            }
        }

        private void CheckThreshold()
        {
            if (currentThresholdIndex < thresholds.Count && 
                rideTimer.TimeFromStart / rideTimer.LimitSeconds > thresholds[currentThresholdIndex])
            {
                if(currentThresholdIndex < colors.Count)
                    ChangeColor(colors[currentThresholdIndex]);
                currentThresholdIndex++;
            }
        }

        private void ChangeColor(Color newColor)
        {
            boxImage.color = newColor;
            circleImage.color = newColor;
        }
    }
}
