using System;
using UnityEngine;

namespace SceneManagement.Locations
{
    public class Location : MonoBehaviour
    {
        [SerializeField] private Transform[] _entrances;

        public Transform GetEntrance(int entrance)
        {
            if (entrance < 0 || entrance >= _entrances.Length)
                throw new ArgumentOutOfRangeException($"{gameObject} doesn't have {entrance} entrance");
            return _entrances[entrance];
        }
    }
}