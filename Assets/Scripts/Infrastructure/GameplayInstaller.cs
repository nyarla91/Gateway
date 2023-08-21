using Gameplay.Entity.Player;
using UnityEngine;
using Zenject;
using MonoInstaller = Extentions.MonoInstaller;

namespace Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;
        
        public override void InstallBindings()
        {
            Container.Bind<GameplayActions>().AsSingle();
            GameObject player = BindFromPrefab<IPlayerTransformService>(_playerPrefab, _playerSpawnPoint);
            BindFromInstance<IPlayerCameraService>(player);
        }
    }
}