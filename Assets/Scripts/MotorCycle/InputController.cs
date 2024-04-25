using System.Collections;
using System.Collections.Generic;
using Ride;
using UI.Ride;
using UnityEngine;
using UnityEngine.Serialization;

namespace MotorCycle
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private MotorcycleMovementController movementController;
        void Update()
        {
            if (!RideController.Instance.Pause && !RideController.Instance.RideOver && !RideController.Instance.IsTutorial)
            {
                movementController.ChangeSpeed(Input.GetAxis("Vertical"));
                movementController.ChangeDirection(Input.GetAxis("Horizontal"));
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (RideController.Instance.Pause)
                {
                    RideController.Instance.SetPause(false);
                }
                else
                {
                    RideController.Instance.SetPause(true);
                }
            }
        }
    }
}
