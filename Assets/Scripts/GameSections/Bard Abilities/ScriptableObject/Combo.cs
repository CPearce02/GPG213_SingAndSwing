using Core.ScriptableObjects;
using UnityEngine;
using Enums;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "New Combo", menuName = "Combo")]
public class Combo : ScriptableObject
{
    [field: SerializeField] public List<ComboValues> ComboValues { get; private set; }
}
