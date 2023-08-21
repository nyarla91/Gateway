using SceneManagement;
using Extentions;
using Extentions.Pause;
using Input;
using UnityEngine;
using Zenject;
using MonoInstaller = Extentions.MonoInstaller;

namespace Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _sceneLoaderPrefab;
        [SerializeField] private GameObject _deviceWatcherPrefab;
        [SerializeField] private GameObject _settingsPrefab;
        [SerializeField] private GameObject _pausePrefab;
        
        public override void InstallBindings()
        {
            BindFromPrefab<SceneLoader>(_sceneLoaderPrefab);
            BindFromPrefab<DeviceWatcher>(_deviceWatcherPrefab);
            BindFromPrefab<Settings.Settings>(_settingsPrefab);
            GameObject pause = BindFromPrefab<IPauseSetService>(_pausePrefab);
            BindFromInstance<IPauseReadService>(pause);
        }
    }
}