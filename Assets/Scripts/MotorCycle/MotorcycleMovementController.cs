using System;
using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;
using UnityEngine.Serialization;


namespace MotorCycle
{
    public class MotorcycleMovementController : MonoBehaviour
    {
        [Header("Speed")] [SerializeField] private float maxSpeed;
        [SerializeField] private float reverseMaxSpeed;
        [SerializeField] [Range(0, 3f)] private float acceleration;
        [field: SerializeField] public float BreakDeceleration { get; private set; }
        [SerializeField] [Range(0, 3f)] private float deceleration;
        [SerializeField] [Range(0, 0.5f)] private float accelerationHelp = 0.01f;
        
        [Space(10)] 
        [Header("Turn")] 
        [SerializeField] [Range(0.1f, 200f)] private float maxRotateSpeed;

        [SerializeField] [Range(0.1f, 10f)] private float rotateAcceleration;
        [SerializeField] [Range(0f, 1f)] private float decelerationRate;
        [SerializeField] private float turnThreshold;

        [Space(10)] 
        [Header("Slant")] 
        [SerializeField] [Range(0, 90f)] private float maxSlantAngle;

        [Space(10)] 
        [Header("Links")] 
        [SerializeField] private Rigidbody motorcycle;
        [SerializeField] private CameraMovement cameraMovement;
        
        private float currentRotateSpeed;
        private float yRotation;
        private float zRotation;

        private Transform tr;
        
        public float CurrentSpeed { get; private set; }
        public Vector3 PhysicalCurrentSpeed => motorcycle.velocity;
        
        private void Awake()
        {
            tr = transform;
            yRotation = tr.eulerAngles.y;

            if (motorcycle is null)
                throw new NullReferenceException("MotorcycleMovement | Rigidbody is null");
            if (cameraMovement is null)
                throw new NullReferenceException("MotorcycleMovement | CameraMovement is null");
        }

        private void FixedUpdate()
        {
            motorcycle.velocity = new Vector3(tr.forward.x * CurrentSpeed, motorcycle.velocity.y,
                tr.forward.z * CurrentSpeed);

            var currentRot = tr.rotation;
            tr.eulerAngles = new Vector3(currentRot.x, yRotation, zRotation);
        }

        public void Initialize(MotorcycleData data)
        {
            maxSpeed = data.MaxSpeed;
            acceleration = data.Acceleration;
            BreakDeceleration = data.BreakDeceleration;
            rotateAcceleration = data.RotateAcceleration;
            maxRotateSpeed = data.MaxRotateSpeed;
            maxSlantAngle = data.MaxSlantAngle;
        }

        public void ChangeSpeed(float input)
        {
            if (input > 0)
            {
                if (CurrentSpeed >= 0)
                    CurrentSpeed += input * acceleration * (Mathf.Pow(1 - CurrentSpeed / maxSpeed, 2) + accelerationHelp);
                else
                    CurrentSpeed += input * BreakDeceleration;
            }
            else if (input < 0)
            {
                if (CurrentSpeed >= 0)
                    CurrentSpeed += input * BreakDeceleration;
                else
                    CurrentSpeed += input * acceleration;

            }
            else
                CurrentSpeed += CurrentSpeed >= 0 ? -deceleration : deceleration;


            CurrentSpeed = Mathf.Clamp(CurrentSpeed, -reverseMaxSpeed, maxSpeed);
        }

        public void ChangeDirection(float input)
        {
            if (Mathf.Abs(CurrentSpeed) > turnThreshold)
            {
                if (input != 0)
                    currentRotateSpeed += input * rotateAcceleration;
                currentRotateSpeed *= decelerationRate;

                currentRotateSpeed = Mathf.Clamp(currentRotateSpeed, -maxRotateSpeed, maxRotateSpeed);

                var deltaRotation = currentRotateSpeed * Time.deltaTime * Mathf.Sign(CurrentSpeed);
                yRotation += deltaRotation;
                cameraMovement.SetRotationByY(deltaRotation);
            }
            
            ChangeSlant();
        }

        private void ChangeSlant()
        {
            zRotation = -maxSlantAngle * (currentRotateSpeed / maxRotateSpeed) * Mathf.Abs(CurrentSpeed / maxSpeed);
            cameraMovement.RotateByZ(zRotation);
        }

        public void ZeroSlant()
        {
            zRotation = 0;
            cameraMovement.RotateByZ(zRotation);
        }

        public void SetLowSpeed(float lowRate)
        {
            CurrentSpeed = maxSpeed * lowRate;
        }
    }
}
