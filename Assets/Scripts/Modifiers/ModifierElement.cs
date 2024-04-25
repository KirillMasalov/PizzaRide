using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modifiers
{
    public class ModifierElement
    {
        public string MName { get; private set; }
        public ModifierType MType { get; private set; }
        public float Value { get; private set; }

        public ModifierElement(string name, ModifierType mType, float value)
        {
            MName = name;
            MType = mType;
            Value = value;
        }
    }
}
