using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Core.Bard
{
    public class ComboDictionary : MonoBehaviour
    {
        public static ComboDictionary instance;

        public Dictionary<ComboValues, SpriteRenderer> comboPrefabDictionary = new ();

        [SerializeField] private SpriteRenderer[] comboValuePrefabs;
        private void Awake()
        {
            // Loop through each enum value and corresponding prefab
            for (int i = 0; i < Enum.GetValues(typeof(ComboValues)).Length; i++)
            {
                ComboValues comboValue = (ComboValues)i;
                SpriteRenderer comboPrefab = comboValuePrefabs[i];

                // Add the enum value and prefab to the dictionary
                comboPrefabDictionary.Add(comboValue, comboPrefab);
            }
        }

        private void Start()
        {
            instance = this;  
        }
    }
}
