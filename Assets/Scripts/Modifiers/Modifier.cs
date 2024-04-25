using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modifiers
{
    public class Modifier : ScriptableObject
    {
        [field:SerializeField] public virtual float Value { get; set; }
        [field:SerializeField] public virtual string Name { get; set; }
        [field:SerializeField] public virtual string Description { get; set; }
        public virtual ModifierType MType { get; }

        public virtual void ModifierOn() { }

        public virtual void ModifierOff() { }


    }
}
