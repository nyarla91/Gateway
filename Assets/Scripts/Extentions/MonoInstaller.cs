using UnityEngine;

namespace Extentions
{
    public class MonoInstaller : Zenject.MonoInstaller
    {
        protected void BindFromInstance<T>(GameObject instance) => BindFromInstance<T>(instance.GetComponent<T>());

        protected void BindFromInstance<T>(T instance) => Container.Bind<T>().FromInstance(instance).AsSingle();

        protected GameObject BindFromPrefab<T>(GameObject prefab, Transform parent = null)
        {
            GameObject instance = Container.InstantiatePrefab(prefab, parent ?? transform);
            BindFromInstance<T>(instance);
            return instance;
        }
    }
}