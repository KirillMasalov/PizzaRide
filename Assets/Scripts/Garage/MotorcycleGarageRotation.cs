using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.MainMenu
{
    public class MotorcycleGarageRotation : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        private Transform tr;

        private void Awake()
        {
            tr = transform;
        }

        private void FixedUpdate()
        {
            tr.Rotate(Vector3.up, rotationSpeed);
        }
    }
}