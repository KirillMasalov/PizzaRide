using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MotorCycle
{
    public class AddtionalGravity : MonoBehaviour
    {
        [SerializeField] private float value;
        [SerializeField] private Rigidbody rigidBody;

        private void FixedUpdate()
        {
            rigidBody.velocity += Vector3.down * value;
        }
    }
}
