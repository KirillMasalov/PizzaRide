using System;
using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;

namespace MotorCycle
{
    public class MotorSound : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float minPitch;
        [SerializeField] private float maxPitch;

        
        private AudioSource audioSource;
        private MotorcycleData data;
        private MotorcycleMovementController motorcycleMovement;
        private float pitchRatio;

        private bool initialized;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            data = GetComponent<MotorcycleDataContainer>().Data;
        }

        public void Initialize(MotorcycleMovementController controller)
        {
            motorcycleMovement = controller;
            pitchRatio = (maxPitch - minPitch) / data.MaxSpeed;

            initialized = true;
        }

        private void Update()
        {
            if (!initialized)
                return;
            
            audioSource.pitch = minPitch + motorcycleMovement.CurrentSpeed * pitchRatio;
        }
    }
}