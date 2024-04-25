using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class MotorcycleDataContainer : MonoBehaviour
    {
        [field: SerializeField] public MotorcycleData Data { get; private set; }
    }
}