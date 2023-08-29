using SceneManagement;
using Extentions.Pause;
using Input;
using Save;
using SceneManagement.Locations;
using UnityEngine;
using MonoInstaller = Extentions.MonoInstaller;

namespace Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _sceneLoaderPrefab;
        [SerializeField] private GameObject _deviceWatcherPrefab;
        [SerializeField] private GameObject _settingsPrefab;
        [SerializeField] private GameObject _pausePrefab;
        [SerializeField] private GameObject _locationsLibraryPrefab;
        [SerializeField] private GameObject _gameSavePrefab;
        
        public override void InstallBindings()
        {
            BindFromPrefab<LocationsLibrary>(_locationsLibraryPrefab);
            
            BindFromPrefab<SceneLoader>(_sceneLoaderPrefab);
            
            BindFromPrefab<DeviceWatcher>(_deviceWatcherPrefab);
            
            BindFromPrefab<Settings.Settings>(_settingsPrefab);
            
            GameObject pause = BindFromPrefab<IPauseSetService>(_pausePrefab);
            BindFromInstance<IPauseReadService>(pause);

            GameObject gameSave = BindFromPrefab<ISaveDataReadService>(_gameSavePrefab);
            BindFromInstance<ISaveDataWriteService>(gameSave);
        }
    }
}