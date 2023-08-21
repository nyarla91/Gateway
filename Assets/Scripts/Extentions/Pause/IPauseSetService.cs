using UnityEngine;

namespace Extentions.Pause
{
    public interface IPauseSetService
    {
        void AddPauseSource(MonoBehaviour source);
        void RemovePauseSource(MonoBehaviour source);
    }
}