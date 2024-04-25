using System.Collections;
using System.Collections.Generic;
using MotorCycle;
using Ride;
using UnityEngine;

namespace UI.Ride
{
    public class Speedometer : MonoBehaviour
    {
        [Header("Links")] [SerializeField] private RectTransform arrow;
        [SerializeField] private MotorcycleMovementController movementController;

        [Space(10)] 
        [Header("Settings")]
        [SerializeField] private float offset;
        [SerializeField] private float ratio;

        private void Update()
        {
            if (!RideController.Instance.Pause)
            {
                arrow.localEulerAngles = new Vector3(0,0, offset - Mathf.Abs(movementController.CurrentSpeed) * ratio);
            }
        }
    }
}
