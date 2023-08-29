using Extentions.Factory;
using Gameplay.Player;
using Save;
using SceneManagement.Locations;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class LocationLoader : MonoBehaviour
    {
        [Inject] private ContainerFactory Factory { get; }
        [Inject] private LocationsLibrary LocationsLibrary { get; }
        [Inject] private ISaveDataReadService SaveDataRead { get; }
        [Inject] private IPlayerTransformService PlayerTransform { get; }

        private void Awake()
        {
            GameObject prefab = LocationsLibrary.GetLocationPrefab(SaveDataRead.SaveData.CurrentLocation);
            Location location = Factory.Instantiate<Location>(prefab, Vector3.zero);
            PlayerTransform.Transform.position = location.GetEntrance(SaveDataRead.SaveData.Entrance).position;
        }
    }
}