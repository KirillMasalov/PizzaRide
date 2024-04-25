using System.Collections;
using System.Collections.Generic;
using Rider;
using UnityEngine;


namespace MotorCycle
{
    public class MotorcycleConstraintsController : MonoBehaviour
    {
        [field: SerializeField] public Transform HipsTarget { get; private set; }
        [field: SerializeField] public float RiderXAngle { get; private set; }
        [field: SerializeField] public Transform RightHandTarget { get; private set; }
        [field: SerializeField] public Transform LeftHandTarget { get; private set; }
        [field: SerializeField] public Transform RightLegTarget { get; private set; }
        [field: SerializeField] public Transform LeftLegTarget { get; private set; }
        
        [field: SerializeField] public Transform RightHandHint { get; private set; }
        [field: SerializeField] public Transform LeftHandHint { get; private set; }
        [field: SerializeField] public Transform RightLegHint { get; private set; }
        [field: SerializeField] public Transform LeftLegHint { get; private set; }

        public RiderConstraintsController RiderConstraintsController { get; private set; }

        public void Initialize(RiderConstraintsController rdConstraints)
        {
            RiderConstraintsController = rdConstraints;
        }
    }
}
        
