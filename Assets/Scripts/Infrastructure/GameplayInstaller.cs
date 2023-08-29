using Gameplay;
using Gameplay.Gateways;
using Gameplay.Player;
using Gameplay.UI;
using UnityEngine;
using MonoInstaller = Extentions.MonoInstaller;

namespace Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerOrigin;
        [SerializeField] private GatewaySystem _gatewaySystem;
        [SerializeField] private InteractablePrompt _interactablePrompt;
        
        public override void InstallBindings()
        {
            BindFromInstance(_interactablePrompt);
            BindFromInstance(_gatewaySystem);
            Container.Bind<GameplayActions>().AsSingle();
            GameObject player = BindFromPrefab<IPlayerTransformService>(_playerPrefab, _playerOrigin);
            BindFromInstance<IPlayerCameraService>(player);
            
        }
    }
}