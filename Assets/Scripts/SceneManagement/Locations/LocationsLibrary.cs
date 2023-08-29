using System;
using UnityEngine;

namespace SceneManagement.Locations
{
    public class LocationsLibrary : MonoBehaviour
    {
        [SerializeField] private GameObject[] _locations;

        public GameObject GetLocationPrefab(int location)
        {
            if (location < 0 || location >= _locations.Length)
                throw new ArgumentOutOfRangeException($"There is no {location} location");
            return _locations[location];
        }
    }
}