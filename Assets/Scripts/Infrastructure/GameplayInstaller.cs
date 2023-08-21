using Gameplay.Entity.Player;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;
        
        public override void InstallBindings()
        {
            Container.Bind<GameplayActions>().AsSingle();
            Container.InstantiatePrefab(_playerPrefab, _playerSpawnPoint);
        }
    }
}