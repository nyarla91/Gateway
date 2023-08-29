using Gameplay.Player;
using Save;
using SceneManagement;
using SceneManagement.Locations;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class LocationEntrance : MonoBehaviour
    {
        [SerializeField] private int _targetLocation;
        [SerializeField] private int _targetEntrance;
        
        [Inject] private LocationsLibrary LocationsLibrary { get; }
        [Inject] private ISaveDataWriteService SaveDataWrite { get; }
        [Inject] private SceneLoader SceneLoader { get; }

        private void OnTriggerEnter(Collider other)
        {
            if ( ! other.TryGetComponent(out PlayerMovement player))
                return;
            SaveDataWrite.SaveDataWrirtable.CurrentLocation = _targetLocation;
            SaveDataWrite.SaveDataWrirtable.Entrance = _targetEntrance;
            SaveDataWrite.Save();
            SceneLoader.LoadGameplay();
        }
    }
}