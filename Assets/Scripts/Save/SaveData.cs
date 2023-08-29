using System;
using Extentions;
using UnityEngine;

namespace Save
{
    [Serializable]
    public class SaveData : ISaveData
    {
        [SerializeField] private int _currentLocation;
        [SerializeField] private int _entrance;

        public int CurrentLocation
        {
            get => _currentLocation;
            set => _currentLocation = value;
        }

        public int Entrance
        {
            get => _entrance;
            set => _entrance = value;
        }
    }
}