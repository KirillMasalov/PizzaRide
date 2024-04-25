using System;
using System.Collections;
using System.Collections.Generic;
using Modifiers;
using Ride;
using Rider;
using UnityEngine;
using UnityEngine.Serialization;

namespace MotorCycle
{
    public class CollisionHandler : MonoBehaviour
    {
        private const string MODIFIER_NAME = "LightDamage";
        
        [Header("Settings")]
        [SerializeField] private float hitPhysicalSpeedThreshold;

        [Space(10)]
        [Header("Links")]
        [SerializeField] private MotorcycleConstraintsController constraintsController;

        [SerializeField] private AudioSource accidentSound;

        private MotorcycleMovementController playerMovement;

        private void Start()
        {
            var currentTransform = transform;
            while (playerMovement is null)
            {
                playerMovement = currentTransform.GetComponent<MotorcycleMovementController>();
                if(currentTransform.parent is null)
                    break;

                currentTransform = currentTransform.parent;
            }

            if (playerMovement is null)
                throw new NullReferenceException("CollisionHandler | Can't find MotorcycleMovementController parent");
        }

        private void OnCollisionEnter(Collision other)
        {
            if(playerMovement is null || other.gameObject.CompareTag("Player"))
                return;

            if (playerMovement.PhysicalCurrentSpeed.magnitude > hitPhysicalSpeedThreshold && !RideController.Instance.RideOver)
            {
                constraintsController.RiderConstraintsController.Release();
                var ragDoll = constraintsController.RiderConstraintsController.GetComponent<RagDollController>();
                ragDoll.AddImpulseForce(other.contacts[0].point - ragDoll.transform.position);

                playerMovement.ChangeSpeed(-playerMovement.CurrentSpeed);
                
                accidentSound.Play();
                var cameraTransform = Camera.main.transform;
                cameraTransform.SetParent(null, worldPositionStays: true);
                cameraTransform.GetComponent<CameraMovement>().enabled = false;
                cameraTransform.eulerAngles = playerMovement.transform.eulerAngles;

                RideController.Instance.EndRide();
            }
            else
            {
                ModifiersContainer.Instance.ActivateModifier(MODIFIER_NAME);
            }
        }
    }
}
