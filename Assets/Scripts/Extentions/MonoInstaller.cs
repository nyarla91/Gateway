using UnityEngine;

namespace Extentions
{
    public class MonoInstaller : Zenject.MonoInstaller
    {
        protected GameObject BindFromInstance<T>(GameObject instance)
        {
            Container.Bind<T>().FromInstance(instance.GetComponent<T>()).AsSingle();
            return instance;
        }
        
        protected GameObject BindFromPrefab<T>(GameObject prefab, Transform parent = null)
        {
            GameObject instance = Container.InstantiatePrefab(prefab, parent ?? transform);
            BindFromInstance<T>(instance);
            return instance;
        }
    }
}