using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ride
{
    public class RideTimer : MonoBehaviour
    {
        [field: SerializeField] public float LimitSeconds { get; private set; }
        [SerializeField] private float tickPeriod;
        public float TimeFromStart { get; private set; }
        public UnityEvent tick;
        
        private float tickBuffer;

        private void Update()
        {
            if (!RideController.Instance.Pause && !RideController.Instance.RideOver)
            {
                var delta = Time.deltaTime;
                TimeFromStart += delta;
                tickBuffer += delta;
                if (tickBuffer > tickPeriod)
                {
                    tickBuffer %= tickPeriod;
                    tick?.Invoke();
                }
            }
        }

        public float RemainingSeconds()
        {
            return LimitSeconds - TimeFromStart;
        }
    }
}
